using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipView : MonoBehaviour
{
    [SerializeField] Transform _handTransform;
    public Transform HandTransform => _handTransform;
}
