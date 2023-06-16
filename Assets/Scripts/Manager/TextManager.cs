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

        Debug.LogError($"テキストマスターでデータを取得できませんでした。ID : {id}");
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

        Debug.LogError($"テキストマスターでデータを取得できませんでした。TextKey : {key}");
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
                Debug.LogError($"言語が見つかりませんでした。ID : {data.Id}, TextKey : {data.TextKey}");
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
