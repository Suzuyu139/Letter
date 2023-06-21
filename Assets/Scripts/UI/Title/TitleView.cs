using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class TitleView : MonoBehaviour
{
    [SerializeField] Button _startButton;

    public IObservable<Unit> StartButtonObservable => _startButton.OnClickAsObservable().ThrottleFirst(TimeSpan.FromSeconds(GameSceneManager.Instance.AnimationSpeed));
}
