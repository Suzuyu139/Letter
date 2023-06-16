using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;

public class TitlePresenter : MonoBehaviour
{
    TitleModel _model;
    TitleView _view;

    [Inject]
    void Construct(TitleModel model, TitleView view)
    {
        _model = model;
        _view = view;
    }

    private void Start()
    {
        BindEvents();
    }

    void BindEvents()
    {
        _view.StartButtonObservable.Subscribe(OnStartButton).AddTo(gameObject);
    }

    void OnStartButton(Unit unit)
    {
        GameManager.Instance.SetStage("Stage1");
        GameSceneManager.Instance.LoadScene("Game");
    }
}
