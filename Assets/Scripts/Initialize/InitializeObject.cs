using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeObject : MonoBehaviour
{
    [SerializeField] GameObject[] _initializeObject = null;

    public bool IsInitialized { get; private set; } = false;

    private void Awake()
    {
        if(_initializeObject == null)
        {
            return;
        }

        for(int i = 0;  i < _initializeObject.Length; i++)
        {
            var obj = Instantiate(_initializeObject[i]);
            DontDestroyOnLoad(obj);
        }

        IsInitialized = true;
    }
}
