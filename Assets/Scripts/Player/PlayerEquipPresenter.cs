using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;

public class PlayerEquipPresenter : MonoBehaviour
{
    PlayerModel _model;
    PlayerEquipView _equipView;

    [Inject]
    void Construct(PlayerModel model, PlayerEquipView equipView)
    {
        _model = model;
        _equipView = equipView;
    }

    private void Start()
    {
        _model.Equip.Subscribe(OnEquipChanged).AddTo(gameObject);
    }

    void OnEquipChanged((PoolType Type, int Id) equip)
    {
        if (_equipView.HandTransform.childCount > 0)
        {
            var childObj = _equipView.HandTransform.GetChild(0).gameObject;
            var itemModel = childObj.GetComponent<ItemModel>();
            var id = itemModel != null ? itemModel.Data.Id : 0;
            GameController.Instance.ItemPool.Return(id, childObj);
        }

        if (equip.Id <= 0)
        {
            return;
        }

        switch(equip.Type)
        {
            case PoolType.Item:
                GameController.Instance.ItemPool.Rent(equip.Id, _equipView.HandTransform, null, Vector3.zero);
                break;

            default:
                break;
        }
    }
}
