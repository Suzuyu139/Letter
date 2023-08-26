using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorView : MonoBehaviour
{
    [SerializeField] Transform _rightDoorTransform;
    [SerializeField] Transform _leftDoorTransform;
    [SerializeField] float _speed;
    [SerializeField] float _movePosX;

    public bool IsInitialized { get; private set; } = false;
    public bool IsOpen { get; private set; } = false;

    float _initRightDoorPosX = 0.0f;
    float _initLeftDoorPosX = 0.0f;

    public void Initialize()
    {
        if(IsInitialized)
        {
            return;
        }

        _initRightDoorPosX = _rightDoorTransform.localPosition.x;
        _initLeftDoorPosX = _leftDoorTransform.localPosition.x;

        IsInitialized = true;
    }

    public void Open(bool isAutoClose = false)
    {
        if (!IsInitialized)
        {
            Initialize();
        }
        if (IsOpen)
        {
            return;
        }

        IsOpen = true;
        _rightDoorTransform.DOLocalMoveX(_movePosX, _speed);
        _leftDoorTransform.DOLocalMoveX(-_movePosX, _speed);

        if (isAutoClose)
        {
            DOVirtual.DelayedCall(5.0f, () =>
            {
                Close();
            });
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
        _leftDoorTransform.DOLocalMoveX(-_initLeftDoorPosX, _speed);
    }
}
