using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using Cysharp.Threading.Tasks;
using System.Linq;
using UniRx;
using System.Security.Cryptography;

public class PlayerEquipListUIPresenter : MonoBehaviour
{
    PlayerEquipListUIModel _model;
    PlayerEquipListUIView _view;
    ItemRenderTextureView _renderTextureView;

    [Inject]
    void Construct(PlayerEquipListUIModel model, PlayerEquipListUIView view, ItemRenderTextureView renderTextureView)
    {
        _model = model;
        _view = view;
        _renderTextureView = renderTextureView;
    }

    public async UniTask Initialize()
    {
        _view.SetCanvasPlaneDistance(_model.PlaneDistance);
        _model.SetPlayerModel(GameController.Instance.Player.GetComponent<PlayerModel>());

        BindEvents();

        _view.Close();

        await UniTask.CompletedTask;
    }

    public void SetShowUI(bool isShow)
    {
        if(isShow)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    void Open()
    {
        var dic = _model.PlayerModel.EquipDictionary.ToList();
        _model.AddEquipDictionary(dic);

        _view.SetInteractableEquipButton(false);
        _view.SetInteractableRemoveButton(false);

        _view.Open();

        int index = 0;
        foreach(var equip in _model.EquipDictionary)
        {
            foreach(var data in MasterDataManager.Instance.ItemMasterData.Items)
            {
                if(data.Id != equip.Key.Item2)
                {
                    continue;
                }

                string name = string.Empty;
                switch(TextManager.Instance.Language)
                {
                    case LanguageType.Jp:
                        name = data.JpName;
                        break;

                    case LanguageType.En:
                        name = data.EnName;
                        break;

                    default:
                        Debug.LogError($"åæåÍÇ™å©Ç¬Ç©ÇËÇ‹ÇπÇÒÇ≈ÇµÇΩÅBID : {data.Id}");
                        break;
                }
                _view.ActiveButton(index, name, equip.Key);
                break;
            }
            
            index++;
        }
    }

    void Close()
    {
        var obj = _renderTextureView.GetItemObject();
        if (obj != null)
        {
            var itemModel = obj.GetComponent<ItemModel>();

            var id = itemModel != null ? itemModel.Data.Id : 0;

            GameController.Instance.ItemPool.Return(id, obj);
        }

        _view.Close();
    }

    void BindEvents()
    {
        for(int i = 0; i < _view.ButtonViews.Count; i++)
        {
            _view.ButtonViews[i].ButtonObservable.Subscribe(OnButton).AddTo(gameObject);
        }

        _model.SelectEquip.SkipLatestValueOnSubscribe().Subscribe(OnSelectEquipChanged).AddTo(gameObject);

        _view.EquipButtonObservable.Subscribe(OnEquipButton).AddTo(gameObject);
        _view.RemoveButtonObservable.Subscribe(OnRemoveButton).AddTo(gameObject);
    }

    void OnButton((PoolType Type, int Id) equip)
    {
        var obj = _renderTextureView.GetItemObject();
        if(obj != null)
        {
            var itemModel = obj.GetComponent<ItemModel>();

            var id = itemModel != null ? itemModel.Data.Id : 0;

            GameController.Instance.ItemPool.Return(id, obj);
        }

        (PoolType, int) selectEquip;
        switch(equip.Type)
        {
            case PoolType.Item:
                foreach(var data in MasterDataManager.Instance.ItemMasterData.Items)
                {
                    if(data.Id != equip.Id)
                    {
                        continue;
                    }

                    var item = GameController.Instance.ItemPool.Rent(equip.Id, data.ItemObject, _renderTextureView.ItemTransform, null, Vector3.zero);
                    selectEquip.Item2 = item.GetComponent<ItemModel>().Data.Id;
                    selectEquip.Item1 = PoolType.Item;
                    _model.SetSelectEquip(selectEquip);
                    break;
                }
                break;

            default:
                break;
        }
    }

    void OnEquipButton(Unit unit)
    {
        _model.PlayerModel.SetEquip(_model.SelectEquip.Value);
        _model.SetSelectEquip(_model.SelectEquip.Value);
    }

    void OnRemoveButton(Unit unit)
    {
        (PoolType, int) equip;
        equip.Item1 = PoolType.None;
        equip.Item2 = 0;
        _model.PlayerModel.SetEquip(equip);
        _model.SetSelectEquip(_model.SelectEquip.Value);
    }

    void OnSelectEquipChanged((PoolType Type, int Id) equip)
    {
        var isActive = equip.Id == _model.PlayerModel.Equip.Value.Item2;
        _view.SetInteractableRemoveButton(isActive);
        _view.SetInteractableEquipButton(!isActive);
    }
}
