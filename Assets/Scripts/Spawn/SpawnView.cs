using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnView : MonoBehaviour
{
    [SerializeField] GameObject _root;

    public void HideRootObject()
    {
        _root.SetActive(false);
    }
}
