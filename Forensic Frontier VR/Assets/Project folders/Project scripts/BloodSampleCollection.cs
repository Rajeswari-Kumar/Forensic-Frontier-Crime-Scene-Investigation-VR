using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSampleCollection : MonoBehaviour
{
    public Material BloodStainInswab;
    [SerializeField] public bool bloodIsDry;
    public bool BloodEvidenceDamaged = false;
    [SerializeField] public float wettingRange = 0;
    [SerializeField] public string BloodGroup;
    private void Start()
    {
        if (gameObject.CompareTag("Blood stain wet"))
            bloodIsDry = false;
        if (gameObject.CompareTag("Blood stain dry"))
            bloodIsDry = true;
    }
    private void OnTriggerStay(Collider other)
    {
       if(other.gameObject.CompareTag("Blood swab"))
       {
            if (wettingRange > 0 && wettingRange < 1.5)
                bloodIsDry = false;

            if (bloodIsDry == true)
                Debug.Log("stain not picked");

            if (BloodEvidenceDamaged == false && bloodIsDry == false)
            {
                bloodIsDry = false;
                other.gameObject.GetComponent<Renderer>().material = BloodStainInswab;
                other.gameObject.name = "Blood swab - " + BloodGroup;
            }
        }
    }
}
