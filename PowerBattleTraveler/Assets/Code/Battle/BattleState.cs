using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battle {

interface IAction
{
    void OnAction();
    void OnDraw();
}

public class PlayerAttack : IAction
{
    public void OnAction()
    {
        Debug.Log("ここでプレイヤー攻撃のロジック");
    }

    public void OnDraw()
    {
        Debug.Log("ここでプレイヤー攻撃演出");
    }
}

public class ItemUse : IAction
{
    public void OnAction()
    {
        Debug.Log("アイテム使用のロジック");
    }

    public void OnDraw()
    {
        Debug.Log("アイテム使用の演出");
    }
}

class BattleStateManager2
{
    public bool IsBattleEnd()
    {
        return m_StateStream.Count == 0;
    }

    public void AddAction(IAction action) 
    {
        m_StateStream.Add(action);
    }

    public void StateUpdate()
    {
        if (m_StateStream.Count != 0){
            m_StateStream[0].OnAction();

            m_StateStream[0].OnDraw();

            m_StateStream.RemoveAt(0);
        }
        
    }

    private List<IAction> m_StateStream = new List<IAction>();
}

}
