using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BreedMode
{
/// <summary>
/// メニューボタン
/// </summary>
public class SelectMenuButton : MonoBehaviour
{
    // コントローラー
    [SerializeField]
    private BreedModeMainMenuController m_controller = default;
    [SerializeField]
    private string m_buttonName = default;
    /// <summary>
    /// ボタンが押された時の処理
    /// </summary>
    public void OnClick()
    {
        if (m_controller)
        {
            ExecuteEvents.Execute<IMainMenuEvents>(
                m_controller.gameObject,
                null,
                (x, data) => x.OnChangeScene(m_buttonName)
            );
        }
    }
}
}
