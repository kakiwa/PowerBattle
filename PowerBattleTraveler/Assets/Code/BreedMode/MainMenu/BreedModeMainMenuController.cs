using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 育成モードメインメニュー基盤
/// </summary>
public class BreedModeMainMenuController : MonoBehaviour
{
    //メニュー種別
    private enum MenuType
    {
        Training,    // 訓練
        Extra_Training,    // 特別訓練
        Rest,    // 休息
        Skill_Up,    // 能力振り分け
        Option    // 設定
    }

    //ターン情報
    [SerializeField]
    private struct TurnCount{
        int Month;    //　月
        int Week;    //　週
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
