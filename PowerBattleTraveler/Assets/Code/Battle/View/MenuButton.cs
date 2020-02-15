using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// メニューボタン
/// </summary>
public class MenuButton : MonoBehaviour
{

    [SerializeField]
    private BattleController controller;
    void Start() {
        
    }

    /// <summary>
    /// ボタンが押された時の処理
    /// </summary>
    public void OnClick() {
        if (controller) {
            ExecuteEvents.Execute<IBattleEvents>(
                controller.gameObject,
                null,
                (x,data)=>x.OnMenu()
            );
        }
    }
}
