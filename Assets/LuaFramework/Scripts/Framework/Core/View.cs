using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using LuaFramework;

public class View : Base, IView {
    public virtual void OnMessage(IMessage message) {
    }

    public virtual void OnEnter()
    {
        GetComponent<Canvas>().enabled = true;
    }

    public virtual void OnExit()
    {
        GetComponent<Canvas>().enabled = false;
    }

    public virtual void OnPause()
    {
        GetComponent<Canvas>().enabled = false;
    }

    public virtual void OnResume()
    {
        GetComponent<Canvas>().enabled = true;
    }
}
