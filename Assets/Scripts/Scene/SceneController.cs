using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using Cysharp.Threading.Tasks;

public class SceneController : MonoBehaviour
{
    [SerializeField] Object[] _loadScene;

    public bool IsInitialized { get; private set; } = false;

    private void Awake()
    {
        LoadScene().Forget();
    }

    async UniTask LoadScene()
    {
        for(int i = 0; i < _loadScene.Length; i++)
        {
            await SceneManager.LoadSceneAsync(_loadScene[i].name, LoadSceneMode.Additive);
        }

        IsInitialized = true;
    }
}
