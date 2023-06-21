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

    float _gravity = 0.0f;

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
        Move();
    }

    void Move()
    {
        var move = Vector3.zero;

        move.x = _model.InputData.Value.MoveHorizontal;
        move.z = _model.InputData.Value.MoveVertical;

        move *= _model.MoveSpeed * Time.deltaTime;

        _gravity += _model.Gravity * Time.deltaTime;
        move.y = _gravity;

        _moveView.Move(move, out _gravity);
    }
}
