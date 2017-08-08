using LuaFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;

public class UILoginView : View
{
    string stringAccount, stringPasswd;
    void Start()
    {
        RegisterEvents();
    }

    void OnLogin()
    {
        //KBEngine.Event.fireIn("login", stringAccount, stringPasswd, System.Text.Encoding.UTF8.GetBytes("kbengine_unity3d_demo"));
    }

    public void DO()
    {
        LuaManager.DoFile("View/UILoginView");
        Util.CallMethod("UILoginView", "OnDO",new object[]{ "kk"});
    }

    void RegisterEvents()
    {
        // common
        //KBEngine.Event.registerOut("onKicked", this, "onKicked");
        //KBEngine.Event.registerOut("onDisconnected", this, "onDisconnected");
        //KBEngine.Event.registerOut("onConnectionState", this, "onConnectionState");

        //// login
        //KBEngine.Event.registerOut("onCreateAccountResult", this, "onCreateAccountResult");
        //KBEngine.Event.registerOut("onLoginFailed", this, "onLoginFailed");
        //KBEngine.Event.registerOut("onVersionNotMatch", this, "onVersionNotMatch");
        //KBEngine.Event.registerOut("onScriptVersionNotMatch", this, "onScriptVersionNotMatch");
        //KBEngine.Event.registerOut("onLoginBaseappFailed", this, "onLoginBaseappFailed");
        //KBEngine.Event.registerOut("onLoginSuccessfully", this, "onLoginSuccessfully");
        //KBEngine.Event.registerOut("onReloginBaseappFailed", this, "onReloginBaseappFailed");
        //KBEngine.Event.registerOut("onReloginBaseappSuccessfully", this, "onReloginBaseappSuccessfully");
        //KBEngine.Event.registerOut("onLoginBaseapp", this, "onLoginBaseapp");
        //KBEngine.Event.registerOut("Loginapp_importClientMessages", this, "Loginapp_importClientMessages");
        //KBEngine.Event.registerOut("Baseapp_importClientMessages", this, "Baseapp_importClientMessages");
        //KBEngine.Event.registerOut("Baseapp_importClientEntityDef", this, "Baseapp_importClientEntityDef");

        //// select-avatars(register by scripts)
        //KBEngine.Event.registerOut("onReqAvatarList", this, "onReqAvatarList");
        //KBEngine.Event.registerOut("onCreateAvatarResult", this, "onCreateAvatarResult");
        //KBEngine.Event.registerOut("onRemoveAvatar", this, "onRemoveAvatar");
    }
}
