using UnityEngine;
using IceMilkTea.Core;
using UniRx.Async;

namespace Battle {

public partial class BattleStateManager
{
/// <summary>
/// 終了チェック
/// </summary>
private class EndCheckState : ImtStateMachine<BattleStateManager>.State
{
    protected override void Enter()
    {
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
        if (Context.m_ActionList.Count == 0) {
            nextState = StateEventType.TURN_START;
            StateMachine.SendEvent((int)nextState);
            return;
        }

        StateMachine.SendEvent((int)nextState);
    }

    protected override void Exit()
    {
    }
}

}
} // Battle