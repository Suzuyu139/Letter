using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRenderTextureView : MonoBehaviour
{
    [SerializeField] Transform _itemTransform;
    public Transform ItemTransform => _itemTransform;

    public GameObject GetItemObject()
    {
        if(_itemTransform.childCount <= 0)
        {
            return null;
        }

        var obj = _itemTransform.GetChild(0).gameObject;
        return obj;
    }
}
