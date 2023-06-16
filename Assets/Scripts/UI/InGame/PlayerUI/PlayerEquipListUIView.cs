using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerEquipListUIView : MonoBehaviour
{
    [SerializeField] Canvas _canvas;
    [SerializeField] GameObject _itemRenderTextureObj;
    [SerializeField] List<PlayerEquipListUIButtonView> _buttonViews;
    [SerializeField] Button _equipButton;
    [SerializeField] TextMeshProUGUI _equipButtonText;
    [SerializeField] Button _removeButton;
    [SerializeField] TextMeshProUGUI _removeButtonText;

    public List<PlayerEquipListUIButtonView> ButtonViews => _buttonViews;
    public IObservable<Unit> EquipButtonObservable => _equipButton.OnClickAsObservable();
    public IObservable<Unit> RemoveButtonObservable => _removeButton.OnClickAsObservable();

    public void Open()
    {
        _equipButtonText.text = TextManager.Instance.GetText("ui_equip");
        _removeButtonText.text = TextManager.Instance.GetText("ui_remove");

        foreach(var buttonView in _buttonViews)
        {
            buttonView.gameObject.SetActive(false);
        }
        this.gameObject.SetActive(true);
        _itemRenderTextureObj.SetActive(true);
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
        _itemRenderTextureObj.SetActive(false);
    }

    public void SetCanvasPlaneDistance(float distance)
    {
        _canvas.planeDistance = distance;
    }

    public void ActiveButton(int index, string name, (PoolType, int) equip)
    {
        _buttonViews[index].gameObject.SetActive(true);
        _buttonViews[index].SetText(name);
        _buttonViews[index].SetEquip(equip);
    }

    public void SetInteractableEquipButton(bool isActive)
    {
        _equipButton.interactable = isActive;
    }

    public void SetInteractableRemoveButton(bool isActive)
    {
        _removeButton.interactable = isActive;
    }
}
