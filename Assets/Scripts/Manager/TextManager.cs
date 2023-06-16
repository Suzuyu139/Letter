using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    static public TextManager Instance { get; private set; }

    public LanguageType Language { get; private set; } = LanguageType.Jp;

    private void Awake()
    {
        Instance = this;
    }

    public string GetText(int id)
    {
        foreach(var text in MasterDataManager.Instance.TextMasterData.Texts)
        {
            if(text.Id != id)
            {
                continue;
            }

            return GetLanguageText(text);
        }

        Debug.LogError($"�e�L�X�g�}�X�^�[�Ńf�[�^���擾�ł��܂���ł����BID : {id}");
        return string.Empty;
    }

    public string GetText(string key)
    {
        foreach (var text in MasterDataManager.Instance.TextMasterData.Texts)
        {
            if (text.TextKey != key)
            {
                continue;
            }

            return GetLanguageText(text);
        }

        Debug.LogError($"�e�L�X�g�}�X�^�[�Ńf�[�^���擾�ł��܂���ł����BTextKey : {key}");
        return string.Empty;
    }

    string GetLanguageText(TextMasterData.TextData data)
    {
        string text = string.Empty;

        switch(Language)
        {
            case LanguageType.Jp:
                text = data.Jp;
                break;

            case LanguageType.En:
                text = data.En;
                break;

            default:
                Debug.LogError($"���ꂪ������܂���ł����BID : {data.Id}, TextKey : {data.TextKey}");
                break;
        }

        return text;
    }

    public void SetLanguageType(LanguageType type)
    {
        Language = type;
    }
}

public enum LanguageType
{
    Jp,
    En,
}
