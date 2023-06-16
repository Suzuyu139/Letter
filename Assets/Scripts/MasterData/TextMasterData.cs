using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextMasterData", menuName = "MasterData/CreateTextMaster")]
public class TextMasterData : ScriptableObject
{
    [System.Serializable]
    public struct TextData
    {
        [SerializeField] int _id;
        [SerializeField] string _textKey;
        [SerializeField] string _jp;
        [SerializeField] string _en;

        public int Id => _id;
        public string TextKey => _textKey;
        public string Jp => _jp;
        public string En => _en;
    }

    [SerializeField] List<TextData> _texts = new List<TextData>();
    public List<TextData> Texts => _texts;
}
