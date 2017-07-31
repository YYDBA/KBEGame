using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LanguageLocalization : MonoBehaviour {
    protected Text content;
    public string key = "";
    public string Key
    {
        get
        {
            return key;
        }

        set
        {
            key = value;
            content.text = LanguageManager.Instance.Get(key);
        }
    }

    void Start()
    {
        content = GetComponent<Text>();
        content.text = LanguageManager.Instance.Get(key);
    }
}
