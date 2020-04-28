using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle {

/// <summary>
/// ターン数表示のビュー
/// </summary>
public class TurnView : MonoBehaviour
{
    [SerializeField]
    private Text m_TurnNum = default;

    private void Awake()
    {
        // ターンを0にしとく
        m_TurnNum.text = 0.ToString();
    }

    /// <summary>
    /// ターンをセット
    /// </summary>
    public void SetTurn(int turn)
    {
        m_TurnNum.text = turn.ToString();
    }
}

} // Battle
