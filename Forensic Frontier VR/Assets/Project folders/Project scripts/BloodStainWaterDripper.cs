using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class BloodStainWaterDripper : MonoBehaviour
{
    [SerializeField] ParticleSystem Water;
    public bool Iswetting = false;
    public float WettingRange = 0;
    public Material bloodEraseMaterial;
    public bool BloodStainEvidenveDamaged = false;
    public bool BloodIsDry = true;
    public bool isBloodTriggered = false;
    public InputActionProperty spray;
    public void DripWater()
    {
        Water.Play();
        if(isBloodTriggered == true)
        {
            Iswetting = true;
            WettingRange += 0.1f;
        }
    }
    
    public void StopDrip()
    {
        Water.Stop();
        Iswetting = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blood stain dry"))
        {
            BloodStainEvidenveDamaged = false;
            other.gameObject.GetComponent<BloodSampleCollection>().BloodEvidenceDamaged = false;
            BloodIsDry = true;
        }
        if (other.gameObject.CompareTag("Blood stain wet"))
        {
            BloodIsDry = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Blood stain dry") || other.gameObject.CompareTag("Blood stain wet"))
            isBloodTriggered = true;
        if(Iswetting && (other.gameObject.CompareTag("Blood stain dry") || other.gameObject.CompareTag("Blood stain wet")) && !BloodStainEvidenveDamaged)
        {
            if (BloodIsDry == true)
            {
                if (WettingRange <= 0.3f)
                    StartCoroutine(WetTheBlood(other.gameObject.GetComponent<Image>(), 0.1f));
                else if (WettingRange > 0.6f)
                {
                    BloodStainEvidenveDamaged = true;
                    other.gameObject.GetComponent<BloodSampleCollection>().BloodEvidenceDamaged = true;
                    BloodstainErased(other.gameObject.GetComponent<Image>());
                }
                else if (WettingRange == 0.5f)
                {
                    BloodIsDry = false;
                    //other.gameObject.GetComponent<BloodSampleCollection>().bloodIsDry = BloodIsDry;
                }float a = spray.action.ReadValue<float>();
                if (spray.action.IsPressed()) ;
                {
                    other.gameObject.GetComponent<BloodSampleCollection>().wettingRange += 0.1f;
                }

            }
            if(BloodIsDry == false)
            {
                BloodStainEvidenveDamaged = true;
                other.gameObject.GetComponent<BloodSampleCollection>().BloodEvidenceDamaged = true;
                BloodstainErased(other.gameObject.GetComponent<Image>());
                if(spray.action.ReadValue<float>() > 0.1f)
                {
                    other.gameObject.GetComponent<BloodSampleCollection>().wettingRange += 0.1f;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Blood stain dry") || other.gameObject.CompareTag("Blood stain wet"))
        isBloodTriggered = false;
    }

    IEnumerator WetTheBlood(Image image , float amount)
    {
        Color BloodColor = image.color;
        float alpha = BloodColor.r / 255f;

        while (alpha < 1f)
        {
            yield return new WaitForSeconds(0.1f);
            alpha += amount;
            alpha = Mathf.Clamp01(alpha);
            BloodColor.r = alpha;
            image.color = BloodColor;
        }
    }

    void BloodstainErased(Image image)
    {
        image.material = bloodEraseMaterial;
    }
}
