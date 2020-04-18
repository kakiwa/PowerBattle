using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        Debug.Log("ターンスタート");
        // ターンを進める
        ++Context.m_BattleDataManager.TurnData.turnCount;
        Context.m_ViewManager.setTurn(Context.m_BattleDataManager.TurnData.turnCount);

        // 行動予約リストを高度リストにコピー
        Context.m_ActionList = new List<IAction>(Context.m_NextActionList);
        // 古い行動リストをクリア
        Context.m_NextActionList.Clear();

        // ターン待ち
        await TurnStart();

        // 行動開始ステートへ
        StateMachine.SendEvent((int)StateEventType.START_ACT);
    }

    private async UniTask TurnStart() {
        await UniTask.Delay(
            TimeSpan.FromSeconds(3)
        );
    }

    protected override void Exit()
    {
    }
}
}
}// Battle
