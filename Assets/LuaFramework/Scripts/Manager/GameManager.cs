using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using System.Reflection;
using System.IO;
using UnityEngine.SceneManagement;

namespace LuaFramework {
    public class GameManager : Manager {
        private List<string> downloadFiles = new List<string>();

        void Start()
        {
            UIManager.Instance.Push(Const.UI.UILOADING);
            Init();
        }

        void Init()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = AppConst.GameFrameRate;
            if(CheckNetWork())
            {
                facade.SendMessageCommand(NotiConst.NOTIFY_MESSAGE, LanguageManager.Instance.Get("UILoading_vaildversion"));
                CheckExtractResource();
            }
        }

        public bool CheckNetWork()
        {
            facade.SendMessageCommand(NotiConst.NOTIFY_MESSAGE, LanguageManager.Instance.Get("UILoading_connect"));
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                UIToastView.Instance.Show(LanguageManager.Instance.Get("NetWork_noteachable"));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void CheckExtractResource() {
            bool isExists = Directory.Exists(Util.DataPath) &&
              Directory.Exists(Util.DataPath + "lua/") && File.Exists(Util.DataPath + "files.txt");
            if (isExists || AppConst.DebugMode) {
                StartCoroutine(OnUpdateResource());
                return;
            }
            StartCoroutine(OnExtractResource());    //启动释放协成 
        }

        IEnumerator OnExtractResource() {
            string dataPath = Util.DataPath;  //数据目录
            string resPath = Util.AppContentPath(); //游戏包资源目录

            if (Directory.Exists(dataPath)) Directory.Delete(dataPath, true);
            Directory.CreateDirectory(dataPath);

            string infile = resPath + "files.txt";
            string outfile = dataPath + "files.txt";
            if (File.Exists(outfile)) File.Delete(outfile);

            facade.SendMessageCommand(NotiConst.UPDATE_EXTRACT_START, LanguageManager.Instance.Get("UILoading_extractres"));
            yield return new WaitForSeconds(1f);
            facade.SendMessageCommand(NotiConst.UPDATE_EXTRACTING, 0);
            if (Application.platform == RuntimePlatform.Android) {
                WWW www = new WWW(infile);
                yield return www;

                if (www.isDone) {
                    File.WriteAllBytes(outfile, www.bytes);
                }
                yield return 0;
            } else File.Copy(infile, outfile, true);
            yield return new WaitForEndOfFrame();

            //释放所有文件到数据目录
            string[] files = File.ReadAllLines(outfile);
            int total = files.Length;
            for (int i = 0;i<total;++i) {
                var file = files[i];
                string[] fs = file.Split('|');
                infile = resPath + fs[0];  //
                outfile = dataPath + fs[0];

                string dir = Path.GetDirectoryName(outfile);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                if (Application.platform == RuntimePlatform.Android) {
                    WWW www = new WWW(infile);
                    yield return www;

                    if (www.isDone) {
                        File.WriteAllBytes(outfile, www.bytes);
                    }
                    yield return 0;
                } else {
                    if (File.Exists(outfile)) {
                        File.Delete(outfile);
                    }
                    File.Copy(infile, outfile, true);
                }
                yield return new WaitForEndOfFrame();
                facade.SendMessageCommand(NotiConst.UPDATE_EXTRACTING, (i + 1) / (float)total);
            }
            yield return new WaitForSeconds(2f);
            facade.SendMessageCommand(NotiConst.UPDATE_EXTRACT_END, "");
            //释放完成，开始启动更新资源
            StartCoroutine(OnUpdateResource());
        }

        /// <summary>
        /// 启动更新下载，这里只是个思路演示，此处可启动线程下载更新
        /// </summary>
        IEnumerator OnUpdateResource() {
            if (!AppConst.UpdateMode) {
                OnResourceInited();
                yield break;
            }
            yield return new WaitForEndOfFrame();
            facade.SendMessageCommand(NotiConst.UPDATE_DOWNLOAD_START, LanguageManager.Instance.Get("UILoading_readyupdate"));
            yield return new WaitForSeconds(1f);
            facade.SendMessageCommand(NotiConst.UPDATE_DOWNLOADING, 0);
            string dataPath = Util.DataPath;  //数据目录
            string url = AppConst.WebUrl;
            string random = DateTime.Now.ToString("yyyymmddhhmmss");
            string listUrl = url + "files.txt?v=" + random;
#if DEBUG
            facade.SendMessageCommand(NotiConst.UPDATE_DOWNLOADING, 1);
#else
            WWW www = new WWW(listUrl);
            yield return www;
            if (www.error != null) {
                OnUpdateFailed(string.Empty);
                yield break;
            }
            if (!Directory.Exists(dataPath)) {
                Directory.CreateDirectory(dataPath);
            }
            File.WriteAllBytes(dataPath + "files.txt", www.bytes);
            string filesText = www.text;
            string[] files = filesText.Split(new char[] { '\n'}, StringSplitOptions.RemoveEmptyEntries);
            int total = files.Length;
            for (int i = 0; i < total; i++) {
                if (string.IsNullOrEmpty(files[i])) continue;
                string[] keyValue = files[i].Split('|');
                string f = keyValue[0];
                string localfile = (dataPath + f).Trim();
                string path = Path.GetDirectoryName(localfile);
                if (!Directory.Exists(path)) {
                    Directory.CreateDirectory(path);
                }
                string fileUrl = url + f + "?v=" + random;
                bool canUpdate = !File.Exists(localfile);
                if (!canUpdate) {
                    string remoteMd5 = keyValue[1].Trim();
                    string localMd5 = Util.md5file(localfile);
                    canUpdate = !remoteMd5.Equals(localMd5);
                    if (canUpdate) File.Delete(localfile);
                }
                facade.SendMessageCommand(NotiConst.UPDATE_DOWNLOADING, (i+1) / (float)total);
                if (canUpdate) {   //本地缺少文件
                    BeginDownload(fileUrl, localfile);//这里都是资源文件，用线程下载
                    while (!(IsDownOK(localfile))) { yield return new WaitForEndOfFrame(); }
                }
            }
#endif
            yield return new WaitForSeconds(2f);
            facade.SendMessageCommand(NotiConst.UPDATE_DOWNLOAD_END, LanguageManager.Instance.Get("UILoading_updateover"));
            yield return new WaitForSeconds(2f);
            OnResourceInited();
        }

        void OnUpdateFailed(string file) {
            facade.SendMessageCommand(NotiConst.NOTIFY_MESSAGE, LanguageManager.Instance.Get("UILoading_updatefail"));
        }

        /// <summary>
        /// 是否下载完成
        /// </summary>
        bool IsDownOK(string file) {
            return downloadFiles.Contains(file);
        }

        /// <summary>
        /// 线程下载
        /// </summary>
        void BeginDownload(string url, string file) {     //线程下载
            object[] param = new object[2] { url, file };

            ThreadEvent ev = new ThreadEvent();
            ev.Key = NotiConst.UPDATE_DOWNLOAD;
            ev.evParams.AddRange(param);
            ThreadManager.AddEvent(ev, OnThreadCompleted);   //线程下载
        }

        /// <summary>
        /// 线程完成
        /// </summary>
        /// <param name="data"></param>
        void OnThreadCompleted(NotiData data) {
            switch (data.evName) {
                case NotiConst.UPDATE_EXTRACTING:  //解压一个完成
                //
                break;
                case NotiConst.UPDATE_DOWNLOAD: //下载一个完成
                downloadFiles.Add(data.evParam.ToString());
                break;
            }
        }

        /// <summary>
        /// 资源初始化结束
        /// </summary>
        public void OnResourceInited() {
#if ASYNC_MODE
            ResManager.Initialize(AppConst.AssetDir, delegate() {
                Debug.Log("Initialize OK!!!");
                this.OnInitialize();
            });
#else
            ResManager.Initialize();
            this.OnInitialize();
#endif
        }

        void OnInitialize() {
            AsyncOperation async = SceneManager.LoadSceneAsync(Const.SCENE.LOGIN);
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            LuaManager.InitStart();
            Debug.LogError("###########");
            LuaManager.DoFile("Logic/Game");
            Util.CallMethod("Game", "OnInitOK");
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.name.Equals(Const.SCENE.LOGIN))
            {
                UILoadingView _view = UIManager.Instance.GetView<UILoadingView>(Const.UI.UILOADING);
                if(_view)
                {
                    UIManager.Instance.Pop();
                }
                RootManager.Instance.kbeMain.SetActive(true);
                
            }
        }

        /// <summary>
        /// 当从池子里面获取时
        /// </summary>
        /// <param name="obj"></param>
        void OnPoolGetElement(TestObjectClass obj) {
            Debug.Log("OnPoolGetElement--->>>" + obj);
        }

        /// <summary>
        /// 当放回池子里面时
        /// </summary>
        /// <param name="obj"></param>
        void OnPoolPushElement(TestObjectClass obj) {
            Debug.Log("OnPoolPushElement--->>>" + obj);
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        void OnDestroy() {
            if (NetManager != null) {
                NetManager.Unload();
            }
            if (LuaManager != null) {
                LuaManager.Close();
            }
            Debug.Log("~GameManager was destroyed");
        }
    }
}