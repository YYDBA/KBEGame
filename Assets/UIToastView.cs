using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToastView : View {
    public static UIToastView Instance;
    protected Text content;
    protected Canvas canvas;
	void Awake () {
        Instance = this;
        content = transform.Find("Content").GetComponent<Text>();
        canvas = GetComponent<Canvas>();
    }

    public void Show(string msg,float duration = 3f)
    {
        content.text = msg;
        canvas.enabled = true;
        CancelInvoke("Hide");
        Invoke("Hide", duration);
    }

    void Hide()
    {
        canvas.enabled = false;
    }
}
