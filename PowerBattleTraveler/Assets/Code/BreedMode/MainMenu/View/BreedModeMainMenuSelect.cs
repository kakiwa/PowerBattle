using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BreedMode
{
/// <summary>
/// 育成モードメインメニューセレクトUI
/// </summary>
public class BreedModeMainMenuSelect : MonoBehaviour
{
    //　メニューUIボタン
    [SerializeField]
    private Button[] m_button = default;

    //学校名
    [SerializeField]
    private Text m_schoolName = default;

    //　ターン数関連
    [SerializeField]
    private Text m_turnCount = default;

    //　ステータス表示
    [SerializeField]
    private Text m_name = default;
    [SerializeField]
    private Text m_hp = default;
    [SerializeField]
    private Text m_mp = default;
    [SerializeField]
    private Text m_physicalAttack = default;
    [SerializeField]
    private Text m_physicalDefence = default;
    [SerializeField]
    private Text m_magicAttack = default;
    [SerializeField]
    private Text m_magicDefence = default;
    [SerializeField]
    private Text m_speed = default;
    [SerializeField]
    private Text m_physicalOperationForce = default;
    [SerializeField]
    private Text m_magicOperationForce = default;


    //　セレクトしているボタン番号
    private int m_selectButtonNum = default;

    void Start()
    {
        // 最初に選択状態にしたいボタンの設定
        m_button[m_selectButtonNum].Select();
    }

    /// <summary>
    /// 選択セレクトUIの設定
    /// </summary>
    public void SetButtonSetting(MenuType menuType)
    {
        m_selectButtonNum = (int)menuType;
    }

    /// <summary>
    /// 表示テキストの変更
    /// </summary>
    public void SetMenuText(string schoolName,BreedModeMainMenuController.TurnData turnData)
    {
        m_schoolName.text = schoolName; 
        m_turnCount.text = string.Format("{0}年目　{1}月　{2}日",turnData.year,turnData.month,turnData.week);

    }

    /// <summary>
    /// 表示ステータスの変更
    /// </summary>
    public void SetStatusText(PlayerBattleData playerBattleData)
    {
        m_name.text = playerBattleData.name;
        m_hp.text = playerBattleData.hp.ToString();
        m_mp.text = playerBattleData.mp.ToString();
        m_physicalAttack.text = playerBattleData.physicalAttack.ToString();
        m_physicalDefence.text = playerBattleData.physicalDefense.ToString();
        m_magicAttack.text = playerBattleData.magicAttack.ToString();
        m_magicDefence.text = playerBattleData.magicDefense.ToString();
        m_speed.text = playerBattleData.speed.ToString();
        m_physicalOperationForce.text = playerBattleData.physicalOperationForce.ToString();
        m_magicOperationForce.text = playerBattleData.magicOperationForce.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        //PlayerInput();
    }

}
}
