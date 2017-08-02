using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager Instance;
    protected Stack<View> _contextStack;
    protected Dictionary<string, View> _uiDict;
    protected UIManager() {
        Instance = this;
        _contextStack = new Stack<View>();
        _uiDict = new Dictionary<string, View>();
    }

    public void Pop()
    {
        if (_contextStack.Count != 0)
        {
            View view = _contextStack.Peek();
            _contextStack.Pop();
            view.OnExit();
        }

        if (_contextStack.Count != 0)
        {
            View view = _contextStack.Peek();
            view.OnResume();
        }
    }

    public void Push(string uiName)
    {
        if (_contextStack.Count != 0)
        {
            View view = _contextStack.Peek();
            view.OnPause();
        }
        View _view = CheckView(uiName);
        _contextStack.Push(_view);
        _view.OnEnter();
    }

    void OnDestory()
    {
        _uiDict = null;
        _contextStack = null;
        Instance = null;
    }

    View CheckView(string uiName)
    {
        View view;
        if(!_uiDict.TryGetValue(uiName, out view))
        {
            view = LoadUIByName(uiName);
        }
        return view;
    }

    View LoadUIByName(string uiName)
    {
        Object prefab = Resources.Load("UI/"+uiName,typeof(GameObject));
        GameObject go = Instantiate(prefab) as GameObject;
        go.name = uiName;
        go.transform.SetParent(RootManager.Instance.UIRoot);
        RectTransform rect = go.transform.GetComponent<RectTransform>();
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        go.transform.localScale = Vector3.one;
        go.transform.localRotation = Quaternion.identity;
        return go.GetComponent<View>();
    }
}
