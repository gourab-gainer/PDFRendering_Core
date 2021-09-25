using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class FetchTexture : MonoBehaviour
{

    public Texture texture;
    private void Start()
    {
        texture = GetComponent<RawImage>().mainTexture;
    }
}