using System.Linq;
using UnityEngine;
using IceMilkTea.Core;
using UniRx.Async;


using UnityEngine.Rendering;
using DG.Tweening;
using Common;

namespace Battle {

public partial class BattleStateManager
{
/// <summary>
/// 終了チェック
/// </summary>
private class EndCheckState : ImtStateMachine<BattleStateManager>.State
{
    protected override async void Enter()
    {
        // 行動リスト更新
        await ActionListUpdate();

        // 勝敗カウント
        int playerCount = 0;
        int enemyCount  = 0;

        foreach (var actor in Context.m_BattleDataManager.Actors) {
            if (actor.Value.Hp <= 0)
            {
                continue;
            }
            var _ = actor.Value.ActorType == ActorType.PLAYER ? ++playerCount : ++enemyCount;
        }

        var nextState
            = (playerCount == 0) || (enemyCount == 0)
            ? StateEventType.BATTLE_END
            : StateEventType.NEXT_ACTOR;

        Debug.Log("行動リスト数:" + Context.m_ActionList.Count);
        // ターン切り替えチェック
        if (Context.m_ActionList.Count == 0) {
            nextState = StateEventType.TURN_START;
        }

        StateMachine.SendEvent((int)nextState);
    }

    protected override void Exit()
    {
    }

    /// <summary>
    /// 行動リストの更新
    /// </summary>
    /// <returns></returns>
    private async UniTask ActionListUpdate()
    {
        
        var action = Context.m_ActionList.First();
        Context.m_NextActionList.Add(action);
        Context.m_ActionList.Remove(action);
        // 行動リストの一つ目をけす


        // 行動リストをずらす

        // 
        await UniTask.Yield();
    }
}

}
} // Battle