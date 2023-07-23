using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class GameController : MonoBehaviour
{
    static public GameController Instance { get; private set; }

    GameControllerModel _model;

    public GameObject Player { get; private set; }
    public int[][] StageData { get; private set; }

    PoolPresenter _itemPool = null;
    public PoolPresenter ItemPool => _itemPool;

    [Inject]
    void Construct(GameControllerModel model)
    {
        _model = model;
    }

    private void Awake()
    {
        _itemPool = Instantiate(_model.ItemPoolPrefab).GetComponent<PoolPresenter>();
        StageData = _model.StageData;

        Instance = this;
    }

    public void SetPlayer(GameObject player)
    {
        Player = player;
    }
}
