using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using Cysharp.Threading.Tasks;

public class SpawnPresenter : MonoBehaviour
{
    SpawnModel _model;
    SpawnView _view;

    [Inject]
    void Construct(SpawnModel model, SpawnView view)
    {
        _model = model;
        _view = view;
    }

    private void Start()
    {
        SpawnAsync().Forget();
    }

    async UniTask SpawnAsync()
    {
        await UniTask.WaitUntil(() => MasterDataManager.Instance != null);
        await UniTask.WaitUntil(() => GameController.Instance != null);

        Spawn();
    }

    void Spawn()
    {
        _view.HideRootObject();

        switch(_model.SpawnType)
        {
            case SpawnType.Item:
                ItemMasterData.ItemData? item = MasterDataManager.Instance.ItemMasterData.Items.Find(x => x.Id == _model.Id);
                if(item == null)
                {
                    return;
                }
                var itemObj = GameController.Instance.ItemPool.Rent(item.Value.Id, item.Value.ItemObject, null, this.transform.position);
                break;

            case SpawnType.Player:
                PlayerMasterData.PlayerData? player = MasterDataManager.Instance.PlayerMasterData.Players.Find(x => x.Id == _model.Id);
                if(player == null)
                {
                    return;
                }
                var playerModel = Instantiate(player.Value.PlayerObject, this.transform.position, Quaternion.identity).GetComponent<PlayerModel>();
                playerModel.SetMasterData(player.Value);
                GameController.Instance.SetPlayer(playerModel.gameObject);
                break;

            default:
                break;
        }
    }
}
