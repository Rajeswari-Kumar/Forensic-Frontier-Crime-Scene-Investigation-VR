using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Scene_Navigation : MonoBehaviour
{
    public string previousSceneName;
    public string nextSceneName;
    public InputActionProperty PreviousScene;
    public InputActionProperty NextScene;

    private void Update()
    {
        if (PreviousScene.action.IsPressed())
        {
            if (previousSceneName != null)
                SceneManager.LoadScene(previousSceneName);
        }
        if (NextScene.action.IsPressed())
        {
            if (nextSceneName != null)
                SceneManager.LoadScene(nextSceneName);
        }
    }
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void quit()
    {
        Application.Quit();
    }
}
