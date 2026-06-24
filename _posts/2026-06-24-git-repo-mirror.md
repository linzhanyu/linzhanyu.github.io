---
layout: post
title: 对一个GitHub仓库做本地镜像且维护修改分支 (高级进阶版)
categories: [git]
tags: [git]
fullview: false
comments: true
---


这篇文章虽然有点AI味儿, 但它不全是AI写的, 它是能解决实际问题的可实践步骤全流程. 我只是懒得码那么多字.

### 目标

对 GitHub 上的开源仓库做一个 Gitea 本地镜像, 包含所有分支, tag. 针对该仓库做的本地修改, 也能推送到本地Gitea仓库中, 还能定期更新GitHub上的提交, 合并, 升级为最新版本。

> [!IMPORTANT]
> **关于分支名称的说明**：本文档中的代码示例使用了 `<your_branch>` 作为占位符。在实际操作时，请将其替换为你想要使用的主分支名（例如 `master` 或 `develop`）。

#### 注意：为什么不能直接用 `git clone --mirror`？
如果你使用 `--mirror` 克隆，你得到的是一个“裸仓库”（没有工作目录），且任何同步操作都会强制将远程的状态与本地完全对齐。这意味着**一旦 GitHub 更新了分支列表或 Tag，你在 Gitea 上做的所有自定义提交和新开的分支都会被抹除（Overwrite）**。

为了实现“全量镜像”+“保留修改”，我们需要采用 **双远程 Refspec 映射法**。

---

### 实现方案：双远程工作流 (Dual-Remote Workflow)

这种方案的核心思想是：建立一个带有完整工作目录的仓库，通过特殊的 `refspec` 命令将 GitHub 的分支变更批量同步到 Gitea，同时利用 `rebase` 保留本地修改。

#### 第一阶段：环境初始化 (Setup)

1.  **克隆原始仓库（标准模式）**
    不要用 `--mirror`！我们要一个能写代码的普通仓库。
    ```bash
    # 克隆 GitHub 仓库，此时 origin 指向 GitHub
    git clone https://github.com/original-owner/repo.git ~/my-work-dir
    cd ~/my-work-dir

    # 为了语义清晰，我们将远程名称改为 github 和 gitea
    # 首先将原本的 origin (GitHub) 改名为 github
    git remote rename origin github
    # 然后添加你的 Gitea 作为第二个远程 (gitea)
    git remote add gitea https://gitea.yourdomain.com/your-user/my-repo.git

    # 先把当前代码推送到 Gitea 初始化仓库
    # 注意：请将 <your_branch> 替换为你的主分支名 (如 master 或 develop)
    git push -u gitea <your_branch>
    ```

2.  **配置状态检查**
    执行 `git remote -v`，你应该看到：
    *   `github`: 指向 **GitHub** (作为原始代码源)
    *   `gitea`: 指向你的 **Gitea** (用于存放你的修改和镜像结果)

#### 第二阶段：日常开发与推送 (Development)

你可以像往常一样在本地创建分支并提交。这些内容只会推送到 Gitea，不会干扰 GitHub 的同步逻辑。

```bash
# 1. 创建新特性分支
git checkout -b feat/my-custom-stuff

# 2. 修改代码并提交
git add .
git commit -m "feat: 我的本地特殊修改"

# 3. 推送到 Gitea (gitea)
git push gitea feat/my-custom-stuff
```

#### 第三阶段：全量同步 GitHub 的更新 (The Magic Sync)

这是解决你问题的核心。当 GitHub 上有了**新的分支、新的 Tag 或新的提交**时，按以下两步操作：

##### 1. 获取 GitHub 的所有变更（包括新分支和 Tag）
```bash
# 拉取 github 的所有更新到本地的远程跟踪引用中
git fetch github --tags
```

##### 2. 将 GitHub 的所有分支“投影”到 Gitea 上
这是最关键的一步。我们利用 `refspec` 直接把 `github` 下的所有分支推送到 `gitea`，而**不会覆盖你正在工作的本地分支**（因为它们在不同的命名空间下）。

```bash
# 魔法命令：将所有 github 的远程跟踪分支 推送为 gitea 的 heads 分支
git push gitea 'refs/remotes/github/*:refs/heads/*'

# 如果恰巧你本地有了一个同名的分支, 那就用这个命令完成推送.
git push gitea '+refs/remotes/github/*:refs/heads/*'

# 同时同步所有的 Tag 到 Gitea
git push gitea --tags
```
**效果**：GitHub 上新开的 `feature-x` 之后，会自动出现在 Gitea 的分支列表里。而你本地正在开发的 `feat/my-custom-stuff` 不会受到任何影响。

#### 第四阶段：将 GitHub 更新合并进你的工作 (Rebase)

同步完镜像后，你需要把你本地的修改“垫”在 GitHub 最新代码之上，以保持历史整洁且不产生冲突。

```bash
# 1. 切换到你正在工作的分支
git checkout feat/my-custom-stuff

# 2. 使用 rebase 将 github 最新的提交合并进来
# 注意：请将 <your_branch> 替换为你的主分支名 (如 master 或 develop)
git rebase github/<your_branch>

# [如果发生冲突]
# 手动解决文件中的 <<<<<<< HEAD -> ======= -> >>>>>>> 标记
# git add <resolved-file>
# git rebase --continue

# 3. 更新 Gitea (由于重写了历史，需要使用 force-with-lease)
git push gitea feat/my-custom-stuff --force-with-lease
```

---

### 总结：避坑指南与最佳实践

| 问题 | 对策 | 命令 / 原理 |
| :--- | :--- | :--- |
| **GitHub 新加了分支，Gitea 没显示？** | 使用 Refspec 批量推送 | `git push gitea 'refs/remotes/github/*:refs/heads/*'` |
| **本地提交被 GitHub 更新覆盖了？** | 不要用 `--mirror`，使用 `rebase` | `git rebase github/<your_branch>` |
| **强制推送太危险怎么办？** | 使用更安全的 force 模式 | `git push --force-with-lease` (它会检查远程是否有你不知道的新提交) |

#### 💡 高级技巧：自动化同步脚本

你可以把第三阶段的操作写成一个简单的 `.sh` 脚本，每天定时运行（Crontab），实现真正的“自动镜像”：

```bash
#!/bin/bash
# sync_mirror.sh

echo "🚀 Starting Sync: GitHub -> Gitea"

# 1. 获取上游所有内容
git fetch github --tags

# 2. 同步分支到 Gitea (不影响本地 working tree)
git push gitea 'refs/remotes/github/*:refs/heads/*'

# 3. 同步 Tag 到 Gitea
git push gitea --tags

echo "✅ Sync Complete!"
```

这样，你就拥有了一个**既能全量同步 GitHub 所有动态、又能完美保护本地修改**的专业级镜像仓库。
