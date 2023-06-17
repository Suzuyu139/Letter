using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using Cysharp.Threading.Tasks;
using System;

public class GameSceneManager : MonoBehaviour
{
    static public GameSceneManager Instance { get; private set; }

    public enum LoadSceneType
    {
        Single,
        Additive,
    }

    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] float _animationSpeed = 1.0f;

    public bool IsSceneChange { get; private set; } = false;

    bool _isLoadComplete = false;

    private void Awake()
    {
        Instance = this;
    }

    public void LoadScene(string sceneName, LoadSceneType type = LoadSceneType.Single)
    {
        if(sceneName == string.Empty)
        {
            return;
        }

        SceneManager.LoadScene(sceneName, ConvertLoadSceneTypeToLoadSceneMode(type));
    }

    public async UniTask LoadSceneAsync(string sceneName, LoadSceneType type = LoadSceneType.Single)
    {
        if (sceneName == string.Empty)
        {
            return;
        }

        await SceneManager.LoadSceneAsync(sceneName, ConvertLoadSceneTypeToLoadSceneMode(type));
        await UniTask.CompletedTask;
    }

    public void LoadSceneChange(string sceneName)
    {
        if (sceneName == string.Empty)
        {
            return;
        }

        FadeLoadScene(sceneName, LoadSceneMode.Single).Forget();
    }

    async UniTask FadeLoadScene(string sceneName, LoadSceneMode mode)
    {
        IsSceneChange = true;

        await UniTask.WaitUntil(() =>
        {
            _canvasGroup.alpha += Time.deltaTime * _animationSpeed;

            return _canvasGroup.alpha >= 1.0f;
        });

        SceneManager.LoadScene(sceneName, mode);

        await UniTask.WaitUntil(() => _isLoadComplete);

        await UniTask.WaitUntil(() =>
        {
            _canvasGroup.alpha -= Time.deltaTime * _animationSpeed;

            return _canvasGroup.alpha <= 0.0f;
        });

        _isLoadComplete = false;
        IsSceneChange = false;
    }

    public void SetIsLoadComplete(bool isLoadComplete)
    {
        _isLoadComplete = isLoadComplete;
    }

    LoadSceneMode ConvertLoadSceneTypeToLoadSceneMode(LoadSceneType type)
    {
        LoadSceneMode mode = LoadSceneMode.Single;

        switch (type)
        {
            case LoadSceneType.Single:
                mode = LoadSceneMode.Single;
                break;

            case LoadSceneType.Additive:
                mode = LoadSceneMode.Additive;
                break;

            default:
                break;
        }

        return mode;
    }
}
