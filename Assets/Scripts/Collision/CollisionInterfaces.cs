using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipActionCollision
{
    public GameObject EquipObject { get; }
    public void SetColliderEnable(bool isEnable);
}
