using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerEquipListUIModel : MonoBehaviour
{
    CompositeDisposable _disposables = new CompositeDisposable();

    [SerializeField] float _planeDistance;
    public float PlaneDistance => _planeDistance;

    Dictionary<(PoolType, int), int> _equipDic = new Dictionary<(PoolType, int), int>();
    public Dictionary<(PoolType, int), int> EquipDictionary => _equipDic;
    public void AddEquipDictionary(List<KeyValuePair<(PoolType, int), int>> equipList)
    {
        _equipDic.Clear();
        _equipDic.AddRange(equipList);
    }

    PlayerModel _playerModel;
    public PlayerModel PlayerModel => _playerModel;
    public void SetPlayerModel(PlayerModel playerModel) => _playerModel = playerModel;

    ReactiveProperty<(PoolType, int)> _selectEquip = new();
    public IReadOnlyReactiveProperty<(PoolType, int)> SelectEquip => _selectEquip;
    public void SetSelectEquip((PoolType, int) equip) => _selectEquip.SetValueAndForceNotify(equip);

    private void Start()
    {
        _disposables.Add(_selectEquip);
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }
}
