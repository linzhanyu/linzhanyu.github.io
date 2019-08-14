---
layout: post
title: GIT 分支相关问题
categories: [git]
tags: [git]
fullview: false
comments: true
---

### 添加多个 tracked repositories

### 创建分支

git checkout -b branch-name

### 分支改名

git branch -m oldName newName

### 删除分支

#### 删除本地分支

git branch -d  branch-name

#### 删除远程分支

git push origin :remote_branch_name

#### 将本地分支推到远程如果不存在就创建

git push origin local_branch_name:remote_branch_name

### 合并Commit

### 合并分支

git checkout branch-name
git pull
git merge origin/other-branch-name

### 从其它分支抓取 Commit

git cherry-pick commitId

git cherry-pick commitId0 .. commitIdN 

### 将指定文件夹换成指定分支中的版本

### 撤消分支合并

### 只查看指定分支的 commit 记录

git log --oneline --walk-reflogs branch-name


### Tag 操作
#### 添加 TAG

git tag -a v1.0.1.1 commit-id


#### 删除 TAG

git tag -d v1.0.1.1

#### 推送 TAG

git push origin v1.0.1.1
git push origin --tags

