using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UnityEngine.InputSystem;
using Cysharp.Threading.Tasks;
using UniRx;

public class PlayerInputPresenter : MonoBehaviour
{
    PlayerModel _model;

    GameInputData _inputData = new GameInputData();

    [Inject]
    void Construct(PlayerModel model)
    {
        _model = model;
    }

    private void Start()
    {
        _model.IsControl.SkipLatestValueOnSubscribe().Subscribe(OnIsControl).AddTo(gameObject);
    }

    void OnIsControl(bool isControl)
    {
        if(isControl)
        {
            return;
        }

        _inputData.SetMoveVertical(0.0f);
        _inputData.SetMoveHorizontal(0.0f);
        _inputData.SetViewpointX(0.0f);
        _inputData.SetViewpointY(0.0f);
        _inputData.SetIsAction(false);
        _inputData.SetEquipListUI(false);

        _model.SetGameInputData(_inputData);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!_model.IsControl.Value ||
            context.action.phase == InputActionPhase.Started)
        {
            return;
        }

        var value = context.ReadValue<Vector2>();

        _inputData.SetMoveVertical(value.y);
        _inputData.SetMoveHorizontal(value.x);

        _model.SetGameInputData(_inputData);
    }

    public void OnViewpoint(InputAction.CallbackContext context)
    {
        if (!_model.IsControl.Value ||
            context.action.phase == InputActionPhase.Started)
        {
            return;
        }

        var value = context.ReadValue<Vector2>();

        _inputData.SetViewpointX(value.x);
        _inputData.SetViewpointY(value.y);

        _model.SetGameInputData(_inputData);
    }

    public void OnIsAction(InputAction.CallbackContext context)
    {
        if (!_model.IsControl.Value ||
            (context.action.phase != InputActionPhase.Started && context.action.phase != InputActionPhase.Canceled))
        {
            return;
        }

        var value = context.ReadValueAsButton();

        _inputData.SetIsAction(value);

        _model.SetGameInputData(_inputData);
    }

    public void OnIsAttack(InputAction.CallbackContext context)
    {
        if (!_model.IsControl.Value ||
            (context.action.phase != InputActionPhase.Started && context.action.phase != InputActionPhase.Canceled))
        {
            return;
        }

        var value = context.ReadValueAsButton();

        _inputData.SetIsAttack(value);

        _model.SetGameInputData(_inputData);
    }

    public void OnIsEquipListUI(InputAction.CallbackContext context)
    {
        if (context.action.phase != InputActionPhase.Started && context.action.phase != InputActionPhase.Canceled)
        {
            return;
        }

        var value = context.ReadValueAsButton();

        _inputData.SetEquipListUI(value);

        _model.SetGameInputData(_inputData);
    }
}
