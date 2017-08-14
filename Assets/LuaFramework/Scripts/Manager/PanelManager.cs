using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using LuaInterface;

namespace LuaFramework {
    public class PanelManager : Manager {

        public void CreatePanel(string name, LuaFunction func = null) {
            string assetName = name + "Panel";
            string abName = name.ToLower() + AppConst.ExtName;
            if (RootManager.Instance.UIRoot.FindChild(assetName) != null) return;
            GameObject prefab = ResManager.LoadAsset<GameObject>(name, assetName);
            if (prefab == null) return;

            GameObject go = Instantiate(prefab) as GameObject;
            go.name = assetName;
            go.transform.SetParent(RootManager.Instance.UIRoot);
            RectTransform rect = go.transform.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            go.transform.localScale = Vector3.one;
            go.transform.localRotation = Quaternion.identity;
            go.AddComponent<LuaBehaviour>();
            UIManager.Instance.Push(assetName, go.GetComponent<View>());
            if (func != null) func.Call(go);
        }

        public void ClosePanel(string name) {
            var panelName = name + "Panel";
            var panelObj = RootManager.Instance.UIRoot.FindChild(panelName);
            if (panelObj == null) return;
            Destroy(panelObj.gameObject);
        }
    }
}