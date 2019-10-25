---
layout: post
title: GIT 分支相关问题
categories: [git]
tags: [git]
fullview: false
comments: true
---

分支常用操作
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
### 合并分支

{% highlight shell %}
git checkout branch-name
git pull
git merge origin/other-branch-name
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

#### 查看 stash 保存的内容

{% highlight shell %}
git stash show -p
git stash show -p stash@{1}
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


