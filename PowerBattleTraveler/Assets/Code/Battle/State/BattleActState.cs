using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using IceMilkTea.Core;
using UniRx.Async;
using System;

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
        await Act();
    }

    private async UniTask Act() 
    {
        // 行動選択
        await m_Action.SelectAsync(Context.m_BattleDataManager, Context.m_ViewManager);

        // ダメージ計算とか
        m_Action.Calc(Context.m_BattleDataManager);

        // 行動反映
        await m_Action.ActionAsync(Context.m_BattleDataManager, Context.m_ViewManager);

        // 次の行動リストに追加
        Context.m_NextActionList.Add(m_Action);

        // 行動終了
        Context.m_ActionList.Remove(m_Action);
        Context.m_StateMachine.SendEvent((int)StateEventType.END_ACT);
    }

    protected override void Update()
    {
        // todo:選択キャンセル
    }

    protected override void Exit()
    {
    }
}
}
} // Battle
