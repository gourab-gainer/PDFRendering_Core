using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

#if UNITY_EDITOR
public class RenedereController : MonoBehaviour
{
    [Range(0.1f, 10)]
    public float ScaleValue;
    [Range(0, 360)]
    public int RotateValue;
    public void ReScaleObject()
    {
        gameObject.transform.localScale = new Vector3(ScaleValue, ScaleValue, ScaleValue);
    }
    public void RotateObject()
    {
        Vector3 v = new Vector3(0, RotateValue, 0);
        transform.localRotation = Quaternion.Euler(v);
    }
}
[CustomEditor(typeof(RenedereController))]
public class ObjectResizeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RenedereController myScript = (RenedereController)target;
        if (GUILayout.Button("Rescale"))
        {
            myScript.ReScaleObject();
        }
        if (GUILayout.Button("Rotate"))
        {
            myScript.RotateObject();
        }
    }
}
#endif