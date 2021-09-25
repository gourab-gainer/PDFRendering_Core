using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class PdfPrototype : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pdfName;
    [SerializeField] private Button pdfSelectButton;
    private PdfAssetFile pdfAsset;
    public static Action<PdfAssetFile> OnSelectedPdf;
    public void InitializePdf(PdfAssetFile _pdf)
    {
        pdfAsset = _pdf;
        pdfName.text = pdfAsset.PdfName;
        pdfSelectButton?.onClick.AddListener(() => 
        {
            Debug.Log("Pdf button clicked: " + pdfName.text);
            pdfSelectButton?.onClick.RemoveAllListeners();
            OnSelectedPdf.Invoke(pdfAsset);
        });
    }


}
