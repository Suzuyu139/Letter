using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameControllerPresenter : MonoBehaviour
{
    GameInputs _gameInputs;

    public bool IsInitialized { get; private set; } = false;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _gameInputs = new GameInputs();

#if DEBUG
        _gameInputs.Debug.GameFinish.started += OnDebugGameFinish;
#endif

        GameSceneManager.Instance.LoadScene(GameManager.Instance.StageName, LoadSceneMode.Additive);

        _gameInputs.Enable();

        IsInitialized = true;
    }

#if DEBUG
    void OnDebugGameFinish(InputAction.CallbackContext context)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
        Application.Quit();//ゲームプレイ終了
#endif
    }
#endif

    private void OnDestroy()
    {
#if DEBUG
        _gameInputs.Debug.GameFinish.started -= OnDebugGameFinish;
#endif

        _gameInputs.Disable();
    }
}
