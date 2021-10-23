using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paroxe.PdfRenderer;
using TMPro;
using UnityEngine.UI;
using System.IO;
using Paroxe.PdfRenderer.Internal.Viewer;
using UnityEditor;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;
using UnityEngine.Networking;
using MiniJSON;


public class Manager : MonoBehaviour
{
    [SerializeField] public PDFViewer m_PDFViewer;
    //[SerializeField] private Button selectBtn;
    [SerializeField] private Button watchBtn;
    [SerializeField] private TextMeshProUGUI fileNameTxt;
    [SerializeField] private GameObject pageContainer;
    [SerializeField] private List<GameObject> pages;
    [SerializeField] private List<Texture> pagesTex;
    [SerializeField] private MeshRenderer pdfMesh;
    [SerializeField] private GameObject SelectionScreen;
    public Texture rawImageTexture;

    private string testString = "https://www.drivehq.com/file/DFPublishFile.aspx/FileID7948462622/Key6l9b6w9p0q57/SR1278_Real-TimeVideoCommunication_2.12sm.pdf";
    public List<PdfAssetFile> pdfs = new List<PdfAssetFile>();

    [SerializeField] private GameObject pdfPrototype;
    [SerializeField] private Transform pdfContainer;
    [SerializeField] private GameObject choosePdfCanvas;

    private void Awake()
    {
        SetupPdfs();
    }
    public void SetupPdfs()
    {
        for (int i = 0; i < pdfs.Count; i++)
        {
            GameObject go = Instantiate(pdfPrototype, pdfContainer);
            go.SetActive(true);
            PdfPrototype t_PickBadgeItem = go.GetComponent<PdfPrototype>();
            t_PickBadgeItem.InitializePdf(pdfs[i]);
        }
    }
    public void ClearItems()
    {
        int children = pdfContainer.childCount;
        Debug.Log("child count" + children);
        for (int i = 1; i < children; ++i)
        {
            Debug.Log("child game object name" + pdfContainer.GetChild(i).gameObject.name);
            Destroy(pdfContainer.GetChild(i).gameObject);
        }
    }

    private void OnEnable()
    {
        //m_PDFViewer.OnPageChanged += ShowPage;
        //selectBtn?.onClick.RemoveAllListeners();
        //string path = "";

        /* selectBtn?.onClick.AddListener(() =>
        {
            SelectionScreen.SetActive(false);
            choosePdfCanvas.SetActive(true);
            // FileUploads_WebTool.Instance.UploadImageFromPC(OnImagePathReceived, path, "others");
        }); */

        watchBtn?.onClick.AddListener(() =>
        {
            m_PDFViewer.LoadDocumentFromAsset(pdfs[0].Pdf);
            SelectionScreen.SetActive(false);
            choosePdfCanvas.SetActive(false);
        });

        PdfPrototype.OnSelectedPdf += HandlePdf;
        FileBrowserWebgl.OnFileChoose += OnFileChooseCallback;

        //fileNameTxt.gameObject.SetActive(false);
    }

    private void HandlePdf(PdfAssetFile assetFile)
    {
        m_PDFViewer.LoadDocumentFromAsset(assetFile.Pdf);
        choosePdfCanvas.SetActive(false);
    }


    //private void CheckVISIBLE()
    //{
    //    pages = new List<GameObject>();
    //    foreach (Transform tr in pageContainer.transform)
    //    {
    //        pages.Add(tr.gameObject);
    //        tr.gameObject.AddComponent<FetchTexture>();
    //      //  pagesTex.Add(tr.gameObject.GetComponent<RawImage>().texture);
    //       // tr.gameObject.AddComponent<GraphicRaycasterRaycasterExample>();
    //    }

    //}
    //private IEnumerator ShowPage()
    //{
    //    yield return new WaitForSeconds(2.0f);
    //    Debug.Log("Show page called");
    //    int index = m_PDFViewer.CurrentPage;
    //    Debug.LogError(index);
    //    //  Texture tex = pages[index].GetComponent<RawImage>().texture;
    //    rawImageTexture = pages[index].GetComponent<FetchTexture>().texture;
    //   // Texture2D tex2 = tex as Texture2D;
    //    pdfMesh.GetComponent<MeshRenderer>().material.mainTexture = TextureToTexture2D( rawImageTexture);

