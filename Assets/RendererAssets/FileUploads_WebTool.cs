using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;
using System;
using UnityEngine.Events;
//using com.ISG.SimpleBrowser;

public class FileUploads_WebTool : MonoBehaviour, IPointerDownHandler
{

    public static FileUploads_WebTool Instance;
    [SerializeField] GameObject ClickToUploadUI;
    public GameObject LoadingScreen;
    UnityAction<string, string, string> uploadFromPC_Callback;
    private Action loaderAction;
    

    private void Awake()
    {
        Instance = this;
    }

    public void UploadImageFromPC(UnityAction<string, string, string> action, string pathFileName, string a_FileType)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
Debug.Log("In uploading");
        uploadFromPC_Callback = action;
        Upload("ImageCallback",pathFileName,true,a_FileType,"OnImageSelected");
#else
        //FileUploads_SimpleBrowser.UploadFromPC(action, () => { });
#endif
    }

    public void UploadSoundFromPC(UnityAction<string, string, string> action, string pathFileName, string a_FileType)
    {

#if UNITY_WEBGL && !UNITY_EDITOR
        uploadFromPC_Callback = action;
        Upload("SoundCallback", pathFileName, true,a_FileType,"OnImageSelected");
#else
        //FileUploads_SimpleBrowser.UploadFromPC(action, () => { });
#endif
    }

    public void UploadTextToS3(string _name, string _data)
    {
        Application.ExternalCall("uploadS3", _data, _name);
    }
    //public void UploadImageToS3(string _name)
    //{
    //    if (rawBytes != null && rawBytes.Length > 0)
    //    {
    //        Logger.Log("Raw Image Base-64 Data: " + Convert.ToBase64String(rawBytes));
    //        Application.ExternalCall("uploadS3", Convert.ToBase64String(rawBytes), _name);
    //    }
    //}

    //public void UploadSoundToS3(string allBytes)
    //{
    //    Application.ExternalCall("uploadS3", allBytes, "dummySoundTesting.mp3");
    //}


    void Upload(string methodName, string filePathName, bool shouldUploadToS3Also, string fileType, string loaderMethod)
    {
        //ClickToUploadUI.SetActive(true);  //file dialog square box
          Application.ExternalCall("ImportLocalFile", methodName, filePathName, shouldUploadToS3Also, fileType, loaderMethod);
        //ImportLocalFile( methodName, filePathName, shouldUploadToS3Also, fileType, loaderMethod);
        //AdminUIManager.Instance.ShowBlockerPanel("Click Here to Upload & wait till Asset Upload in progress to server... ", false);
        // Application.ExternalEval(
        //    @"
        // document.addEventListener('click', function() {

        //     var fileuploader = document.getElementById('fileuploader');
        //     if (!fileuploader) {
        //         fileuploader = document.createElement('input');
        //         fileuploader.setAttribute('style','display:none;');
        //         fileuploader.setAttribute('type', 'file');
        //         fileuploader.setAttribute('id', 'fileuploader');
        //         fileuploader.setAttribute('class', 'focused');
        //         document.getElementsByTagName('body')[0].appendChild(fileuploader);

        //         fileuploader.onchange = function(e) {
        //         var files = e.target.files;
        //             for (var i = 0, f; f = files[i]; i++) {
        //                 window.alert(URL.createObjectURL(f));
        //                 SendMessage('" + gameObject.name + @"', '" + methodName + @"', URL.createObjectURL(f));
        //             }
        //         };
        //     }
        //     if (fileuploader.getAttribute('class') == 'focused') {
        //         fileuploader.setAttribute('class', '');
        //         fileuploader.click();
        //     }
        // });"
        //);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Application.ExternalCall("ClickedToImport");
        //        Application.ExternalEval(
        //            @"
        //var fileuploader = document.getElementById('fileuploader');
        //if (fileuploader) {
        //    fileuploader.setAttribute('class', 'focused');
        //}
        //");
    }

    public void OnImageSelected()
    {
        Debug.Log("Image Is Selected Wait for it get uploaded");
        if (LoadingScreen != null && !LoadingScreen.activeInHierarchy)
            LoadingScreen.SetActive(true);
    }

    public void ImageCallback(string receivedString)
    {
     //   AdminUIManager.Instance.HideBlockerPanel();
        string[] csv = receivedString.Split(',');

        Debug.Log(csv[0] + " :next: " + csv[1] + " next1: "+ csv[2]);
        uploadFromPC_Callback.Invoke(csv[0], csv[1], csv[2]);
        //text.text = fileUrl;
        //StartCoroutine(PreviewCoroutine(fileUrl));
    }

    public void SoundCallback(string receivedString)
    {
       // AdminUIManager.Instance.HideBlockerPanel();
        string[] csv = receivedString.Split(',');
        string fileName = csv[1].Substring(0, csv[1].LastIndexOf('.'));

        Debug.Log(csv[0] + " :next: " + csv[1]);
        uploadFromPC_Callback.Invoke(csv[0], fileName, "");
    }

    public void FileUploadStart()
    {
       // AdminUIManager.Instance.ShowBlockerPanel("Wait... File upload is in progress to server...", false);
    }

    public UnityEngine.UI.RawImage preview;
    byte[] rawBytes;

    IEnumerator PreviewCoroutine(string url)
    {
        var www = new WWW(url);
        yield return www;
        Debug.Log("WWW download complete");
        //rend.material.mainTexture = www.texture;
        Debug.Log("placed on Image");
        preview.texture = www.texture;

        rawBytes = www.bytes;
        //UIManager.Instance.UpdateStatus("Successfully imported Image from PC...");
        //preview.SetNativeSize();

    }
}
