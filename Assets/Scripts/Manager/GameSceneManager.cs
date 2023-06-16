using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    static public GameSceneManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        if(sceneName == string.Empty)
        {
            return;
        }

        SceneManager.LoadScene(sceneName, mode);
    }
}
