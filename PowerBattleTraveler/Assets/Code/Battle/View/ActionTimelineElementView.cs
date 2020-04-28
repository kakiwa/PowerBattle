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
    [SerializeField] private Image m_CharactrImage = default;

    /// <summary>
    /// 敵か味方のどっちサイドかの色イメージ
    /// </summary>
    [SerializeField] private Image m_SideImage = default;

    public float Width { get =>  m_CharactrImage.rectTransform.sizeDelta.x * m_CharactrImage.transform.lossyScale.x; }

    public void SetCo(Color c)
    {
        // ちょっと薄くする
        c.a = 0.3f;
        m_SideImage.color = c;
    }

    public void SetImage(Sprite sprite)
    {
        m_CharactrImage.overrideSprite = sprite;
    }

    public void Focus() {
        this.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.1f);
    }

}
} // Battle
