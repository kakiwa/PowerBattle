using UnityEngine;
using IceMilkTea.Core;
using UniRx.Async;

namespace Battle {
public partial class BattleStateManager {

/// <summary>
/// 初期化ステート
/// </summary>
private class InitState : ImtStateMachine<BattleStateManager>.State
{
    protected override async void Enter()
    {
        Debug.Log("初期化ステート");

        // 初期化開始
        await StartAsync();
    }

    private async UniTask StartAsync()
    {
        Debug.Log("てきとーな初期化");

        // バトルのデータ初期化
        await Context.m_BattleDataManager.SetupBattleData(0);

        // ビューの初期化
        await Context.m_ViewManager.SetupView(Context.m_BattleDataManager);

        // 最初の行動順を決定
        var actors = Context.m_BattleDataManager.Actors;
        foreach (var it in actors)
        {
            IAction actionCommand = it.Value.ActorType == ActorType.PLAYER ?
                (IAction)new PlayerAction(it.Key) :
                (IAction)new EnemyDiside(it.Key) ;
            Context.m_NextActionList.Add(actionCommand);
        }

        // バトル開始へ
        StateMachine.SendEvent((int)StateEventType.BATTLE_START);
    }

    protected override void Exit()
    {
        Debug.Log("初期化終了");
    }

}
}
} // Battle
