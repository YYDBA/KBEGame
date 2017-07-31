using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class DataHubManager{
    private static DataHubManager instance;
    private Dictionary<Type, List<object>> dataDict;
    protected const string dirPrefix = "Bytes/";
    public Dictionary<Type, List<object>> DataDict
    {
        get
        {
            return dataDict;
        }
    }

    public static DataHubManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DataHubManager();
            }
            return instance;
        }
    }

    public void LoadAll()
    {
        dataDict = new Dictionary<Type, List<object>>();
        LoadData<SkillData>();
    }

    public void LoadData<T>()
    {
        string bytesFileName = typeof(T).Name;
        TextAsset bytesTxt;
        bytesTxt = Resources.Load<TextAsset>(dirPrefix + bytesFileName);
        Read<T>(bytesFileName, bytesTxt.bytes);
    }

    void Read<T>(string tName,byte[] content)
    {
        char STX = (char)0x02;
        string txt = System.Text.Encoding.Default.GetString(content);
        string[] rows = txt.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

        System.Type t = System.Type.GetType(tName);
        object inst = Activator.CreateInstance(t, null);
        List<object> _list = new List<object>();
        FieldInfo[] fileds = inst.GetType().GetFields();
        for (int i = 0; i < rows.Length; ++i)
        {
            inst = Activator.CreateInstance(t, null);
            string[] cols = rows[i].Split(STX);
            for (int j = 0; j < cols.Length; ++j)
            {
                fileds[j].SetValue(inst, Convert.ChangeType(cols[j], fileds[j].FieldType));
            }
            _list.Add(inst);
        }
        dataDict.Add(typeof(T), _list);
    }

    #region 泛型访问区域

    public List<T> Get<T>()
    {
        return (dataDict[typeof(T)]).ConvertAll<T>(a => (T)a);
    }

    public List<T> Get<T>(Func<T,bool> predicate)
    {
        List<T> _list =  (dataDict[typeof(T)]).ConvertAll<T>(a => (T)a);
        return _list.Where(predicate).ToList<T>();
    }

    public T GetFrist<T>(Func<T, bool> predicate)
    {
        List<T> _list = (dataDict[typeof(T)]).ConvertAll<T>(a => (T)a);
        _list = _list.Where(predicate).ToList<T>();
        if(_list.Count == 0)
        {
            return default(T);
        }
        else
        {
            return _list[0];
        }
    }

    #endregion
}
