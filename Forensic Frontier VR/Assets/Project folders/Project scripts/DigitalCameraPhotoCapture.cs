using System;
using System.IO;
using UnityEngine;
using TMPro;

public class DigitalCameraPhotoCapture : MonoBehaviour
{
    int photonumber = 1;
    public Camera cameraToCapture;
    public float lineLength = 5f;
    public LineRenderer lineRenderer;
    public GameObject AttachtoCam;
    public SpriteRenderer displaySpriteRenderer; // Assign a UI Image or SpriteRenderer in the Inspector

    void Start()
    {
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        lineRenderer.SetPosition(0, AttachtoCam.transform.position);
        lineRenderer.SetPosition(1, AttachtoCam.transform.position + AttachtoCam.transform.forward * lineLength);
    }

    public void PhotoCapture()
    {
        if (cameraToCapture == null)
        {
            Debug.LogError("No camera assigned to capture screenshots.");
            return;
        }

        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraToCapture.targetTexture = renderTexture;

        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        cameraToCapture.Render();

        RenderTexture.active = renderTexture;
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        cameraToCapture.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        // Convert Texture2D to Sprite
        Sprite capturedSprite = TextureToSprite(screenshot);
        if (displaySpriteRenderer != null)
        {
            displaySpriteRenderer.sprite = capturedSprite; // Display the captured sprite in UI
        }

        // Save as PNG
        string folderPath = "Assets/Project folders/Project assets/Evidences/Evidences collected/PhotoEvidence";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = Path.Combine(folderPath, "PhotoEvidence-" + photonumber++ + "-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".png");
        GameObject.FindGameObjectWithTag("Evidence timings record").GetComponent<TMP_Text>().text += "PhotoEvidence-" + (photonumber - 1) + "-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "\n";

        byte[] bytes = screenshot.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);

        Debug.Log("Screenshot saved to: " + filePath);

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    private Sprite TextureToSprite(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}
