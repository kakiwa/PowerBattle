using UnityEngine;
using UnityEngine.SceneManagement;
using IceMilkTea.Core;
using UniRx.Async;

namespace Battle {

public partial class BattleStateManager
{
/// <summary>
/// 結果発表ステート
/// </summary>
private class ResultState : ImtStateMachine<BattleStateManager>.State
{
    protected override async void Enter()
    {
        await ResultAsync();
    }

    /// <summary>
    /// リザルトの演出
    /// </summary>
    private async UniTask ResultAsync()
    {
        Debug.Log("戦闘終了");
        await Context.m_ViewManager.StartEnd().EndAnim();
        Debug.Log("次のシーンへ");
    }
}
}
}// Battle
