using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using UniRx.Async;

namespace Battle {

public partial class BattleStateManager
{
/// <summary>
/// ターン開始ステート
/// </summary>
private class TurnStartState : ImtStateMachine<BattleStateManager>.State
{
    protected override async void Enter()
    {
        Debug.Log("ターンスタート準備");

        // todo: 同時にはしらせてもいい説
        // 行動リスト更新
        await ActionTimelineSwap();
        // ターン開始演出待ち
        await TurnStart();

        // 行動開始ステートへ
        StateMachine.SendEvent((int)StateEventType.START_ACT);
    }

    protected override void Exit()
    {
        Debug.Log("ターンスタート");
    }

    /// <summary>
    /// ターン開始演出
    /// </summary>
    private async UniTask TurnStart()
    {
        // ターンを進める
        ++Context.m_BattleDataManager.TurnData.turnCount;
        Context.m_ViewManager.setTurn(Context.m_BattleDataManager.TurnData.turnCount);

        // todo: てきとーな演出
        await UniTask.Delay(
            TimeSpan.FromSeconds(1)
        );
    }

    /// <summary>
    /// 行動リストを反映
    /// </summary>
    private async UniTask ActionTimelineSwap()
    {
        // 行動予約リストを行動リストにコピー
        Context.m_ActionList = new List<IAction>(Context.m_NextActionList);
        // 古い行動予約リストをクリア
        Context.m_NextActionList.Clear();



        await UniTask.Yield();
    }
}
}
}// Battle