    //}
    //private Texture2D TextureToTexture2D(Texture texture)
    //{
    //    Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
    //    RenderTexture currentRT = RenderTexture.active;
    //    RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height, 32);
    //    Graphics.Blit(texture, renderTexture);

    //    RenderTexture.active = renderTexture;
    //    texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
    //    texture2D.Apply();

    //    RenderTexture.active = currentRT;
    //    RenderTexture.ReleaseTemporary(renderTexture);
    //    return texture2D;
    //}
    private void OnDisable()
    {
        //m_PDFViewer.OnPageChanged += ShowPage;
        PdfPrototype.OnSelectedPdf -= HandlePdf;
        FileBrowserWebgl.OnFileChoose -= OnFileChooseCallback;

    }

    public void OnChoosePDF()
    {
        choosePdfCanvas.SetActive(true);
    }

    // public void OnSelecPDF()
    // {
    //     // FileUploads_WebTool.Instance.UploadImageFromPC(OnImagePathReceived);
    // }

    void OnImagePathReceived(string path, string fileName, string extension)
    {

        Debug.Log(path + "On image path recieved: " + extension);
        m_PDFViewer.FileURL = path;
        //assetPath_MakeUpBG = path;
        //  ed.assetpath_MakeupBG = actualAssetPath + extension;
        //  assetPath_MakeUpBG = actualAssetPath + extension;
        //   StartCoroutine(Utils.Extract_AssignImage(path, AddMakeUPBGButton.GetComponent<RawImage>(), OnImageExtracted));
        //viewButton.interactable = true;
        //   if (FileUploads_WebTool.Instance.LoadingScreen.activeInHierarchy)
        //    FileUploads_WebTool.Instance.LoadingScreen.SetActive(false);
    }

