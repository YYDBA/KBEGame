using UnityEngine;
using System.Collections;

namespace LuaFramework
{
    public class Main : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(transform.parent.gameObject);
            LanguageManager.Instance.initLanguage();
            DataHubManager.Instance.LoadAll();
        }

        void Start()
        {
            AppFacade.Instance.StartUp();
        }
    }
}