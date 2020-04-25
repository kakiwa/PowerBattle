using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using IceMilkTea.Core;
using UniRx.Async;

namespace Battle {
public partial class BattleStateManager {

/// <summary>
/// キャラクターが一回行動するステート
/// </summary>
private class ActState : ImtStateMachine<BattleStateManager>.State
{
    /// <summary>
    /// 行動をキャッシュする用
    /// </summary>
    IAction m_Action = default;

    protected override async void Enter()
    {
        // リストの頭から行動
        m_Action = Context.m_ActionList.First();

        await ActStartAnimation();

        await Act();

        await ActEndAnimation();

        // 行動終了
        Context.m_StateMachine.SendEvent((int)StateEventType.END_ACT);
    }

    private async UniTask Act()
    {
        // 行動選択
        await m_Action.SelectAsync(Context.m_BattleDataManager, Context.m_ViewManager);

        // ダメージ計算とか
        m_Action.Calc(Context.m_BattleDataManager);

        // 行動反映
        await m_Action.ActionAsync(Context.m_BattleDataManager, Context.m_ViewManager);
    }

    protected override void Update()
    {
        // todo:選択キャンセル
    }

    protected override void Exit()
    {
    }

#region Animations

    /// <summary>
    /// 行動開始アニメーション
    /// </summary>
    private async UniTask ActStartAnimation()
    {
        var actT = Context.m_ViewManager.ActionTimeline;

        List<UniTask> tasks = new List<UniTask>();

        tasks.Add(actT.TopElementMoveAnim());
        tasks.Add(actT.StuffOfFrontElementsAnim());

        await UniTask.WhenAll(tasks);
    }

    /// <summary>
    /// 行動終了アニメーション
    /// </summary>
    private async UniTask ActEndAnimation()
    {
        await Context.m_ViewManager.ActionTimeline.TopElementDropOutAnim();
    }

#endregion Animations
}
}
} // Battle
