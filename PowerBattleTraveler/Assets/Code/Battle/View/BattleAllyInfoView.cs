using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle {

/// <summary>
/// 味方の情報UI
/// </summary>
public class BattleAllyInfoView : MonoBehaviour
{
    [SerializeField] private Image m_Image = default;
    
    [SerializeField] private Text m_Hp = default;

    [SerializeField] private Text m_Mp = default;

    public void setImage(Sprite sprite)
    {
        m_Image.overrideSprite = sprite;
    }

    public void setHp(int hp)
    {
        m_Hp.text = hp.ToString();
    }

    public void setMp(int mp)
    {
        m_Mp.text = mp.ToString();
    }
}
} // Battle
