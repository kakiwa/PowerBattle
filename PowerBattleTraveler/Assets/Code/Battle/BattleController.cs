using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// バトルを司る
/// </summary>
public class BattleController : MonoBehaviour, IBattleEvents
{
    public void OnMenu()
    {
        Debug.Log("メニューボタンが押された！");
    }

    public void OnPause()
    {
        Debug.Log("ポーズボタンがおささった！");
    }

}
