#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransportEvidence : MonoBehaviour
{
    public GameObject objectToCreate;
    public string savePath = "Assets/Project folders/Project assets/Evidences/Evidences collected";

    void Start()
    {
        
    }

   public void CreateAndSavePrefab()
    {
        if(SceneManager.GetActiveScene().name == "Crime scene Investigation")
        {
            GameObject newObject = objectToCreate;
            newObject.transform.SetParent(transform);

#if UNITY_EDITOR
            string prefabPath = savePath + newObject.name + ".prefab";
            PrefabUtility.SaveAsPrefabAsset(newObject, prefabPath);
            Debug.Log("Prefab saved at: " + prefabPath);
#endif

            Instantiate(newObject, Vector3.zero, Quaternion.identity);

        }
       
    }
}
