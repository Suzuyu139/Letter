using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;

public class PlayerEquipListPresenter : MonoBehaviour
{
    PlayerModel _model;

    [Inject]
    void Construct(PlayerModel model)
    {
        _model = model;
    }

    private void Start()
    {
        _model.EquipDictionary.ObserveAdd().Subscribe(OnAddEquipDictionary).AddTo(gameObject);
    }

    void OnAddEquipDictionary(DictionaryAddEvent<(PoolType, int), int> eventDic)
    {
        if(_model.EquipDictionary.Count == 1)
        {
            _model.SetEquip(eventDic.Key);
        }
    }
}
