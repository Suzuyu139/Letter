using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnModel : MonoBehaviour
{
    [SerializeField] SpawnType _spawnType;
    public SpawnType SpawnType => _spawnType;

    [SerializeField] int _id;
    public int Id => _id;
}

public enum SpawnType
{
    None,
    Player,
    Item,
}
