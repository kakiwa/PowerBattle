using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle {
    
/// <summary>
/// 行動リストの要素
/// </summary>
public class ActionTimelineElementView : MonoBehaviour
{
    [SerializeField] private Image m_Image = default;

    public void SetCo(Color c)
    {
        m_Image.color = c;
    }

    public void Focus() {
        this.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.1f);
    }

}
} // Battle
