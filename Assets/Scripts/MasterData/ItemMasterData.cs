using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemMasterData", menuName = "MasterData/CreateItemMaster")]
public class ItemMasterData : ScriptableObject
{
    [System.Serializable]
    public struct ItemData
    {
        [SerializeField] int _id;
        [SerializeField] string _jpName;
        [SerializeField] string _enName;
        [SerializeField] GameObject _itemObject;

        public int Id => _id;
        public string JpName => _jpName;
        public string EnName => _enName;
        public GameObject ItemObject => _itemObject;
    }

    [SerializeField] List<ItemData> _items = new List<ItemData>();
    public List<ItemData> Items => _items;
}
