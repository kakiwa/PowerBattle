using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using UniRx.Async;

#region エイリアス
using SpeedData = System.Collections.Generic.KeyValuePair<uint, int>;
using ActorPair = System.Collections.Generic.KeyValuePair<uint, Battle.ActorData>;
#endregion エイリアス

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

        // 行動順を決定
        var actors = Context.m_BattleDataManager.Actors;
        Actions(actors);

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

    private void Actions(Dictionary<uint, ActorData> actors)
    {
        List<SpeedData> speedDataList = new List<SpeedData>();
        // 全アクターのスピードリスト生成
        foreach (var it in actors)
        {
            if (!it.Value.IsAlive) {
                continue;
            }

            speedDataList.Add(new SpeedData(it.Key, it.Value.Speed));
        }

        // TODO:同スピードの判定式も追加
        speedDataList.Sort((a, b) => b.Value - a.Value);

        //

        Context.m_ViewManager.ActionTimeline.clearccc();
        // 生成したスピードリストを元に、タイムラインに追加していく
        foreach (var it in speedDataList) {
            var actor = actors[it.Key];
            IAction actionCommand = actor.ActorType == ActorType.PLAYER ?
                (IAction)new PlayerAction(it.Key) :
                (IAction)new EnemyDiside(it.Key);

            // タイムラインに追加
            Context.m_ViewManager.ActionTimeline.AddElement(new ActorPair(it.Key, actor));

            // 行動リストに追加
            Context.m_NextActionList.Add(it.Key, actionCommand);
        }
    }

#region Animation

    /// <summary>
    /// ターン開始演出
    /// </summary>
    private async UniTask TurnStart()
    {
        // ターンを進める
        ++Context.m_BattleDataManager.TurnData.turnCount;
        Context.m_ViewManager.setTurn(Context.m_BattleDataManager.TurnData.turnCount);


        await Context.m_ViewManager.ActionTimeline.TurnStartAnim();
    }

    /// <summary>
    /// 行動リストを反映
    /// </summary>
    private async UniTask ActionTimelineSwap()
    {
        // 行動予約リストを行動リストにコピー
        Context.m_ActionList = new Dictionary<uint, IAction>(Context.m_NextActionList);
        // 古い行動予約リストをクリア
        Context.m_NextActionList.Clear();

        await UniTask.Yield();
    }

#endregion Animation
}
}
}// Battle
