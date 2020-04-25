using System;
using UnityEngine;
using UniRx.Async;
using DG.Tweening;
using Common;

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

    public void Calc( BattleDataManager dataManager)
    {
        var targetData = dataManager.Actors[targetId];
        targetData.Hp -= dataManager.Actors[ownId].Attack;

        // リストに戻す
        dataManager.Actors[targetId] = targetData;
    }



#region Animations

    public async UniTask ActionAsync(BattleDataManager dataManager, BattleViewManager viewManager)
    {
        var ownTransform = viewManager.ActorsRootView.ActorViews[ownId].transform;
        var pos = ownTransform.position.x;
        // 前に
        await ownTransform.DOMoveX(pos - 1.0f, 0.5f).SetEase(Ease.InQuart);

        // ぐわぁ
        var targetTransform = viewManager.ActorsRootView.ActorViews[targetId].transform;
        await targetTransform.DOShakePosition(0.5f).SetEase(Ease.InQuart);

        // もどる
        await ownTransform.DOMoveX(pos, 0.5f).SetEase(Ease.InQuart);
    }

#endregion Animations
}
} // Battle
