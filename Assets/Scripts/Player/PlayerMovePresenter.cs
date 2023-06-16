using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using VContainer;

public class PlayerMovePresenter : MonoBehaviour
{
    PlayerModel _model;
    PlayerMoveView _moveView;

    [Inject]
    void Construct(PlayerModel model, PlayerMoveView move)
    {
        _model = model;
        _moveView = move;
    }

    private void Start()
    {
        this.UpdateAsObservable().Subscribe(OnUpdate).AddTo(gameObject);
    }

    void OnUpdate(Unit unit)
    {
        var move = Vector3.zero;

        move.x = _model.InputData.Value.MoveHorizontal;
        move.z = _model.InputData.Value.MoveVertical;

        _moveView.Move(move * _model.MoveSpeed * Time.deltaTime);
    }
}
