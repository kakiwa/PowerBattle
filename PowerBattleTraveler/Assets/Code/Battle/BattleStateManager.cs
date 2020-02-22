using System;
using UnityEngine;
using IceMilkTea.Core;
using UniRx.Async;

namespace Battle {
    
/// <summary>
/// バトルのステート
/// </summary>
public class BattleStateManager : MonoBehaviour
{
    private enum StateType : int
    {
        INIT,
        BATTLE,
        RESULT,
    }

    [SerializeField]
    private BattleViewManager m_ViewManager = default;

    private ImtStateMachine<BattleStateManager> m_StateMachine;

    private void Awake()
    {
        // ステートの遷移図を列挙していく
        m_StateMachine = new ImtStateMachine<BattleStateManager>(this);
        m_StateMachine.AddAnyTransition<InitState>((int)StateType.INIT);
        m_StateMachine.AddAnyTransition<BattleState>((int)StateType.BATTLE);
        m_StateMachine.AddAnyTransition<ResultState>((int)StateType.RESULT);
        // m_StateMachine.AddTransition<InitState, BattleState>((int)StateType.INIT);
        // m_StateMachine.AddTransition<InitState, BattleState>((int)StateType.BATTLE);
        // m_StateMachine.AddTransition<InitState, BattleState>((int)StateType.BATTLE);
        Debug.Log("ステート初期化");

        // ステートスタート
        m_StateMachine.SetStartState<InitState>();
    }

    private void Update()
    {
        m_StateMachine.Update();
    }

#region インナークラス

    /// <summary>
    /// 初期化ステート
    /// </summary>
    private class InitState : ImtStateMachine<BattleStateManager>.State
    {
        protected override void Enter()
        {
            Debug.Log("初期化ステート");

            // 初期化開始
            UniTask.Run(StartAsync);
        }

        protected override void Update()
        {
            
        }

        private async UniTask StartAsync()
        {
            await tes();

            StateMachine.SendEvent((int)StateType.BATTLE);
        }

        private async UniTask tes()
        {
            Debug.Log("ここからスタート");

            await UniTask.Delay(TimeSpan.FromSeconds(2));

            Debug.Log("待ったよ");
        }

        protected override void Exit()
        {
            Debug.Log("初期化終了");
        }

    }
    private class BattleState : ImtStateMachine<BattleStateManager>.State
    {
        protected override void Enter()
        {
            Debug.Log("戦闘開始");
            
            UniTask.Run(WaitAnimation);
        }

        protected override void Update()
        {

        }


        protected override void Exit()
        {

        }

        private async UniTask WaitAnimation()
        {
            var isNext = await Context.m_ViewManager.AnimationAsync();
            if (isNext)
            {  
                Debug.Log("つぎへ");
                StateMachine.SendEvent((int)StateType.RESULT);
            }
            else 
            {
                Debug.Log("あちゃー");
            }
        }
        
    }

    private class ResultState : ImtStateMachine<BattleStateManager>.State
    {
        protected override void Enter()
        {
            Debug.Log("戦闘終了");
        }

        protected override void Exit()
        {
            Debug.Log("次のシーンへ");
        }
    }
#endregion インナークラス
}

} // Battle
