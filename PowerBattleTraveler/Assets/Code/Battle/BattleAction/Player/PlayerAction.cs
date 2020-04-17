using UnityEngine;
using UniRx.Async;

namespace Battle{
public class PlayerAction : IAction
{
    uint ownId;

    public PlayerAction(uint ownId)
    {
        this.ownId = ownId;
    }

    public async UniTask SelectAsync(BattleDataManager dataManager, BattleViewManager viewManager)
    {
        Debug.Log(dataManager.Actors[ownId].Name + "の行動");
        await viewManager.CommandAsync();
    }

    public async UniTask ActionAsync(BattleDataManager dataManager, BattleViewManager viewManager)
    {
        await viewManager.AnimationAsync();
    }

    public void Calc(BattleDataManager dataManager)
    {

    }
}
} // Battle