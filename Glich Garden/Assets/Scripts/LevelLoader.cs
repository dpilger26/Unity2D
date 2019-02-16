using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // configurable parameters
    [SerializeField] float splashScreenWaitSeconds = 3f;

    // state parameters
    int currentSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            StartCoroutine(WaitAndLoadStartScene(splashScreenWaitSeconds));
        }
    }

    IEnumerator WaitAndLoadStartScene(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        LoadNextScene();

    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(++currentSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
