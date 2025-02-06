using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EvidenceDisplay : MonoBehaviour
{
    [SerializeField] public GameObject playerEvidenceCollected;
    public List<GameObject> evidences = new List<GameObject>();
    int columns = 5; // Number of evidences per row
    float spacing = 1f; // Spacing between evidences

    void Start()
    {
        playerEvidenceCollected = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Project folders/Project assets/Evidences/Evidences collected/Evidences.prefab");

        foreach (Transform child in playerEvidenceCollected.transform)
        {
            evidences.Add(child.gameObject);
        }

        for (int i = 0; i < evidences.Count; i++)
        {
            GameObject evidenceGameobject = Instantiate(evidences[i], transform);

            // Calculate row and column positions
            int row = i / columns;
            int col = i % columns;

            // Set local position
            evidenceGameobject.transform.localPosition = new Vector3(col * spacing, 0, -row * spacing);
            evidenceGameobject.SetActive(true);
        }
    }
}
