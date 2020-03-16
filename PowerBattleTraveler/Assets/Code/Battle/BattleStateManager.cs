using System;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using UniRx.Async;

namespace Battle {
    
/// <summary>
/// バトルのステート
/// </summary>
public partial class BattleStateManager : MonoBehaviour
{
    private enum StateType : int
    {
        INIT,
        CHECK,
        BATTLE,
        SELECTION,
        ACTION,
        RESULT,
    }

    /// <summary>
    /// Viewをまとめたやつ
    /// </summary>
    [SerializeField]
    private BattleViewManager m_ViewManager = default;

    /// <summary>
    /// 自身を制御するステート
    /// </summary>
    private ImtStateMachine<BattleStateManager> m_StateMachine = default;

    /// <summary>
    /// データ周り
    /// </summary>
    private BattleDataManager m_BattleDataManager = new BattleDataManager();

    private void Awake()
    {
        // ステートの遷移図を列挙していく
        m_StateMachine = new ImtStateMachine<BattleStateManager>(this);

        m_StateMachine.AddAnyTransition<InitState>((int)StateType.INIT);
        m_StateMachine.AddAnyTransition<BattleState>((int)StateType.BATTLE);
        m_StateMachine.AddAnyTransition<ResultState>((int)StateType.RESULT);

        // ステートスタート
        m_StateMachine.SetStartState<InitState>();
    }

    private void Update()
    {
        m_StateMachine.Update();
    }

}

} // Battle
