---
layout: post
title: C# ToLower 速度优化
categories: [unity3d]
tags: [string, optimization]
fullview: false
comments: true
---

C# program that implements lowercase optimization
{% highlight cshape %}
public static class Program
{
    static string _lookupStringL;
    static string _lookupStringU;

    public static char ToLowerFast(char c)
    {
        // Use char lookup table.
        return _lookupStringL[c];
    }

    public static char ToUpperFast(char c)
    {
        // Use char lookup table.
        return _lookupStringU[c];
    }

    public static char ToLowerFastIf(char c)
    {
        // Use if-statement.
        if (c >= 'A' && c <= 'Z')
        {
            return (char)(c + 32);
        }
        else
        {
            return c;
        }
    }

    public static char ToUpperFastIf(char c)
    {
        // Use if-statement.
        if (c >= 'a' && c <= 'z')
        {
            return (char)(c - 32);
        }
        else
        {
            return c;
        }
    }

    public static void Main()
    {
        // Init the strings.
        char[] lData = new char[128];
        char[] uData = new char[128];
        for (int i = 0; i < 128; i++)
        {
            char value = (char)i;
            if (!char.IsLetter(value))
            {
                lData[i] = '-';
                uData[i] = '-';
            }
            else
            {
                lData[i] = ToLowerFastIf(value);
                uData[i] = ToUpperFastIf(value);
            }
        }

        _lookupStringL = new string(lData);
        _lookupStringU = new string(uData);

        // Test the char transformation methods.
        string phrase = "Is it a BIRD?";
        char[] array = phrase.ToCharArray();
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = ToLowerFast(array[i]);
        }
        string result = new string(array);
        System.Console.WriteLine("IN:  " + phrase);
        System.Console.WriteLine("OUT: " + result);
    }
}
{% endhighlight %}

Optimized ways to lowercase characters:
char.ToLower: 10704 ms   (Framework)
ToLowerIf:     1231 ms   (If-statement)
ToLowerFast:    563 ms   (Lookup table)
