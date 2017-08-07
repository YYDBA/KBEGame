using UnityEngine;
using System.Collections;

public class NotiConst
{
    #region Controller层消息通知
    /// <summary>
    /// 启动框架
    /// </summary>
    public const string START_UP = "StartUp";
    /// <summary>
    /// 派发信息
    /// </summary>
    public const string DISPATCH_MESSAGE = "DispatchMessage";
    #endregion


    #region View层消息通知
    /// <summary>
    /// 同步消息
    /// </summary>
    public const string NOTIFY_MESSAGE = "NotifyMessage";
    /// <summary>
    /// 解包开始
    /// </summary> 
    public const string UPDATE_EXTRACT_START = "UpdateExtractStart";
    /// <summary>
    /// 解包中
    /// </summary>          
    public const string UPDATE_EXTRACTING = "UpdateExtracting";
    /// <summary>
    /// 解包结束
    /// </summary>
    public const string UPDATE_EXTRACT_END = "UpdateExtractEnd";

    /// <summary>
    /// 下载开始
    /// </summary>
    public const string UPDATE_DOWNLOAD_START = "UpdateDownloadStart";
    /// <summary>
    /// 下载中
    /// </summary>
    public const string UPDATE_DOWNLOADING = "UpdateDownloading";
    /// <summary>
    /// 下载结束
    /// </summary>
    public const string UPDATE_DOWNLOAD_END = "UpdateDownloadEnd";
    /// <summary>
    /// 下载单个文件结束
    /// </summary>
    public const string UPDATE_DOWNLOAD = "UpdateDownloaded";
    /// <summary>
    /// 下载进度
    /// </summary>
    public const string UPDATE_PROGRESS = "UpdateProgress";
    /// <summary>
    /// 网络状态通知
    /// </summary>
    public const string NETWORK_STATE = "NetworkState";
    #endregion
}
