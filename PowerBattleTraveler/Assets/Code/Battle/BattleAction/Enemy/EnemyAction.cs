using System;
using UnityEngine;
using UniRx.Async;

namespace Battle {
public class EnemyDiside : IAction
{
    ActorData ownData;

    uint ownId;

    uint targetId;

    public EnemyDiside(uint ownId)
    {
        this.ownId = ownId;
    }

    public async UniTask SelectAsync(BattleDataManager dataManager, BattleViewManager viewManager)
    {
        Debug.Log(ownData.Name + "の行動");
        await UniTask.Delay(System.TimeSpan.FromSeconds(1));

        targetId = 1;
    }



    public async UniTask ActionAsync(BattleDataManager dataManager, BattleViewManager viewManager)
    {
        // var acView = viewManager.GetActorsView().GetActorData(targetId);
        // acView.setHp(dataManager.Actors[targetId].Hp);

        // if (dataManager.Actors[targetId].Hp <= 0) {
        //     acView.gameObject.SetActive(false);
        // }

        await viewManager.AnimationAsync();

    }

    public void Calc( BattleDataManager dataManager)
    {
        var targetData = dataManager.Actors[targetId];
        targetData.Hp -= dataManager.Actors[ownId].Attack;

        // リストに戻す
        dataManager.Actors[targetId] = targetData;
    }
}
} // Battle
