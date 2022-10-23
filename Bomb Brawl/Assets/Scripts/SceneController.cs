using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private int currentBuildIndex = 0;

    private void Awake() {
        
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene(currentBuildIndex + 1);
    }

    public void PreviousScene()
    {
        SceneManager.LoadScene(currentBuildIndex - 1);
    }

    public void LoadScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public int GetCurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
