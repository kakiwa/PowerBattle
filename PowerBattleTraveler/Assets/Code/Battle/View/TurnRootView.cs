using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle {

public class TurnRootView : MonoBehaviour
{
    [SerializeField] private GameObject m_TurnPrefab = default;

    private TurnView m_TurnView = default;


    /// <summary>
    /// セットアップ
    /// </summary>
    public void SetupView()
    {
        m_TurnView = Instantiate(m_TurnPrefab, this.transform).GetComponent<TurnView>();
    }

    /// <summary>
    /// ターン数セット
    /// </summary>
    public void SetTurn(int turn)
    {
        m_TurnView.SetTurn(turn);
    }
}
} // Battle
