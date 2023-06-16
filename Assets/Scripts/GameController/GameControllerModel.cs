using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerModel : MonoBehaviour
{
    [SerializeField] GameObject _itemPoolPrefab = null;
    public GameObject ItemPoolPrefab => _itemPoolPrefab;
}
