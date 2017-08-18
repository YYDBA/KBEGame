using KBEngine;
using LuaInterface;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Client {
    public class LoginMoudle
    {
        public static LoginMoudle Instance;
        private Dictionary<UInt64, Dictionary<string, object>> ui_avatarList = null;
        public int ui_state = 0;
        private string stringAccount = "";
        private string stringPasswd = "";
        private string stringAvatarName = "";
        private bool startCreateAvatar = false;
        private UInt64 selAvatarDBID = 0;
        public bool showReliveGUI = false;
        private string labelMsg = "";
        protected byte[] bytes = System.Text.Encoding.UTF8.GetBytes("kbengine_unity3d_demo");
        bool startRelogin = false;
        private LuaFunction func;
        void RegisterEvent()
        {
            KBEngine.Event.registerOut("onKicked", this, "onKicked");
            KBEngine.Event.registerOut("onDisconnected", this, "onDisconnected");
            KBEngine.Event.registerOut("onConnectionState", this, "onConnectionState");

            // login
            KBEngine.Event.registerOut("onCreateAccountResult", this, "onCreateAccountResult");
            KBEngine.Event.registerOut("onLoginFailed", this, "onLoginFailed");
            KBEngine.Event.registerOut("onVersionNotMatch", this, "onVersionNotMatch");
            KBEngine.Event.registerOut("onScriptVersionNotMatch", this, "onScriptVersionNotMatch");
            KBEngine.Event.registerOut("onLoginBaseappFailed", this, "onLoginBaseappFailed");
            KBEngine.Event.registerOut("onLoginSuccessfully", this, "onLoginSuccessfully");
            KBEngine.Event.registerOut("onReloginBaseappFailed", this, "onReloginBaseappFailed");
            KBEngine.Event.registerOut("onReloginBaseappSuccessfully", this, "onReloginBaseappSuccessfully");
            KBEngine.Event.registerOut("onLoginBaseapp", this, "onLoginBaseapp");
            KBEngine.Event.registerOut("Loginapp_importClientMessages", this, "Loginapp_importClientMessages");
            KBEngine.Event.registerOut("Baseapp_importClientMessages", this, "Baseapp_importClientMessages");
            KBEngine.Event.registerOut("Baseapp_importClientEntityDef", this, "Baseapp_importClientEntityDef");

            // select-avatars(register by scripts)
            KBEngine.Event.registerOut("onReqAvatarList", this, "onReqAvatarList");
            KBEngine.Event.registerOut("onCreateAvatarResult", this, "onCreateAvatarResult");
            KBEngine.Event.registerOut("onRemoveAvatar", this, "onRemoveAvatar");
        }

        public void Init(LuaFunction _func)
        {
            this.func = _func;
            RegisterEvent();
        }

        void OnDestroy()
        {
            KBEngine.Event.deregisterOut(this);
        }

        public void info(string s)
        {
            labelMsg = s;
            if(func != null)
            {
                func.Call(s);
            }
        }

        public void err(string s)
        {
            labelMsg = s;
            if (func != null)
            {
                func.Call(s);
            }
        }

        public void Login(string account, string pwd)
        {
#if DEBUG
            SceneManager.LoadSceneAsync(Const.SCENE.BATTLE);
#else
            info("connect to server...(连接到服务端...)");
            KBEngine.Event.fireIn("login", account, pwd, bytes);
#endif
        }

        public void createAccount(string account, string pwd)
        {
            info("connect to server...(连接到服务端...)");
            KBEngine.Event.fireIn("createAccount", account, pwd, bytes);
        }

        public void onCreateAccountResult(UInt16 retcode, byte[] datas)
        {
            if (retcode != 0)
            {
                err("createAccount is error(注册账号错误)! err=" + KBEngineApp.app.serverErr(retcode));
                return;
            }

            if (KBEngineApp.validEmail(stringAccount))
            {
                info("createAccount is successfully, Please activate your Email!(注册账号成功，请激活Email!)");
            }
            else
            {
                info("createAccount is successfully!(注册账号成功!)");
            }
        }

        public void onConnectionState(bool success)
        {
            if (!success)
                err("connect(" + KBEngineApp.app.getInitArgs().ip + ":" + KBEngineApp.app.getInitArgs().port + ") is error! (连接错误)");
            else
                info("connect successfully, please wait...(连接成功，请等候...)");
        }

        public void onLoginFailed(UInt16 failedcode)
        {
            if (failedcode == 20)
            {
                err("login is failed(登陆失败), err=" + KBEngineApp.app.serverErr(failedcode) + ", " + System.Text.Encoding.ASCII.GetString(KBEngineApp.app.serverdatas()));
            }
            else
            {
                err("login is failed(登陆失败), err=" + KBEngineApp.app.serverErr(failedcode));
            }
        }

        public void onVersionNotMatch(string verInfo, string serVerInfo)
        {
            err("");
        }

        public void onScriptVersionNotMatch(string verInfo, string serVerInfo)
        {
            err("");
        }

        public void onLoginBaseappFailed(UInt16 failedcode)
        {
            err("loginBaseapp is failed(登陆网关失败), err=" + KBEngineApp.app.serverErr(failedcode));
        }

        public void onLoginBaseapp()
        {
            info("connect to loginBaseapp, please wait...(连接到网关， 请稍后...)");
        }

        public void onReloginBaseappFailed(UInt16 failedcode)
        {
            err("relogin is failed(重连网关失败), err=" + KBEngineApp.app.serverErr(failedcode));
            startRelogin = false;
        }

        public void onReloginBaseappSuccessfully()
        {
            info("relogin is successfully!(重连成功!)");
            startRelogin = false;
        }

        public void onLoginSuccessfully(UInt64 rndUUID, Int32 eid, Account accountEntity)
        {
            info("login is successfully!(登陆成功!)");
            ui_state = 1;

            //Application.LoadLevel("selavatars");
        }

        public void onKicked(UInt16 failedcode)
        {
            err("kick, disconnect!, reason=" + KBEngineApp.app.serverErr(failedcode));
            //Application.LoadLevel("login");
            ui_state = 0;
        }

        public void Loginapp_importClientMessages()
        {
            info("Loginapp_importClientMessages ...");
        }

        public void Baseapp_importClientMessages()
        {
            info("Baseapp_importClientMessages ...");
        }

        public void Baseapp_importClientEntityDef()
        {
            info("importClientEntityDef ...");
        }

        public void onReqAvatarList(Dictionary<UInt64, Dictionary<string, object>> avatarList)
        {
            ui_avatarList = avatarList;
        }

        public void onCreateAvatarResult(Byte retcode, object info, Dictionary<UInt64, Dictionary<string, object>> avatarList)
        {
            if (retcode != 0)
            {
                err("Error creating avatar, errcode=" + retcode);
                return;
            }

            onReqAvatarList(avatarList);
        }

        public void onRemoveAvatar(UInt64 dbid, Dictionary<UInt64, Dictionary<string, object>> avatarList)
        {
            if (dbid == 0)
            {
                err("Delete the avatar error!(删除角色错误!)");
                return;
            }

            onReqAvatarList(avatarList);
        }

        public void onDisconnected()
        {
            err("disconnect! will try to reconnect...(你已掉线，尝试重连中!)");
            startRelogin = true;
            //Invoke("onReloginBaseappTimer", 1.0f);
        }

        public void onReloginBaseappTimer()
        {
            if (ui_state == 0)
            {
                err("disconnect! (你已掉线!)");
                return;
            }

            KBEngineApp.app.reloginBaseapp();

            if (startRelogin)
            {
                //Invoke("onReloginBaseappTimer", 3.0f);
            }
        }
    }
}

