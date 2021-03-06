﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Async;
using DG.Tweening;

namespace Battle {

/// <summary>
/// 行動リストのルートビュー
/// </summary>
public class ActionTimelineRootView : MonoBehaviour
{
    [SerializeField] private GameObject m_ActionTimelinePrefab = default;

    /// <summary>
    /// 現在の行動リスト
    /// </summary>
    [SerializeField] private Transform m_CurrentActionTimelineRoot = default;
    public ActionTimelineView CurrentActionTimeline
    {
        get => this.m_CurrentActionTimelineView;
        private set => this.m_CurrentActionTimelineView = value;
    }
    private ActionTimelineView m_CurrentActionTimelineView = default;

    /// <summary>
    /// 初期化
    /// </summary>
    public void SetupView()
    {
        var obj = Instantiate(m_ActionTimelinePrefab, m_CurrentActionTimelineRoot);
        CurrentActionTimeline = obj.GetComponent<ActionTimelineView>();
    }

    /// <summary>
    /// 開始時のアニメーション
    /// </summary>
    /// <returns></returns>
    public async UniTask StartAnim()
    {
        // タイムライン全体のアニメーション
        await m_CurrentActionTimelineView.StartAnim();
    }
}
} // Battle
