using UnityEngine;
using System.Collections;

namespace LuaFramework {
    public class Main : MonoBehaviour {
        void Awake()
        {
            LanguageManager.Instance.initLanguage();
            DataHubManager.Instance.LoadAll();
        }

        void Start() {
            AppFacade.Instance.StartUp();   //启动游戏
        }
    }
}