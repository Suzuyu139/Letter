using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;
using UniRx.Triggers;

public class PlayerActionPresenter : MonoBehaviour
{
    PlayerModel _model;
    PlayerActionCollisionView _collisionView;
    PlayerEquipView _equipView;

    bool _isPush = false;

    [Inject]
    void Construct(PlayerModel model, PlayerActionCollisionView collisionView, PlayerEquipView equipView)
    {
        _model = model;
        _collisionView = collisionView;
        _equipView = equipView;
    }

    private void Start()
    {
        _model.InputData.Subscribe(OnInput).AddTo(gameObject);
    }

    void OnInput(GameInputData data)
    {
        if(data.IsAction && !_isPush)
        {
            _isPush = true;
        }
        else if(!data.IsAction && _isPush)
        {
            _isPush = false;
            Equip();
        }
    }

    void Equip()
    {
        if(_collisionView.CollisionObj != null)
        {
            var itemModel = _collisionView.CollisionObj.GetComponent<ItemModel>();
            if(itemModel != null)
            {
                if(itemModel.Data.Id != 0)
                {
                    var actionCollision = _collisionView.CollisionObj.GetComponentInChildren<IEquipActionCollision>();
                    actionCollision.SetColliderEnable(false);
                    GameController.Instance.ItemPool.Return(itemModel.Data.Id, _collisionView.CollisionObj);
                    _model.AddEquipDictionary((PoolType.Item, itemModel.Data.Id));
                }
            }
        }
    }
}
