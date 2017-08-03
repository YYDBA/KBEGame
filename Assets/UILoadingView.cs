using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoadingView : View
{
    private Slider _slider;
    private Text _tip;
    private Text _progress;
    protected string file = "";
    Tweener tween;
    List<string> MessageList
    {
        get
        {
            return new List<string>()
            {
                NotiConst.UPDATE_MESSAGE,
                NotiConst.UPDATE_EXTRACT,
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
            case NotiConst.UPDATE_MESSAGE:
                task = UpdateMessage;
                break;
            case NotiConst.UPDATE_EXTRACT:
                task = UpdateExtract;
                break;
            case NotiConst.UPDATE_DOWNLOAD_FILE:
                task = UpdateDownloadFile;
                break;
            case NotiConst.UPDATE_PROGRESS:
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

    public void UpdateDownloadFile()
    {
        float value = float.Parse(data.Body.ToString());
        if(value == 0)
        {
            if(tween != null)
            {
                tween.Kill(false);
            }
            _slider.value = value;
        }
        else
        {
            tween = _slider.DOValue(value,1f);
        }
    }

    public void UpdateProgress()
    {
         _progress.text = data.Body.ToString();
    }

    public void Slider_OnChanged()
    {
        _tip.text = string.Format(LanguageManager.Instance.Get("UILoading_updatefile"), (_slider.value).ToString("#%"));
    }
}
