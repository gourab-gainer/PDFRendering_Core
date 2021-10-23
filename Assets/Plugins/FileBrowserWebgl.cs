using System.Runtime.InteropServices;
using UnityEngine;
using System;


public class FileBrowserWebgl : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter);

    [DllImport("__Internal")]
    private static extern void DownloadFile(string gameObjectName, string methodName, string filename, byte[] byteArray, int byteArraySize);

    [DllImport("__Internal")]
    private static extern void ShowIFrame(string iframeId);

    [DllImport("__Internal")]
    private static extern void HideIFrame(string iframeId);

    public static Action<string> OnFileChoose;
    public static Action OnFileSave;

    public static FileBrowserWebgl _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void ChooseFile(string filters)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        UploadFile(_instance.gameObject.name, "FileChooseCallback", filters);
#endif
    }

    public static void SaveFile(byte[] data, string filename)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        DownloadFile(_instance.gameObject.name, "FileSaveCallback", filename, data, data.Length);
#endif
    }

    public static void OnShowIFrame(string iframeId)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ShowIFrame(iframeId);
#endif
    }

    public static void OnHideIFrame(string iframeId)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        HideIFrame(iframeId);
#endif
    }

    public void FileChooseCallback(string fileUrl)
    {
        OnFileChoose?.Invoke(fileUrl);
    }

    public void FileSaveCallback()
    {
        OnFileSave?.Invoke();
    }
}
