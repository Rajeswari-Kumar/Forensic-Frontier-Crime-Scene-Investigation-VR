using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FingerPrintSampleCollection : MonoBehaviour
{
    private Quaternion previousRotation;
    public ParticleSystem PowderApply;
    public bool Printrevealed = false;
    bool IsPowderApplying;
    int powderApplied = 0;
    public bool isDamagedPrint = false;
    void Start()
    {
        previousRotation = transform.rotation;
    }

    void Update()
    {
        if (transform.rotation != previousRotation)
        {
            PowderApply.Play();
            IsPowderApplying = true;
        }
        else
        {
            PowderApply.Stop();
            IsPowderApplying = false;
        }

        previousRotation = transform.rotation;
        if(powderApplied > 300)
        {
            isDamagedPrint = true;
        }

        if(!isDamagedPrint && powderApplied > 100)
        {
            Printrevealed = true;
        }
        else
        {
            Printrevealed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Printrevealed = false;
        isDamagedPrint = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(IsPowderApplying)
            powderApplied += 1;
        if (IsPowderApplying && other.gameObject.CompareTag("Fingerprint Trigger"))
        {
            Image fingerprintImage = other.gameObject.GetComponentInChildren<Canvas>().gameObject.GetComponentInChildren<Image>();
            if (fingerprintImage != null)
            {
                StartCoroutine(RevealFingerprints(fingerprintImage));
            }
            if (isDamagedPrint)
            {
                if(fingerprintImage != null)
                {
                    other.gameObject.GetComponentInChildren<Canvas>().gameObject.GetComponentInChildren<Image>().sprite = null;
                    other.gameObject.GetComponentInChildren<Canvas>().gameObject.GetComponentInChildren<Image>().color = Color.black;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IsPowderApplying = false;
    }
    IEnumerator RevealFingerprints(Image fingerprintImage)
    {
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
    }
}
