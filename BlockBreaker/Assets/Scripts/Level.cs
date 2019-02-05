using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // parameters
    private int numBlocks = 0;

    // cashed refernences
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void IncramentBlocks()
    {
        numBlocks++;
    }

    public void BlockDestroyed()
    {
        numBlocks--;

        if (numBlocks == 0)
        {
            // load the next level
            sceneLoader.LoadNextScene();
        }
    }
}
