using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tweezers_script : MonoBehaviour
{
    public Animator tweezers;
    public Transform AttachStrandHere;
    public InputActionProperty tweeze;
    bool HairPicked = false;
    float pick;
    public void Tweeze(bool a)
    {
        tweezers.SetBool("Tweeze", a);
        pick = tweeze.action.ReadValue<float>();
        if (pick > 0)
        {
            HairPicked = true;
            Debug.Log(HairPicked);
        }
        else
            HairPicked = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Hair strand"))
        {
            if (HairPicked)
            {
                other.gameObject.transform.position = AttachStrandHere.position;
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                Debug.Log("Move it slowly otherwise you might loose it");
            }
            else
                other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
