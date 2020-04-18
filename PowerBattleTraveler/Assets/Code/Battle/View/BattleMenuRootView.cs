using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle {

/// <summary>
/// バトルメニューのルート
/// </summary>
public class BattleMenuRootView : MonoBehaviour
{
    [SerializeField] private GameObject m_BattleMenuPrefab = default;    //< 戦闘コマンドプレファブ

    /// <summary>
    /// バトルコマンドメニュー
    /// </summary>
    public BattleMenuView BattleMenu
    {
        get
        {
            return this.m_BattleMenu;
        }
        private set 
        {
            this.m_BattleMenu = value;
        }
    }
    private BattleMenuView m_BattleMenu = default;

    /// <summary>
    /// セットアップ
    /// </summary>
    public void SetupView() 
    {
        var obj = Instantiate(m_BattleMenuPrefab, this.transform);
        m_BattleMenu = obj.GetComponent<BattleMenuView>();
    }
}
}
