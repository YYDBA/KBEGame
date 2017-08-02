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

    }

    public virtual void OnExit()
    {
    }

    public virtual void OnPause()
    {
    }

    public virtual void OnResume()
    {
    }
}
