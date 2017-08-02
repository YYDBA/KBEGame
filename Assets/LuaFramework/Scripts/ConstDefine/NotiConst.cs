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
    /// 更新消息
    /// </summary>
    public const string UPDATE_MESSAGE = "UpdateMessage";
    /// <summary>
    /// 更新解包
    /// </summary>          
    public const string UPDATE_EXTRACT = "UpdateExtract";
    /// <summary>
    /// 更新下载
    /// </summary>
    public const string UPDATE_DOWNLOAD = "UpdateDownload";
    /// <summary>
    /// 更新下载文件进度
    /// </summary>
    public const string UPDATE_DOWNLOAD_FILE = "UpdateDownloadFile";
    /// <summary>
    /// 更新进度
    /// </summary>
    public const string UPDATE_PROGRESS = "UpdateProgress";
    /// <summary>
    /// 网络通知
    /// </summary>
    public const string NETWORK_STATE = "NetworkState";
    #endregion
}
