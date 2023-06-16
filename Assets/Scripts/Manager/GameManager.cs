using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance { get; private set; }

    public string StageName { get; private set; } = string.Empty;

    private void Awake()
    {
        Instance = this;
    }

    public void SetStage(string name)
    {
        StageName = name;
    }
}
