using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Text;

public class GameControllerMakeStagePresenter : MonoBehaviour
{
    public enum WayDirectionType
    {
        None,
        Up,
        Down,
        Left,
        Right,
    }

    GameControllerModel _model;

    public bool IsInitialized { get; private set; } = false;

    Vector2Int _currentPos = new Vector2Int(0, 0);
    Vector2Int _startPos = new Vector2Int(0, 0);

    [Inject]
    void Construct(GameControllerModel model)
    {
        _model = model;
    }

    private void Start()
    {
        InitializeAsync().Forget();
    }

    async UniTask InitializeAsync()
    {
        await CreateStage();

#if DEBUG
        var stageStringBuilder = new StringBuilder();
        for(int y = 0; y < _model.StageData.Length; y++)
        {
            for(int x = 0; x < _model.StageData[y].Length; x++)
            {
                stageStringBuilder.Append(_model.StageData[y][x]);
            }
            stageStringBuilder.AppendLine();
        }
        Debug.Log($"StageData\n{stageStringBuilder.ToString()}");
#endif

        IsInitialized = true;

        await UniTask.CompletedTask;
    }

    async UniTask CreateStage()
    {
        Vector2Int startPos = new Vector2Int(0, 0);

        // スタートの位置を決める
        startPos.y = Random.Range(1, 8);
        await UniTask.WaitUntil(() =>
        {
            bool isStartX = false;

            startPos.x = Random.Range(1, 8);
            isStartX = !((startPos.y == 4 || startPos.y == 5) && (startPos.x == 4 || startPos.x == 5));

            return isStartX;
        });
        _model.SetStageData(startPos.y, startPos.x, (int)LocalAppConst.StageDataType.Way);
        _startPos = startPos;
        _currentPos = startPos;

        // 位置的にx,yそれぞれスタート位置が真ん中の列にあったらずらす
        WayDirectionType way = WayDirectionType.None;
        if(startPos.y == 4 || startPos.y == 5)
        {
            if(startPos.x < 4)
            {
                way = WayDirectionType.Up;
            }
            else if (startPos.x > 5)
            {
                way= WayDirectionType.Down;
            }
        }
        else if(startPos.x == 4 || startPos.x == 5)
        {
            if (startPos.y < 4)
            {
                way = WayDirectionType.Right;
            }
            else if (startPos.y > 5)
            {
                way = WayDirectionType.Left;
            }
        }
        BuildStageWay(way);

        // 道を作る（最後一歩手前まで作成）
        for(int i = 0; i < 3; i++)
        {
            if(_currentPos.x > 5 && _currentPos.y < 4)
            {
                way = WayDirectionType.Down;
            }
            else if (_currentPos.x > 5 && _currentPos.y > 5)
            {
                way = WayDirectionType.Left;
            }
            else if (_currentPos.x < 4 && _currentPos.y < 4)
            {
                way = WayDirectionType.Right;
            }
            else if (_currentPos.x < 4 && _currentPos.y > 5)
            {
                way = WayDirectionType.Up;
            }
            BuildStageWay(way);
        }

        // 最後の道を作る
        if (_currentPos.x > 5 && _currentPos.y < 4)
        {
            way = WayDirectionType.Down;
        }
        else if (_currentPos.x > 5 && _currentPos.y > 5)
        {
            way = WayDirectionType.Left;
        }
        else if (_currentPos.x < 4 && _currentPos.y < 4)
        {
            way = WayDirectionType.Right;
        }
        else if (_currentPos.x < 4 && _currentPos.y > 5)
        {
            way = WayDirectionType.Up;
        }
        BuildStageWay(way, true);

        await UniTask.CompletedTask;
    }

    private void BuildStageWay(WayDirectionType way, bool isLast = false)
    {
        if(way == WayDirectionType.None)
        {
            return;
        }

        var destination = _currentPos;
        switch (way)
        {
            case WayDirectionType.Up:
                destination.y = Random.Range(1, 3);
                break;

            case WayDirectionType.Down:
                destination.y = Random.Range(6, 8);
                break;

            case WayDirectionType.Left:
                destination.x = Random.Range(1, 3);
                break;

            case WayDirectionType.Right:
                destination.x = Random.Range(6, 8);
                break;
        }

        if(isLast)
        {
            switch(way)
            {
                case WayDirectionType.Up:
                case WayDirectionType.Down:
                    destination.y = Random.Range(4, 5);
                    break;

                case WayDirectionType.Left:
                case WayDirectionType.Right:
                    destination.x = Random.Range(4, 5);
                    break;
            }
            LastStageWay(destination);
        }
        else
        {
            StageWay(destination);
        }
    }

    void StageWay(Vector2Int destination)
    {
        Vector2Int calcPos = new Vector2Int(0, 0);

        calcPos = destination - _currentPos;
        int calcMax = calcPos.x != 0 ? calcPos.x : calcPos.y;
        for (int i = 0; i < System.Math.Abs(calcMax); i++)
        {
            _model.SetStageData(
                _currentPos.y + ((i + 1) * (calcPos.y == 0 ? 0 : System.Math.Sign(calcPos.y))),
                _currentPos.x + ((i + 1) * (calcPos.x == 0 ? 0 : System.Math.Sign(calcPos.x))),
                (int)LocalAppConst.StageDataType.Way);
        }

        _currentPos = destination;
    }

    void LastStageWay(Vector2Int destination)
    {
        Vector2Int calcPos = new Vector2Int(0, 0);

        calcPos = destination - _currentPos;
        int calcMax = calcPos.x != 0 ? calcPos.x : calcPos.y;
        for (int i = 0; i < System.Math.Abs(calcMax); i++)
        {
            _model.SetStageData(
                _currentPos.y + ((i + 1) * (calcPos.y == 0 ? 0 : System.Math.Sign(calcPos.y))),
                _currentPos.x + ((i + 1) * (calcPos.x == 0 ? 0 : System.Math.Sign(calcPos.x))),
                (int)LocalAppConst.StageDataType.Way);
        }
        _currentPos = destination;

        if (calcPos.x != 0)
        {
            var calcPosY = _startPos.y - _currentPos.y;
            destination.y += calcPosY;
            for(int i = 0; i < System.Math.Abs(calcPosY); i++)
            {
                _model.SetStageData(
                    _currentPos.y + ((i + 1) * (calcPosY == 0 ? 0 : System.Math.Sign(calcPosY))),
                    _currentPos.x,
                    (int)LocalAppConst.StageDataType.Way);
            }
        }
        else if(calcPos.y != 0)
        {
            var calcPosX = _startPos.x - _currentPos.x;
            destination.x += calcPosX;
            for (int i = 0; i < System.Math.Abs(calcPosX); i++)
            {
                _model.SetStageData(
                    _currentPos.y,
                    _currentPos.x + ((i + 1) * (calcPosX == 0 ? 0 : System.Math.Sign(calcPosX))),
                    (int)LocalAppConst.StageDataType.Way);
            }
        }
        _currentPos = destination;

        if(_startPos == _currentPos)
        {
            return;
        }
        StageWay(_startPos);
    }
}
