using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LanguageManager
{
    private static LanguageManager instance;
    private Dictionary<string, string> stringDict;

    public Dictionary<string, string> StringDict
    {
        get
        {
            return stringDict;
        }
    }

    public static LanguageManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new LanguageManager();
            }
            return instance;
        }
    }

    public void initLanguage()
    {
        SystemLanguage language = Application.systemLanguage;
        TextAsset bytesTxt;
        switch (language)
        {
            case SystemLanguage.Chinese:
            case SystemLanguage.ChineseSimplified:
            case SystemLanguage.ChineseTraditional:
                bytesTxt = Resources.Load<TextAsset>("Bytes/CN");
                break;
            case SystemLanguage.English:
                bytesTxt = Resources.Load<TextAsset>("Bytes/EN");
                break;
            default:
                bytesTxt = Resources.Load<TextAsset>("Bytes/CN");
                break;
        }
        Read(bytesTxt.bytes);
    }

    void Read(byte[] content)
    {
        char STX = (char)0x02;
        stringDict = new Dictionary<string, string>();
        string txt = System.Text.Encoding.UTF8.GetString(content);
        string[] rows = txt.Split(new string[]{ "\r\n"},StringSplitOptions.RemoveEmptyEntries);
        for(int i = 0;i <rows.Length;++i)
        {
            string[] cols = rows[i].Split(STX);
            stringDict.Add(cols[0], cols[1]);
        }
    }

    public string Get(string key)
    {
        string value = "";
        if(!stringDict.TryGetValue(key, out value))
        {
            Debug.LogError("Key not exits:" + key);
        }
        return value;
    }
}