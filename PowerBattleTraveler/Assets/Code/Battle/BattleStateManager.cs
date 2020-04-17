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
    /// <summary>
    /// ステートを遷移するためのイベント名
    /// </summary>
    private enum StateEventType : int
    {
        BATTLE_START,   //< バトル開始
        START_ACT,      //< 行動開始
        END_ACT,        //< 行動終了
        NEXT_ACTOR,     //< 次のキャラクターへ
        TURN_START,     //< ターンが進む
        BATTLE_END,     //< バトル終了
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Awake()
    {
        // ステートの遷移図を列挙していく
        m_StateMachine = new ImtStateMachine<BattleStateManager>(this);

        m_StateMachine.AddTransition<InitState, TurnStartState>((int)StateEventType.BATTLE_START);
        m_StateMachine.AddTransition<TurnStartState, ActState>((int)StateEventType.START_ACT);
        m_StateMachine.AddTransition<ActState, EndCheckState>((int)StateEventType.END_ACT);
        m_StateMachine.AddTransition<EndCheckState, ActState>((int)StateEventType.NEXT_ACTOR);
        m_StateMachine.AddTransition<EndCheckState, TurnStartState>((int)StateEventType.TURN_START);
        m_StateMachine.AddTransition<EndCheckState, ResultState>((int)StateEventType.BATTLE_END);

        // ステートスタート
        m_StateMachine.SetStartState<InitState>();
    }

    /// <summary>
    /// 自身を制御するステートマシン
    /// </summary>
    private ImtStateMachine<BattleStateManager> m_StateMachine = default;

    /// <summary>
    /// Viewマネージャ
    /// </summary>
    [SerializeField]
    private BattleViewManager m_ViewManager = default;

    /// <summary>
    /// データマネージャ
    /// </summary>
    private BattleDataManager m_BattleDataManager = new BattleDataManager();

    /// <summary>
    /// 行動のリスト
    /// </summary>
    List<IAction> m_ActionList = new List<IAction>();
    List<IAction> m_NextActionList = new List<IAction>();

    /// <summary>
    /// 全体の更新
    /// </summary>
    private void Update()
    {
        m_StateMachine.Update();
    }
}

} // Battle
