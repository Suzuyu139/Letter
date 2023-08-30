using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UniRx.Diagnostics;

public class DoorView : MonoBehaviour
{
    [SerializeField] Transform _rightDoorTransform;
    [SerializeField] Transform _leftDoorTransform;
    [Header("ƒhƒA‚ÌÝ’è")]
    [SerializeField] float _speed;
    [SerializeField] float _movePosX;
    [SerializeField] float _autoClose;

    public bool IsInitialized { get; private set; } = false;
    public bool IsOpen { get; private set; } = false;

    float _initRightDoorPosX = 0.0f;
    float _initLeftDoorPosX = 0.0f;
    float _autoCloseTimer = 0.0f;

    public void Initialize()
    {
        if(IsInitialized)
        {
            return;
        }

        _initRightDoorPosX = _rightDoorTransform.localPosition.x;
        _initLeftDoorPosX = _leftDoorTransform.localPosition.x;

        this.UpdateAsObservable().Subscribe(OnUpdate).AddTo(gameObject);

        IsInitialized = true;
    }

    void OnUpdate(Unit unit)
    {
        if(IsOpen && _autoCloseTimer > 0.0f)
        {
            _autoCloseTimer -= Time.deltaTime;
            if( _autoCloseTimer <= 0.0f)
            {
                _autoCloseTimer = 0.0f;
                Close();
            }
        }
    }

    public void Open(bool isAutoClose = false)
    {
        if (!IsInitialized)
        {
            Initialize();
        }
        if (IsOpen)
        {
            _autoCloseTimer = _autoClose;
            return;
        }

        IsOpen = true;
        _rightDoorTransform.DOLocalMoveX(_movePosX, _speed);
        _leftDoorTransform.DOLocalMoveX(-_movePosX, _speed);

        if (isAutoClose)
        {
            _autoCloseTimer = _autoClose;
        }
    }

    public void Close()
    {
        if (!IsInitialized)
        {
            Initialize();
        }
        if (!IsOpen)
        {
            return;
        }

        IsOpen = false;
        _rightDoorTransform.DOLocalMoveX(_initRightDoorPosX, _speed);
        _leftDoorTransform.DOLocalMoveX(_initLeftDoorPosX, _speed);
    }
}
