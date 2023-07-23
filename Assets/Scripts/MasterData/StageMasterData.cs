using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageMasterData", menuName = "MasterData/CreateStageMaster")]
public class StageMasterData : ScriptableObject
{
    [System.Serializable]
    public struct StageData
    {
        [SerializeField] int _id;
        [SerializeField] GameObject _stageObject;

        public int Id => _id;
        public GameObject StageObject => _stageObject;
    }

    [SerializeField] List<StageData> _stages = new List<StageData>();
    public List<StageData> Stages => _stages;
}