    /* private void SelectPDF()
    {

        string baseDirectory = "";

        if (m_PDFViewer.FileSource == PDFViewer.FileSourceType.PersistentData)
            baseDirectory = Application.persistentDataPath;
        else if (m_PDFViewer.FileSource == PDFViewer.FileSourceType.StreamingAssets)
            baseDirectory = Application.streamingAssetsPath;
        else if (m_PDFViewer.FileSource == PDFViewer.FileSourceType.Resources)
            baseDirectory = "Assets/Resources";
        else if (m_PDFViewer.FileSource == PDFViewer.FileSourceType.FilePath)
        {
            string projectRoot = System.IO.Path.GetFullPath(System.IO.Path.Combine(Application.dataPath, ".."));
            projectRoot = projectRoot.Replace('\\', '/');

            baseDirectory = projectRoot;
        }

        if (!Directory.Exists(baseDirectory))
        {
            Directory.CreateDirectory(baseDirectory);
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif

        }

        string folder = "";
        string fileName = "";
        string filePath = "";
        bool usePersistentData = false;
        bool useStreamingAssets = false;
        bool useResources = false;
        bool useFilePath = false;

        if (Browse(baseDirectory, ref fileName, ref folder, ref filePath, ref useStreamingAssets, ref usePersistentData, ref useResources, ref useFilePath))
        {
            if (useStreamingAssets)
                m_PDFViewer.FileSource = PDFViewer.FileSourceType.StreamingAssets;
            else if (usePersistentData)
                m_PDFViewer.FileSource = PDFViewer.FileSourceType.PersistentData;
            else if (useResources)
                m_PDFViewer.FileSource = PDFViewer.FileSourceType.Resources;
            else if (useFilePath)
                m_PDFViewer.FileSource = PDFViewer.FileSourceType.FilePath;

            if (m_PDFViewer.FileSource != PDFViewer.FileSourceType.FilePath)
            {
                m_PDFViewer.Folder = folder;
                m_PDFViewer.FileName = fileName;
            }
            else
            {
                m_PDFViewer.FilePath = filePath;
            }
            //  fileNameTxt.gameObject.SetActive(true);
            // fileNameTxt.text = fileName;
        }
    } */
    /* private static bool Browse(string startPath, ref string filename, ref string folder, ref string filePath, ref bool isStreamingAsset, ref bool isPersistentData, ref bool isResourcesAsset, ref bool isFilePath)
    {
        bool result = false;
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Open File", Application.dataPath, "", true);
        // string path = UnityEditor.EditorUtility.OpenFilePanel("Browse video file", startPath, "*");
        string path = paths[0];
        Debug.Log("Path: " + path);
        Debug.Log("start: " + startPath);
        Debug.Log("filename: " + filename);
        foreach (string m in paths)
            Debug.Log("paths: " + m);



        if (!string.IsNullOrEmpty(path) && !path.EndsWith(".meta"))
        {
            string projectRoot = System.IO.Path.GetFullPath(System.IO.Path.Combine(Application.dataPath, ".."));
            projectRoot = projectRoot.Replace('\\', '/');

            if (path.StartsWith(projectRoot))
            {
                if (path.StartsWith(Application.streamingAssetsPath))
                {
                    path = path.Remove(0, Application.streamingAssetsPath.Length);
                    filename = System.IO.Path.GetFileName(path);
                    path = System.IO.Path.GetDirectoryName(path);
                    if (path.StartsWith(System.IO.Path.DirectorySeparatorChar.ToString()) || path.StartsWith(System.IO.Path.AltDirectorySeparatorChar.ToString()))
                    {
                        path = path.Remove(0, 1);
                    }
                    folder = path;

                    isPersistentData = false;
                    isStreamingAsset = true;
                    isResourcesAsset = false;
                    isFilePath = false;
                }
                else if (path.StartsWith(Application.persistentDataPath))
                {
                    path = path.Remove(0, Application.persistentDataPath.Length);
                    filename = System.IO.Path.GetFileName(path);
                    path = System.IO.Path.GetDirectoryName(path);
                    if (path.StartsWith(System.IO.Path.DirectorySeparatorChar.ToString()) || path.StartsWith(System.IO.Path.AltDirectorySeparatorChar.ToString()))
                    {
                        path = path.Remove(0, 1);
                    }
                    folder = path;

                    isPersistentData = true;
                    isStreamingAsset = false;
                    isResourcesAsset = false;
                    isFilePath = false;
                }
                else if (path.StartsWith(Application.dataPath + "/Resources"))
                {
                    path = path.Remove(0, (Application.dataPath + "/Resources").Length);
                    filename = System.IO.Path.GetFileName(path);
                    path = System.IO.Path.GetDirectoryName(path);
                    if (path.StartsWith(System.IO.Path.DirectorySeparatorChar.ToString()) || path.StartsWith(System.IO.Path.AltDirectorySeparatorChar.ToString()))
                    {
                        path = path.Remove(0, 1);
                    }
                    folder = path;

                    isPersistentData = false;
                    isStreamingAsset = false;
                    isResourcesAsset = true;
                    isFilePath = false;
                }
                else
                {
                    path = path.Remove(0, projectRoot.Length + 1);
                    filePath = path;

                    isPersistentData = false;
                    isStreamingAsset = false;
                    isResourcesAsset = false;
                    isFilePath = true;
                }
            }
            else
            {
                filePath = path;

                isPersistentData = false;
                isStreamingAsset = false;
                isResourcesAsset = false;
                isFilePath = true;
            }

            result = true;
        }
        return result;
    } */

    #region webgl file browser implementation
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
                Debug.Log("Error opening the file...");
            }
            else
            {
                byte[] buffer = webRequest.downloadHandler.data;
                Debug.Log(string.Format("{0}({1} bytes) file choosed", data["name"].ToString(), buffer.Length));
                m_PDFViewer.LoadDocumentFromBuffer(buffer);
                SelectionScreen.SetActive(false);
                choosePdfCanvas.SetActive(false);
            }
        }
    }

    public void OnFileBrowserDown()
    {
        FileBrowserWebgl.ChooseFile(".pdf");
    }
    #endregion
}

[System.Serializable]
public class PdfAssetFile
{
    public PDFAsset Pdf;
    public string PdfName;
}