using UnityEngine;
using IceMilkTea.Core;
using UniRx.Async;

namespace Battle {

public partial class BattleStateManager 
{
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
            await UniTask.Delay(System.TimeSpan.FromSeconds(1));

            // バトルのデータ初期化
            await Context.m_BattleDataManager.SetupBattleData(0);

            // ビューの初期化
            await Context.m_ViewManager.SetupView(Context.m_BattleDataManager);

            // 終了したら次のステートへ
            StateMachine.SendEvent((int)StateType.BATTLE);
        }

        protected override void Exit()
        {
            Debug.Log("初期化終了");
        }

    }
}

} // Battle
