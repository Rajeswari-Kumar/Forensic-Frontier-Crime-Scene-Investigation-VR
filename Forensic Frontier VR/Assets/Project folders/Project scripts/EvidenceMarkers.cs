using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EvidenceMarkers : MonoBehaviour
{
    public int NumberOfMarker = 1;
    public TMP_Text NumberOnMarker;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Player"))
        {
            transform.GetComponent<XRGrabInteractable>().enabled = false;
            NumberOfMarker += 1;
            NumberOnMarker.text = NumberOfMarker.ToString();
        }
    }
}
