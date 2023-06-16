using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipActionCollision : MonoBehaviour, IEquipActionCollision
{
    [SerializeField] GameObject _equipObj = null;
    public GameObject EquipObject => _equipObj;

    Collider _collider;

    private void Start()
    {
        _collider = this.GetComponent<Collider>();
    }

    public void SetColliderEnable(bool isEnable)
    {
        _collider.enabled = isEnable;
    }
}
