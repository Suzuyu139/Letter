using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerModel : MonoBehaviour
{
    [SerializeField] GameObject _itemPoolPrefab = null;
    public GameObject ItemPoolPrefab => _itemPoolPrefab;

    int[][] _stageData = new int[LocalAppConst.StageDataSize][];
    public int[][] StageData => _stageData;
    public void SetStageData(int y, int x, int data)
    {
        _stageData[y][x] = data;
    }

    private void Awake()
    {
        for(int i = 0; i < _stageData.Length; i++)
        {
            _stageData[i] = new int[LocalAppConst.StageDataSize];
        }
    }
}
