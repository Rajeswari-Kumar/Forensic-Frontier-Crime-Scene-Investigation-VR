using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class Equipments_spawn : MonoBehaviour
{

    public GameObject canvas;
    public InputActionProperty Canvas_spawn;
    bool isSetActive = false;
    public GameObject[] tools;
    public Transform SpawnTransform;
    public GameObject ray;
    private static Equipments_spawn Instance;

   
    private void Awake()
    {
        
    }

    void Update()
    {
        if(Canvas_spawn.action.IsPressed())
        {
            isSetActive = !isSetActive;
            canvas.SetActive(isSetActive);
            ray.GetComponent<XRRayInteractor>().enabled = isSetActive;
        }
    }

    public void spawnTools(string toolName)
    {
        if (tools.Length == 0)
        {
            Debug.LogError("No tools assigned to the array!");
            return;
        }

        foreach (GameObject tool in tools)
        {
            if (tool != null && tool.name == toolName)
            {
                Instantiate(tool, SpawnTransform.position , SpawnTransform.rotation);
                return; 
            }
        }

        Debug.LogWarning($"Tool with name {toolName} not found in tools array!");
    }



}
