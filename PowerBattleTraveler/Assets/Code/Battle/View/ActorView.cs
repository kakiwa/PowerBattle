using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle {
public class ActorView : MonoBehaviour
{

    [SerializeField]
    private Image image;

    [SerializeField]
    private Text m_ActorName;
    
    [SerializeField]
    private Text m_Hp;
    
    [SerializeField]
    private Text m_Attack;

    public void setCo(Color col)
    {
        image.color = col;
    }

    public void setName(string name) 
    {
        m_ActorName.text = name;
    }

    public void setAttack(int attack)
    {
        m_Attack.text = attack.ToString();
    }

    public void setHp(int hp)
    {
        m_Hp.text = hp.ToString();
    }

}

} // Battle
