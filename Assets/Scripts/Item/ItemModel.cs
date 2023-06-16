using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemModel : MonoBehaviour
{
    public ItemMasterData.ItemData Data { get; private set; }
    public void SetMasterData(ItemMasterData.ItemData data) => Data = data;
}
