using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToastView : View {
    public static UIToastView Instance;
    protected Text content;

	void Awake () {
        Instance = this;
        content = transform.Find("Content").GetComponent<Text>();
    }

    public void Show(string msg,float duration = 3f)
    {
        content.text = msg;
        gameObject.SetActive(true);
        CancelInvoke("Hide");
        Invoke("Hide", duration);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
