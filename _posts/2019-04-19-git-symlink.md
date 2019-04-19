---
layout: post
title: GIT Symlink 兼容Win与Linux
categories: [git]
tags: [git]
fullview: false
comments: true
---

不得不先吐槽一下 Win10 这个运行权限搞得越来越不爽了.

### 先解决烦人的权限问题

![Win10.AdminRun](/assets/image/AdminRun.png)

重启后可以从根本上解决掉以管理员运行的问题

### 添加命令别名

[参考资料](http://stackoverflow.com/a/16754068)

#### 让Win仓库支持 symlink

1. 安装 GIT 时需要选择 support symlink
1. clone 仓库的时候使用如下命令

{% highlight shell %}
git clone -c core.symlinks=true <URL>
{% endhighlight %}

#### 在 Windows 上创建 symlink
{% highlight shell %}
git config --global alias.add-symlink '!'"$(cat <<'ETX'
__git_add_symlink() {
  if [ $# -ne 2 ] || [ "$1" = "-h" ]; then
    printf '%b\n' \
        'usage: git add-symlink <source_file_or_dir> <target_symlink>\n' \
        'Create a symlink in a git repository on a Windows host.\n' \
        'Note: source MUST be a path relative to the location of target'
    [ "$1" = "-h" ] && return 0 || return 2
  fi

  source_file_or_dir=${1#./}
  source_file_or_dir=${source_file_or_dir%/}

  target_symlink=${2#./}
  target_symlink=${target_symlink%/}
  target_symlink="${GIT_PREFIX}${target_symlink}"
  target_symlink=${target_symlink%/.}
  : "${target_symlink:=.}"

  if [ -d "$target_symlink" ]; then
    target_symlink="${target_symlink%/}/${source_file_or_dir##*/}"
  fi

  case "$target_symlink" in
    (*/*) target_dir=${target_symlink%/*} ;;
    (*) target_dir=$GIT_PREFIX ;;
  esac

  target_dir=$(cd "$target_dir" && pwd)

  if [ ! -e "${target_dir}/${source_file_or_dir}" ]; then
    printf 'error: git-add-symlink: %s: No such file or directory\n' \
        "${target_dir}/${source_file_or_dir}" >&2
    printf '(Source MUST be a path relative to the location of target!)\n' >&2
    return 2
  fi

  git update-index --add --cacheinfo 120000 \
      "$(printf '%s' "$source_file_or_dir" | git hash-object -w --stdin)" \
      "${target_symlink}" \
    && git checkout -- "$target_symlink" \
    && printf '%s -> %s\n' "${target_symlink#$GIT_PREFIX}" "$source_file_or_dir" \
    || return $?
}
__git_add_symlink
ETX
)"
{% endhighlight %}

Usage: git add-symlink [source_file_or_dir] [target_symlink]

##### 删除 symlink

{% highlight shell %}
git config --global alias.rm-symlinks '!'"$(cat <<'ETX'
__git_rm_symlinks() {
  case "$1" in (-h)
    printf 'usage: git rm-symlinks [symlink] [symlink] [...]\n'
    return 0
  esac
  ppid=$$
  case $# in
    (0) git ls-files -s | grep -E '^120000' | cut -f2 ;;
    (*) printf '%s\n' "$@" ;;
  esac | while IFS= read -r symlink; do
    case "$symlink" in
      (*/*) symdir=${symlink%/*} ;;
      (*) symdir=. ;;
    esac

    git checkout -- "$symlink"
    src="${symdir}/$(cat "$symlink")"

    posix_to_dos_sed='s_^/\([A-Za-z]\)_\1:_;s_/_\\\\_g'
    doslnk=$(printf '%s\n' "$symlink" | sed "$posix_to_dos_sed")
    dossrc=$(printf '%s\n' "$src" | sed "$posix_to_dos_sed")

    if [ -f "$src" ]; then
      rm -f "$symlink"
      cmd //C mklink //H "$doslnk" "$dossrc"
    elif [ -d "$src" ]; then
      rm -f "$symlink"
      cmd //C mklink //J "$doslnk" "$dossrc"
    else
      printf 'error: git-rm-symlink: Not a valid source\n' >&2
      printf '%s =/=> %s  (%s =/=> %s)...\n' \
          "$symlink" "$src" "$doslnk" "$dossrc" >&2
      false
    fi || printf 'ESC[%d]: %d\n' "$ppid" "$?"

    git update-index --assume-unchanged "$symlink"
  done | awk '
    BEGIN { status_code = 0 }
    /^ESC\['"$ppid"'\]: / { status_code = $2 ; next }
    { print }
    END { exit status_code }
  '
}
__git_rm_symlinks
ETX
)"

git config --global alias.rm-symlink '!git rm-symlinks'  # for back-compat.
{% endhighlight %}

Usage: git rm-symlinks [symlink] [symlink] [...]

##### 还原 symlink

{% highlight shell %}
git config --global alias.checkout-symlinks '!'"$(cat <<'ETX'
__git_checkout_symlinks() {
  case "$1" in (-h)
    printf 'usage: git checkout-symlinks [symlink] [symlink] [...]\n'
    return 0
  esac
  case $# in
    (0) git ls-files -s | grep -E '^120000' | cut -f2 ;;
    (*) printf '%s\n' "$@" ;;
  esac | while IFS= read -r symlink; do
    git update-index --no-assume-unchanged "$symlink"
    rmdir "$symlink" >/dev/null 2>&1
    git checkout -- "$symlink"
    printf 'Restored git symlink: %s -> %s\n' "$symlink" "$(cat "$symlink")"
  done
}
__git_checkout_symlinks
ETX
)"

git config --global alias.co-symlinks '!git checkout-symlinks'
{% endhighlight %}

Usage: git checkout-symlinks [symlink] [symlink] [...]

