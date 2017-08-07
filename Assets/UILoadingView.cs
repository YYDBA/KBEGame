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
    int updateLevel = 0;
    List<string> MessageList
    {
        get
        {
            return new List<string>()
            {
                NotiConst.NOTIFY_MESSAGE,
                NotiConst.UPDATE_EXTRACT_START,
                NotiConst.UPDATE_EXTRACTING,
                NotiConst.UPDATE_EXTRACT_END,
                NotiConst.UPDATE_DOWNLOAD_START,
                NotiConst.UPDATE_DOWNLOADING,
                NotiConst.UPDATE_DOWNLOAD_END,
                NotiConst.UPDATE_PROGRESS,
            };
        }
    }
    IMessage data;
    Action task = null;
    bool bUpdate = false;
    enum ViewLayout
    {
        /// <summary>
        /// 初始化时更新
        /// </summary>
        UPDATE,
    }

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
            case NotiConst.NOTIFY_MESSAGE:
                task = NotifyMessage;
                break;
            case NotiConst.UPDATE_EXTRACT_START:
                task = UpdateExtractStart;
                break;
            case NotiConst.UPDATE_EXTRACTING:
                task = UpdateExtract;
                break;
            case NotiConst.UPDATE_EXTRACT_END:
                task = UpdateExtractEnd;
                break;
            case NotiConst.UPDATE_DOWNLOAD_START:
                task = UpdateDownloadStart;
                break;
            case NotiConst.UPDATE_DOWNLOADING:
                task = UpdateDownloading;
                break;
            case NotiConst.UPDATE_DOWNLOAD_END:
                task = UpdateDownloadEnd;
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

    void NotifyMessage()
    {
        _tip.text = data.Body.ToString();
    }

    #region 解压
    void UpdateExtractStart()
    {
        updateLevel = 1;
        _tip.text = data.Body.ToString();
    }

    void UpdateExtract()
    {
        float value = float.Parse(data.Body.ToString());
        SetProgress(value);
    }

    void UpdateExtractEnd()
    {
        SetProgress(1);
    }

    #endregion

    #region 解压
    void UpdateDownloadStart()
    {
        updateLevel = 2;
        _tip.text = data.Body.ToString();
    }

    void UpdateDownloading()
    {
        float value = float.Parse(data.Body.ToString());
        SetProgress(value);
    }

    void UpdateDownloadEnd()
    {
        _tip.text = data.Body.ToString();
        SetProgress(1);
        updateLevel = 0;
    }
    #endregion

    public void UpdateProgress()
    {
         _progress.text = data.Body.ToString();
    }

    public void Slider_OnChanged()
    {
        if(updateLevel == 1)
        {
            _tip.text = string.Format(LanguageManager.Instance.Get("UILoading_extractfile"), (_slider.value).ToString("#%"));
        }
        else
        {
            _tip.text = string.Format(LanguageManager.Instance.Get("UILoading_updatefile"), (_slider.value).ToString("#%"));
        }
    }

    void SetProgress(float value)
    {
        if (value == 0)
        {
            _progress.text = "";
            if (tween != null)
            {
                tween.Kill(false);
            }
            _slider.value = value;
        }
        else
        {
            tween = _slider.DOValue(value, 1f).OnComplete(() =>
            {
                if (value == 1)
                {
                    _progress.text = "";
                }
            });
        }
    }
}
