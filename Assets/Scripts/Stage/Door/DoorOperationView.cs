using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;

public class DoorOperationView : MonoBehaviour
{
    [SerializeField] DoorView _doorView;
    [SerializeField] Collider[] _colliderAutoDoor = new Collider[] { };
    [Space(10)]
    [SerializeField] DoorType _doorType;

    void Start()
    {
        _doorView.Initialize();

        switch (_doorType)
        {
            case DoorType.Auto:
                if (_colliderAutoDoor.Length <= 0)
                {
                    return;
                }
                for (int i = 0; i < _colliderAutoDoor.Length; i++)
                {
                    _colliderAutoDoor[i].OnTriggerEnterAsObservable()
                        .Subscribe(OnAutoDoorTriggerEnter).AddTo(this);
                }
                break;

            default:
                break;
        }
    }

    void OnAutoDoorTriggerEnter(Collider collider)
    {
        _doorView.Open(true);
    }
}

public enum DoorType
{
    Auto,
}