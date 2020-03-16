using UnityEngine;
using UnityEngine.SceneManagement;
using IceMilkTea.Core;
using UniRx.Async;

namespace Battle {

public partial class BattleStateManager
{
    private class ResultState : ImtStateMachine<BattleStateManager>.State
    {
        protected override async void Enter()
        {
            await ResultAsync();
        }

        private async UniTask ResultAsync()
        {
            Debug.Log("戦闘終了");
            await UniTask.Delay(System.TimeSpan.FromSeconds(1));
            Debug.Log("次のシーンへ");
        }
    }
}
}// Battle
