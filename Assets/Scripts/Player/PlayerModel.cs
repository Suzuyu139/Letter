using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

public class PlayerModel : MonoBehaviour
{
    CompositeDisposable _disposables = new CompositeDisposable();

    [SerializeField] float _moveSpeed = 0.0f;
    public float MoveSpeed => _moveSpeed;
    public void SetMoveSpeed(float speed) => _moveSpeed = speed;

    [SerializeField] float _sensiHorizontal = 0.0f;
    public float SensiHorizontal => _sensiHorizontal;
    [SerializeField] float _sensiVertical = 0.0f;
    public float SensiVertical => _sensiVertical;
    [SerializeField] float _viewpointVerticalMin = 0.0f;
    public float ViewpointVerticalMin => _viewpointVerticalMin;
    [SerializeField] float _viewpointVerticalMax = 0.0f;
    public float ViewpointVerticalMax => _viewpointVerticalMax;

    ReactiveProperty<GameInputData> _inputData = new();
    public IReactiveProperty<GameInputData> InputData => _inputData;
    public void SetGameInputData(GameInputData inputData) => _inputData.SetValueAndForceNotify(inputData);

    ReactiveProperty<(PoolType, int)> _equip = new();
    public IReadOnlyReactiveProperty<(PoolType, int)> Equip => _equip;
    public void SetEquip((PoolType, int) equip) => _equip.Value = equip;

    ReactiveDictionary<(PoolType, int), int> _equipDictionary = new();
    public IReadOnlyReactiveDictionary<(PoolType, int), int> EquipDictionary => _equipDictionary;
    public void AddEquipDictionary((PoolType, int) equip)
    {
        if(_equipDictionary.ContainsKey(equip))
        {
            _equipDictionary[equip] += 1;
        }
        else
        {
            _equipDictionary.Add(equip, 1);
        }
    }
    public void RemoveEquipDictionary((PoolType, int) equip)
    {
        if(_equipDictionary[equip] <= 1)
        {
            _equipDictionary.Remove(equip);
        }
        else
        {
            _equipDictionary[equip] -= 1;
        }
    }

    BoolReactiveProperty _isControl = new(true);
    public IReadOnlyReactiveProperty<bool> IsControl => _isControl;
    public void SetIsControl(bool isControl) => _isControl.Value = isControl;

    public PlayerMasterData.PlayerData Data { get; private set; }
    public void SetMasterData(PlayerMasterData.PlayerData data) => Data = data;

    private void Start()
    {
        _disposables.Add(_inputData);
        _disposables.Add(_equip);
        _disposables.Add(_equipDictionary);
        _disposables.Add(_isControl);
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }
}
