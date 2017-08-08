using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace LuaFramework {
    public class AppConst {
        public const bool DebugMode = false;                       //调试模式-用于内部测试
        public const bool UpdateMode = true;                       //更新模式-默认关闭 
        public const bool LuaByteMode = false;                     //Lua字节码模式-默认关闭 
        public const bool LuaBundleMode = true;                    //Lua代码AssetBundle模式

        public const int TimerInterval = 1;
        public const int GameFrameRate = 30;                        //游戏帧频

        public const string AppName = "LuaFramework";               //应用程序名称
        public const string LuaTempDir = "Lua/";                    //临时目录
        public const string AppPrefix = AppName + "_";              //应用程序前缀
        public const string ExtName = ".unity3d";                   //素材扩展名
        public const string AssetDir = "StreamingAssets";           //素材目录 
        public const string WebUrl = "http://127.0.0.1/StreamingAssets/";      //测试更新地址

        public static string UserId = string.Empty;                 //用户ID
        public static int SocketPort = 0;                           //Socket服务器端口
        public static string SocketAddress = string.Empty;          //Socket服务器地址

        public static string FrameworkRoot {
            get {
                return Application.dataPath + "/" + AppName;
            }
        }
    }
}