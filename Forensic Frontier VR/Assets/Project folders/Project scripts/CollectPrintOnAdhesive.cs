using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class CollectPrintOnAdhesive : XRSocketInteractor
{
    public Image FingerPrint;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        bool Isrevealed = GameObject.FindObjectOfType<FingerPrintSampleCollection>().Printrevealed;
        Image fingerprintImage = args.interactableObject.transform.gameObject.GetComponentInChildren<Canvas>().transform.GetComponentInChildren<Image>();
        if (Isrevealed == true)
        {
            StartCoroutine(RevealFingerprints(fingerprintImage));
        }
        if(Isrevealed == false && GameObject.FindObjectOfType<FingerPrintSampleCollection>().isDamagedPrint)
        {
            StartCoroutine(RevealFingerprints(fingerprintImage));
            args.interactableObject.transform.gameObject.GetComponentInChildren<Canvas>().transform.GetComponentInChildren<Image>().sprite = null;
        }
    }

    IEnumerator RevealFingerprints(Image fingerprintImage)
    {
        fingerprintImage.sprite = FingerPrint.sprite;
        Color fingerprintColor = fingerprintImage.color;
        float alpha = fingerprintColor.a / 255f;

        while (alpha < 1f)
        {
            yield return new WaitForSeconds(0.1f);
            alpha += 0.1f;
            alpha = Mathf.Clamp01(alpha);
            fingerprintColor.a = alpha;
            fingerprintImage.color = fingerprintColor;
        }
        if(GameObject.FindObjectOfType<FingerPrintSampleCollection>().gameObject != null)
        {
            Destroy(GameObject.FindObjectOfType<FingerPrintSampleCollection>().gameObject);
        }
    }
}
