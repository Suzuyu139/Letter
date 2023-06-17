using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using Cysharp.Threading.Tasks;

public class InGameUIPresenter : MonoBehaviour
{
    PlayerUIPresenter _playerUIPresenter;

    public bool IsInitialized { get; private set; } = false;

    [Inject]
    void Construct(PlayerUIPresenter playerUIPresenter)
    {
        _playerUIPresenter = playerUIPresenter;
    }

    void Start()
    {
        Initialize().Forget();
    }

    async UniTask Initialize()
    {
        await UniTask.WaitUntil(() => GameController.Instance != null && MasterDataManager.Instance != null && TextManager.Instance != null);

        await _playerUIPresenter.Initialize();

        IsInitialized = true;
    }
}
