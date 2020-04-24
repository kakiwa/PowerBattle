using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle {

/// <summary>
/// 行動リスト
/// </summary>
public class ActionTimelineView : MonoBehaviour
{
    
    [SerializeField] private GameObject m_ElementPrefab = default;

    /// <summary>
    /// 先頭のアクターのルート
    /// </summary>
    [SerializeField] private Transform m_TopElementRoot = default;
    /// <summary>
    /// その他のアクターのルート
    /// </summary>
    [SerializeField] private Transform m_ElementsRoot = default;

    private List<ActionTimelineElementView> m_ElementList = default;

    
    void AddElement()
    {
        
    }
}
} // Battle
