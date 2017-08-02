using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoadingView : View
{
    public Slider _slider;
    public Text _tip;
    public Text _progress;
    protected string file = "";
    List<string> MessageList
    {
        get
        {
            return new List<string>()
            {
                NotiConst.UPDATE_MESSAGE,
                NotiConst.UPDATE_EXTRACT,
                NotiConst.UPDATE_DOWNLOAD,
                NotiConst.UPDATE_DOWNLOAD_FILE,
                NotiConst.UPDATE_PROGRESS,
            };
        }
    }
    IMessage data;
    Action task = null;

	void Awake()
    {
        _slider = transform.Find("LoadSlider").GetComponent<Slider>();
        _tip = transform.Find("Tip").GetComponent<Text>();
        _progress = transform.Find("Progress").GetComponent<Text>();
        RemoveMessage(this, MessageList);
        RegisterMessage(this, MessageList);
    }

    /// <summary>
    /// 处理View消息
    /// </summary>
    /// <param name="message"></param>
    public override void OnMessage(IMessage message)
    {
        string name = message.Name;
        object body = message.Body;
        data = message;
        switch (name)
        {
            case NotiConst.UPDATE_MESSAGE:      //更新消息
                task = UpdateMessage;
                break;
            case NotiConst.UPDATE_EXTRACT:      //更新解压
                task = UpdateExtract;
                break;
            case NotiConst.UPDATE_DOWNLOAD_FILE:     //更新下载
                task = UpdateDownload;
                break;
            case NotiConst.UPDATE_PROGRESS:     //更新下载进度
                task = UpdateProgress;
                break;
        }
    }

    void Update()
    {
        if(task != null)
        {
            task();
            task = null;
        }
    }

    public void UpdateMessage()
    {
        _tip.text = data.Body.ToString();
    }

    public void UpdateExtract()
    {
    }

    public void UpdateDownload()
    {
        Debug.LogError(data.Body.ToString());
        float value = float.Parse(data.Body.ToString());
        _slider.value = value;
        string.Format(LanguageManager.Instance.Get("UILoading_updatefile"), value.ToString());
    }

    public void UpdateProgress()
    {
         _progress.text = data.Body.ToString();
    }
}
