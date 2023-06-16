using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputProvider
{
    
}

public struct GameInputData
{
    public float MoveHorizontal { get; private set; }
    public float MoveVertical { get; private set; }
    public float ViewpointX { get; private set; }
    public float ViewpointY { get; private set; }
    public bool IsAction { get; private set; }
    public bool IsEquipListUI { get; private set; }
    public bool IsAttack { get; private set; }

    public void SetMoveHorizontal(float h) => MoveHorizontal = h;
    public void SetMoveVertical(float v) => MoveVertical = v;
    public void SetViewpointX(float x) => ViewpointX = x;
    public void SetViewpointY(float y) => ViewpointY = y;
    public void SetIsAction(bool isAction) => IsAction = isAction;
    public void SetEquipListUI(bool isEquipListUI) => IsEquipListUI = isEquipListUI;
    public void SetIsAttack(bool isAttack) => IsAttack = isAttack;
}
