using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx.Async;

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

    public async UniTask StartAnim()
    {
        await UniTask.Yield();
        m_StartEnd.SetActive(true);

        await UniTask.Delay(TimeSpan.FromSeconds(1));

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
