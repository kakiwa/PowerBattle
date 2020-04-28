using System.Collections.Generic;
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
        Debug.Log("初期化開始");

        // 初期化開始
        Setup();

        // 開始演出
        await StartAsync();

        // バトル開始へ
        StateMachine.SendEvent((int)StateEventType.BATTLE_START);
    }

    protected override void Exit()
    {
        Debug.Log("初期化終了");
    }

    private void Setup()
    {
        // バトルのデータ初期化
        Context.m_BattleDataManager.SetupBattleData(0);

        // ビューの初期化
        Context.m_ViewManager.SetupView(Context.m_BattleDataManager);
    }

    /// <summary>
    /// バトル開始演出
    /// </summary>
    /// <returns></returns>
    private async UniTask StartAsync()
    {
        // 演出リスト
        List<UniTask> tasks = new List<UniTask>();

        // バトル開始演出
        tasks.Add(Context.m_ViewManager.StartEnd.StartAnim());

        // 行動リスト開始演出
        tasks.Add(Context.m_ViewManager.ActionTimelineRootView.StartAnim());

        // 味方情報開始演出


        // アクター開始演出


        // 登録した演出を全部やる
        await UniTask.WhenAll(tasks);
    }




}
}
} // Battle
