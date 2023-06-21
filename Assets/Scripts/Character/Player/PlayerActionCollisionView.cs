using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerActionCollisionView : MonoBehaviour
{
    [SerializeField] float _distance = 10.0f;

    GameObject _collisionObj = null;
    public GameObject CollisionObj => _collisionObj;

    Transform myTransform = null;
    Transform cameraTransform = null;
    int _layerMask;

    private void Start()
    {
        myTransform = this.transform;
        cameraTransform = Camera.main.transform;
        _layerMask = 1 << 6;

        this.UpdateAsObservable().Subscribe(OnUpdate).AddTo(gameObject);
    }

    void OnUpdate(Unit unit)
    {
        var rayStartPos = cameraTransform.position;
        var rayDirection = cameraTransform.forward.normalized;

        RaycastHit hit;
        Debug.DrawLine(rayStartPos, rayDirection * _distance, Color.red, 0.001f, false);
        if(Physics.Raycast(rayStartPos, rayDirection, out hit, _distance, _layerMask))
        {
            if(_collisionObj == null)
            {
                var equip = hit.collider.GetComponent<IEquipActionCollision>();
                if(equip != null)
                {
                    _collisionObj = equip.EquipObject;
                }
            }
        }
        else
        {
            _collisionObj = null;
        }
    }
}
