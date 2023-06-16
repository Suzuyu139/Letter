using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMasterData", menuName = "MasterData/CreatePlayerMaster")]
public class PlayerMasterData : ScriptableObject
{
    [System.Serializable]
    public struct PlayerData
    {
        [SerializeField] int _id;
        [SerializeField] GameObject _playerObject;

        public int Id => _id;
        public GameObject PlayerObject => _playerObject;
    }

    [SerializeField] List<PlayerData> _players = new List<PlayerData>();
    public List<PlayerData> Players => _players;
}
