using System.Linq;
using UnityEngine;
using IceMilkTea.Core;
using UniRx.Async;


using UnityEngine.Rendering;
using DG.Tweening;
using Common;
using System.Collections.Generic;

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

        List<uint> deathActorIdList = new List<uint>();

        // ステータスチェック
        foreach (var actor in Context.m_BattleDataManager.Actors)
        {
            if (actor.Value.Hp <= 0)
            {
                if (actor.Value.IsAlive)
                {
                    deathActorIdList.Add(actor.Key);
                }
                continue;
            }
            var _ = actor.Value.ActorType == ActorType.PLAYER ? ++playerCount : ++enemyCount;
        }

        // 死亡アニメーション
        if (deathActorIdList.Count != 0)
        {
            await DeathAnim(deathActorIdList);
        }

        // 基本は次のキャラへ
        StateEventType nextState = StateEventType.NEXT_ACTOR;
        // ターン切り替えチェック
        if (Context.m_ActionList.Count == 0) {
            TurnEnd();
            nextState = StateEventType.TURN_START;
        }

        // 戦闘終了チェック
        if (playerCount == 0 || enemyCount == 0)
        {
            nextState = StateEventType.BATTLE_END;
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
        // 行動リストの一つ目をけす
        var action = Context.m_ActionList.First();
        Context.m_ActionList.Remove(action.Key);

        //
        await UniTask.Yield();
    }

    private void TurnEnd()
    {
        Context.m_ViewManager.ActionTimeline.ResetElementsAlpha();
    }

#region Animation

    private async UniTask DeathAnim(List<uint> deathActorIdList)
    {
        List<UniTask> tasks = new List<UniTask>();
        foreach (var deathId in deathActorIdList)
        {
            // タイムラインから除く
            tasks.Add(Context.m_ViewManager.ActionTimeline.RemoveAnim(deathId));
            //TODO アクターモデルの死亡も追加
            Context.m_ViewManager.ActorsRootView.RemoveActor(deathId);

            // 行動リストデータからも消す
            Context.m_ActionList.Remove(deathId);

            // データ更新
            var d = Context.m_BattleDataManager.Actors[deathId];
            d.IsAlive = false;
            Context.m_BattleDataManager.Actors[deathId] = d;
        }
        await UniTask.WhenAll(tasks);
    }

#endregion Animation
}

}
} // Battle