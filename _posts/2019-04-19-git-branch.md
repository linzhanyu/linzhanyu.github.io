---
layout: post
title: GIT 分支相关问题
categories: [git]
tags: [git]
fullview: false
comments: true
---

GIT的分支是众多版本管理软件中最好用的,没有之一.在创建,同步,合并过程中都显示出其设计的优越性.这里主要记录一些和分支相关的操作.

---

### 创建分支

{% highlight shell %}
git checkout -b branch-name
{% endhighlight %}

---
### 分支改名

{% highlight shell %}
git branch -m oldName newName
{% endhighlight %}

---
### 删除分支

#### 删除本地分支

{% highlight shell %}
git branch -d  branch-name
{% endhighlight %}

#### 删除远程分支

{% highlight shell %}
git push origin :remote_branch_name
{% endhighlight %}

#### 查看分支追踪情况

{% highlight shell %}
git remote show origin
{% endhighlight %}

#### 本地同步远端已被删除的分支

{% highlight shell %}
git remote prune origin
{% endhighlight %}

---

### 将本地分支推到远程如果不存在就创建

{% highlight shell %}
git push origin local_branch_name:remote_branch_name
{% endhighlight %}

---
### 合并Commit

- 从HEAD版本开始往过去数三个版本
{% highlight shell %}
git rebase -i HEAD~3
{% endhighlight %}

- 指定要合并之前的版本号
{% highlight shell %}
git rebase -i commitId
{% endhighlight %}

---
### 查看某个 Commit 所属的分支

{% highlight shell %}
git branch --contains commitId
{% endhighlight %}

---
### 对比两个分支的差异


#### 显示两个分支的差异文件列表

{% highlight shell %}
git diff branch1 branch2 --stat
{% endhighlight %}

#### 显示指定文件的详细差异

{% highlight shell %}
git diff branch1 branch2 filePath
{% endhighlight %}

#### 显示所有文件的详细差异

{% highlight shell %}
git diff branch1 branch2
{% endhighlight %}



---
### 合并分支

{% highlight shell %}
git checkout branch-name
git pull
git merge origin/other-branch-name
{% endhighlight %}

### 合并分支但不形成 merge commit
{% highlight shell %}
git fetch origin
git checkout target-branch-name
git merget --no-commit source-branch-name
{% endhighlight %}
---
### 从其它分支抓取 Commit

{% highlight shell %}
git cherry-pick commitId

git cherry-pick commitId0 .. commitIdN 
{% endhighlight %}

---
~~将指定文件夹换成指定分支中的版本~~

~~撤消分支合并~~

### 只查看指定分支的 commit 记录

{% highlight shell %}
git log --oneline --walk-reflogs branch-name
{% endhighlight %}

---

### Tag 操作
#### 添加 TAG

{% highlight shell %}
git tag -a v1.0.1.1 commit-id
{% endhighlight %}


#### 删除 TAG

{% highlight shell %}
git tag -d v1.0.1.1
{% endhighlight %}

#### 删除远程 TAG

{% highlight shell %}
git push origin :refs/tags/v1.0.1.1
{% endhighlight %}

#### 推送 TAG

{% highlight shell %}
git push origin v1.0.1.1
git push origin --tags
{% endhighlight %}

---

### stash 相关

#### 把工作区中的修改藏起来
{% highlight shell %}
git stash push -m "message"
git stash save
git stash store
{% endhighlight %}

#### 查看 stash 保存的内容

{% highlight shell %}
git stash list
git stash show -p
git stash show -p stash@{1}
{% endhighlight %}

#### 把 stash 中的内容还原出来

{% highlight shell %}
git stash pop
git stash apply
{% endhighlight %}

#### 把 stash 中的内容清除掉

{% highlight shell %}
git stash clear
git stash drop
{% endhighlight %}

---

### git 使用指定的密钥

{% highlight shell %}
git config core.sshCommand "ssh -i ~/.ssh/id_rsa_example -F /dev/null"
{% endhighlight %}


---

### 添加多个 tracked repositories
#### 添加

{% highlight shell %}
git remote add name git-url
{% endhighlight %}

#### 删除

{% highlight shell %}
git remote remove name
{% endhighlight %}

#### 改名

{% highlight shell %}
git remote rename <old> <new>
{% endhighlight %}

#### 裸仓库

{% highlight shell %}
git clone --bare git-url
{% endhighlight %}

#### 更新祼仓库

{% highlight shell %}
git fetch origin +refs/heads/*:refs/heads/* --prune
{% endhighlight %}

#### 镜像推送

{% highlight shell %}
git push --mirror <remote-repo>
{% endhighlight %}

---

### SubModule 使用

#### 1. 添加

{% highlight shell %}
git submodule add git-url path
{% endhighlight %}

***
#### 2. 使用

在已经添加了子模块的仓库拉取之后需要用下面的方式将子模块中的代码同步到本地.
{% highlight shell %}
git submodule init
git submodule update
{% endhighlight %}

或者:

{% highlight shell %}
git submodule update --init --recursive
{% endhighlight %}

***
#### 3. 更新

子模块的维护者提交了更新以后,使用子模块的项目必须手动更新才能包含最新提交.

进入到子模块目录下执行 __git pull__ 更新, 然后在项目目录中 __git add__ 提交即可.

统一更新全部子模块

{% highlight shell %}
git submodule foreach git pull
{% endhighlight %}

引用的子模块可以锚定在某一个 commit 或者某一个特定的分支上,使用维护起来非常方便.

***
#### 4. 删除子模块

这个操作不多见,但是遇到了就挺烧脑的.

- 删除子模块目录
{% highlight shell %}
rm -rf 子模块目录
{% endhighlight %}

- 打开 __.gitmodules__ 删除相关条目
- 打开 __.git/config__ 删除配置中子模块相关条目
- 删除 __.git/module/__中的对应目录. 每一个子模块在该目录下会有一个对应的目录 只删除对应的子模块目录即可
{% highlight shell %}
rm -rf .git/module/... 
{% endhighlight %}
- 删除缓存区的子模块相关内容
{% highlight shell %}
git rm --cached 子模块名
{% endhighlight %}

- 完成删除后提交到仓库即可.


