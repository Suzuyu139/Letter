using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerUIModel : MonoBehaviour
{
    CompositeDisposable _disposables = new CompositeDisposable();

    BoolReactiveProperty _isShowEquipListUI = new(false);
    public IReadOnlyReactiveProperty<bool> IsShowEquipListUI => _isShowEquipListUI;
    public void SetIsShowEquipListUI(bool isShowEquipListUI) => _isShowEquipListUI.Value = isShowEquipListUI;

    private void Start()
    {
        _disposables.Add(_isShowEquipListUI);
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }
}
