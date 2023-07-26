using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;
using Cysharp.Threading.Tasks;

public class CreateStagePresenter : MonoBehaviour
{
    public async UniTask CreateStage(int[][] stageData)
    {
        await UniTask.WaitUntil(() => MasterDataManager.Instance != null);

        for(int y = 0; y < stageData.Length; y++)
        {
            for(int x = 0; x < stageData[y].Length; x++)
            {
                if (stageData[y][x] == (int)LocalAppConst.StageDataType.None)
                {
                    continue;
                }

                Vector3 worldPos = new Vector3((x + 1) * 10.0f, -0.5f, (y + 1) * 10.0f);
                GameObject stageObj = null;
                switch (stageData[y][x])
                {
                    case (int)LocalAppConst.StageDataType.Start:
                        stageObj = MasterDataManager.Instance.StageMasterData.GetStageObject(5001);
                        break;
                    case (int)LocalAppConst.StageDataType.Goal:
                        stageObj = MasterDataManager.Instance.StageMasterData.GetStageObject(5002);
                        break;
                    case (int)LocalAppConst.StageDataType.Way:
                        stageObj = MasterDataManager.Instance.StageMasterData.GetStageObject(5003);
                        break;
                    case (int)LocalAppConst.StageDataType.Empty:
                        stageObj = MasterDataManager.Instance.StageMasterData.GetStageObject(5004);
                        break;
                    case (int)LocalAppConst.StageDataType.Key:
                        stageObj = MasterDataManager.Instance.StageMasterData.GetStageObject(5005);
                        break;
                }

                if(stageObj == null)
                {
                    Debug.LogError($"ステージオブジェクトが見つかりませんでした。StageData : {(LocalAppConst.StageDataType)stageData[y][x]}");
                    continue;
                }

                var obj = Instantiate(stageObj, worldPos, Quaternion.identity);
                obj.transform.SetParent(this.transform);
                if (stageData[y][x] == (int)LocalAppConst.StageDataType.Way)
                {
                    var walls = obj.GetComponentsInChildren<StageWallView>();
                    if (stageData[y - 1][x] != (int)LocalAppConst.StageDataType.None)
                    {
                        SetActiveWall(walls, StageWallType.Front, false);
                    }
                    if(stageData[y + 1][x] != (int)LocalAppConst.StageDataType.None)
                    {
                        SetActiveWall(walls, StageWallType.Back, false);
                    }
                    if (stageData[y][x - 1] != (int)LocalAppConst.StageDataType.None)
                    {
                        SetActiveWall(walls, StageWallType.Left, false);
                    }
                    if (stageData[y][x + 1] != (int)LocalAppConst.StageDataType.None)
                    {
                        SetActiveWall(walls, StageWallType.Right, false);
                    }
                }
            }
        }

        await UniTask.CompletedTask;
    }

    void SetActiveWall(StageWallView[] walls, StageWallType wallType, bool isActive)
    {
        for(int i = 0; i < walls.Length; i++)
        {
            if (walls[i].WallType == wallType)
            {
                walls[i].gameObject.SetActive(isActive);
                break;
            }
        }
    }
}
