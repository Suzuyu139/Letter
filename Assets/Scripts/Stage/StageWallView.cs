using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageWallView : MonoBehaviour
{
    [SerializeField] StageWallType _wallType = default;
    public StageWallType WallType => _wallType;
}

public enum StageWallType
{
    Front,
    Back,
    Right,
    Left,
}
