using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterDataManager : MonoBehaviour
{
    static public MasterDataManager Instance { get; private set; }

    [SerializeField] ItemMasterData _itemMasterData = null;
    public ItemMasterData ItemMasterData => _itemMasterData;

    [SerializeField] PlayerMasterData _playerMasterData = null;
    public PlayerMasterData PlayerMasterData => _playerMasterData;

    [SerializeField] TextMasterData _textMasterData = null;
    public TextMasterData TextMasterData => _textMasterData;

    private void Awake()
    {
        Instance = this;
    }
}
