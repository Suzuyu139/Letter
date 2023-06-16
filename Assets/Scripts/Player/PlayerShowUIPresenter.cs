using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;

public class PlayerShowUIPresenter : MonoBehaviour
{
    PlayerModel _model;

    PlayerUIModel _playerUIModel;
    bool _isOpenEquipList = false;

    [Inject]
    void Construct(PlayerModel model)
    {
        _model = model;
    }

    private void Start()
    {
        _playerUIModel = GameObject.FindGameObjectWithTag(TagConstants.PlayerUI).GetComponent<PlayerUIModel>();

        _model.InputData.Subscribe(OnInputData).AddTo(gameObject);
    }

    void OnInputData(GameInputData input)
    {
        EquipListUI(input);
    }

    void EquipListUI(GameInputData input)
    {
        if(!input.IsEquipListUI)
        {
            return;
        }

        ShowCursor(!_isOpenEquipList);
        _model.SetIsControl(_isOpenEquipList);

        _playerUIModel.SetIsShowEquipListUI(!_isOpenEquipList);
        _isOpenEquipList = !_isOpenEquipList;
    }

    void ShowCursor(bool isShow)
    {
        Cursor.visible = isShow;
        if(isShow)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
