using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using MiniJSON;


public class UIController : MonoBehaviour
{
    public Text debugText;

    private bool takeScreenShot = false;

    private void OnEnable()
    {
        FileBrowserWebgl.OnFileChoose += OnFileChooseCallback;
        FileBrowserWebgl.OnFileSave += OnFileSaveCallback;
    }

    private void OnDisable()
    {
        FileBrowserWebgl.OnFileChoose -= OnFileChooseCallback;
        FileBrowserWebgl.OnFileSave -= OnFileSaveCallback;
    }

    private void OnFileSaveCallback()
    {
        debugText.text = "File saved sucessfully...";
    }

    private void OnFileChooseCallback(string callbackData)
    {
        Dictionary<string, object> value = Json.Deserialize(callbackData) as Dictionary<string, object>;

        //value contains "name" and "url" key
        StartCoroutine(MoveFile(value));
    }

    IEnumerator MoveFile(Dictionary<string, object> data)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(data["url"].ToString()))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                debugText.text = "Error reading file";
            }
            else
            {
                byte[] bytes = webRequest.downloadHandler.data;
                debugText.text = string.Format("{0}({1} bytes) file choosed", data["name"].ToString(), bytes.Length);
                //use this data to upload
            }
        }
    }

    public void OnFileBrowserDown()
    {
        FileBrowserWebgl.ChooseFile(".jpg, .png, .bmp"); //or "image/*"
    }

    public void SaveImage()
    {
        takeScreenShot = true;
    }

    void LateUpdate()
    {
        if (!takeScreenShot)
            return;

        RenderTexture rt = new RenderTexture(480, 480, 24);
        Camera.main.targetTexture = rt;
        Texture2D screenShot = new Texture2D(480, 480, TextureFormat.RGB24, false);
        Camera.main.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, 480, 480), 0, 0);
        Camera.main.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();

        Debug.Log(bytes.Length);

        FileBrowserWebgl.SaveFile(bytes, "webgl_save.png");

        takeScreenShot = false;
    }
}
