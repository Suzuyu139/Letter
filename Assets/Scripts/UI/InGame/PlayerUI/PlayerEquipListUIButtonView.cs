using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UniRx;

public class PlayerEquipListUIButtonView : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] TextMeshProUGUI _text;

    public (PoolType Type, int Id) Equip { get; private set; }

    public IObservable<(PoolType Type, int Id)> ButtonObservable => _button.OnClickAsObservable().Select(x => Equip);

    public void SetText(string text)
    {
        _text.text = text;
    }

    public void SetEquip((PoolType Type, int Id) equip)
    {
        Equip = equip;
    }
}
