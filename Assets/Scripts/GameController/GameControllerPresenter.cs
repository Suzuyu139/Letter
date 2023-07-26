using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.InputSystem;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer;

public class GameControllerPresenter : MonoBehaviour
{
    [SerializeField] InGameUIPresenter _inGameUIPresenter;

    GameControllerModel _model;
    GameControllerMakeStagePresenter _makeStagePresenter;

    GameInputs _gameInputs;

    public bool IsInitialized { get; private set; } = false;

    [Inject]
    void Construct(GameControllerModel model, GameControllerMakeStagePresenter makeStagePresenter)
    {
        _model = model;
        _makeStagePresenter = makeStagePresenter;
    }

    private void Start()
    {
        Initialize().Forget();
    }

    async UniTask Initialize()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _gameInputs = new GameInputs();

#if DEBUG
        _gameInputs.Debug.GameFinish.started += OnDebugGameFinish;
#endif

        await GameSceneManager.Instance.LoadSceneAsync(GameManager.Instance.StageName, GameSceneManager.LoadSceneType.Additive);
        await UniTask.WaitUntil(() => _makeStagePresenter.IsInitialized);
        var stage = GameObject.FindGameObjectWithTag(TagConstants.Stage).GetComponent<CreateStagePresenter>();
        await stage.CreateStage(_model.StageData);
        await UniTask.WaitUntil(() => _inGameUIPresenter.IsInitialized);

        _gameInputs.Enable();

        IsInitialized = true;

        GameSceneManager.Instance.SetIsLoadComplete(true);

        await UniTask.CompletedTask;
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
