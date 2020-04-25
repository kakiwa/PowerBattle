using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx.Async;

namespace Battle {

/// <summary>
/// バトルの表示を各ビューに伝える
/// </summary>
public class BattleViewManager : MonoBehaviour
{
    /// <summary>
    /// ターン表示
    /// </summary>
    [SerializeField] private TurnRootView m_TurnRootView = default;

    /// <summary>
    /// バトルの開始終了
    /// </summary>
    [SerializeField] private StartEndRootView m_StartEndRootView = default;
    public StartEndRootView StartEnd { get => m_StartEndRootView; }

    /// <summary>
    /// バトルコマンドメニュー
    /// </summary>
    [SerializeField] private BattleMenuRootView m_BattleMenuRootView = default;
    public BattleMenuView BattleMenu { get => m_BattleMenuRootView.BattleMenu; }

    /// <summary>
    /// 味方情報
    /// </summary>
    [SerializeField] private BattleAllyInfoRootView m_BattleAllyInfoRootView = default;

    /// <summary>
    /// 行動リスト
    /// </summary>
    [SerializeField] private ActionTimelineRootView m_ActionTimelineRootView = default;
    public ActionTimelineRootView ActionTimelineRootView { get => m_ActionTimelineRootView; }
    public ActionTimelineView ActionTimeline { get => m_ActionTimelineRootView.CurrentActionTimeline; }


    /// <summary>
    /// アクターリスト
    /// </summary>
    [SerializeField] private ActorsRootView m_ActorsRootView = default;
    public ActorsRootView ActorsRootView { get => m_ActorsRootView; }

    /// <summary>
    /// Viewの初期化
    /// </summary>
    public void SetupView(BattleDataManager battleData)
    {
        // キャラのView初期化
        foreach (var actor in battleData.Actors)
        {
            Debug.Log(actor.Key);
            // アクターの3Dデータ追加
            m_ActorsRootView.AddActor(actor);

            // 味方のみUI追加
            if (actor.Value.ActorType == ActorType.PLAYER)
            {
                m_BattleAllyInfoRootView.AddAlly(actor);
            }
        }
        // ターン表記の初期化
        m_TurnRootView.SetupView();

        // バトルの開始終了
        m_StartEndRootView.SetupView();

        // バトルメニュー
        m_BattleMenuRootView.SetupView();

        // 行動リスト
        m_ActionTimelineRootView.SetupView();
    }





    public void setTurn(int turn)
    {
        m_TurnRootView.SetTurn(turn);
    }
}
} // Battle
