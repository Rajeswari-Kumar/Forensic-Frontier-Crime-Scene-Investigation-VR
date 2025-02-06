using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;
using System.IO;
using UnityEngine.SceneManagement;
public class EvidenceDetails : XRSocketInteractor
{
    private List<string> evidenceCollectionList = new List<string>();
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
            Transform evidence = args.interactableObject.transform;
            string evidenceName = evidence.gameObject.name;
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            evidenceCollectionList.Add($"{evidenceName} - {timestamp}");
            UpdateEvidenceUI();

            evidence.parent = GameObject.FindGameObjectWithTag("Evidences collected").transform;
            evidence.transform.position = Vector3.zero;
            evidence.gameObject.SetActive(false);
    }

    private void UpdateEvidenceUI()
    {
        TMP_Text evidenceText = GameObject.FindGameObjectWithTag("Evidence timings record").GetComponent<TMP_Text>();
        evidenceText.text = "Collected Evidence:\n";

        foreach (string record in evidenceCollectionList)
        {
            evidenceText.text += record + "\n";
        }
    }
}