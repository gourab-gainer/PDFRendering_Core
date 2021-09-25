using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Paroxe.PdfRenderer;
public class ObjectCreator : MonoBehaviour
{
    public GameObject obj;
    public Vector3 spawnPoint;
    [SerializeField] private PDFViewer pdf;

    public void BuildObject()
    {
        GameObject go = Instantiate(obj, spawnPoint, Quaternion.identity);
        //if(go != null)
       // pdf.pdfMesh.Add(go.transform.GetChild(0).GetComponent<MeshRenderer>());
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(ObjectCreator))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ObjectCreator myScript = (ObjectCreator)target;
        if (GUILayout.Button("Build Object"))
        {
            myScript.BuildObject();
        }
    }
}
#endif



