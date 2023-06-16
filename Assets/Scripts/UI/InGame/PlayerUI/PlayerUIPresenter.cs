using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;
using Cysharp.Threading.Tasks;

public class PlayerUIPresenter : MonoBehaviour
{
    PlayerUIModel _model;
    PlayerEquipListUIPresenter _equipListPresenter;

    Camera _mainCamera;

    [Inject]
    void Construct(PlayerUIModel model, PlayerEquipListUIPresenter equipListPresenter)
    {
        _model = model;
        _equipListPresenter = equipListPresenter;
    }

    public async UniTask Initialize()
    {
        await UniTask.WaitUntil(() => GameController.Instance.Player != null);

        _mainCamera = Camera.main;

        var canvases = this.GetComponentsInChildren<Canvas>();
        for(int i = 0; i < canvases.Length; i++)
        {
            var canvas = canvases[i];
            if(canvas.worldCamera != null)
            {
                continue;
            }

            canvas.worldCamera = _mainCamera;
        }

        await _equipListPresenter.Initialize();

        BindEvents();
    }

    void BindEvents()
    {
        _model.IsShowEquipListUI.Subscribe(x => _equipListPresenter.SetShowUI(x)).AddTo(gameObject);
    }
}
