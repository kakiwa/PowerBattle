using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx.Async;
using DG.Tweening;
using Common;

namespace Battle {

/// <summary>
/// バトルの開始と終了演出のルート
/// </summary>
public class StartEndRootView : MonoBehaviour
{
    [SerializeField] private GameObject m_StartEndPrefab = default;

    private GameObject m_StartEnd;

    public void SetupView()
    {
        m_StartEnd = Instantiate(m_StartEndPrefab, this.transform);
        m_StartEnd.SetActive(false);
    }

    /// <summary>
    /// 戦闘開始演出
    /// </summary>
    /// <returns></returns>
    public async UniTask StartAnim()
    {
        await UniTask.Yield();
        m_StartEnd.SetActive(true);

        var tasks = new List<UniTask>();
        await DOTween.ToAlpha(
                ()=> m_StartEnd.GetComponent<Text>().color,
                color => m_StartEnd.GetComponent<Text>().color = color,
            0.0f,
            0.0f);

        await DOTween.ToAlpha(
                ()=> m_StartEnd.GetComponent<Text>().color,
                color => m_StartEnd.GetComponent<Text>().color = color,
            1.0f,
            1.0f);

        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

        await DOTween.ToAlpha(
                ()=> m_StartEnd.GetComponent<Text>().color,
                color => m_StartEnd.GetComponent<Text>().color = color,
            0.0f,
            1.0f);

        m_StartEnd.SetActive(false);
    }

    public async UniTask EndAnim()
    {
        await UniTask.Yield();
        m_StartEnd.GetComponent<Text>().text = "ばとるしゅりょー";

        m_StartEnd.SetActive(true);

        await UniTask.Delay(TimeSpan.FromSeconds(1));

        m_StartEnd.SetActive(false);
    }
}
} // Battle
