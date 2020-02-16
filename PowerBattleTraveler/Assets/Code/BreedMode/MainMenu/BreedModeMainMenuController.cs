using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 育成モードメインメニュー基盤
/// </summary>
namespace BreedMode
{
public class BreedModeMainMenuController : MonoBehaviour,IMainMenuEvents
{
    #region  // 変数
    //　セレクトUI
    [SerializeField]
    private BreedModeMainMenuSelect m_breedModeMenuSelect = default;

    //　メニュー種別
    private MenuType m_menuType = default;

    //　ターン情報
    public struct TurnData
    {
        public int year { get; set; }    //　年
        public int month { get; set; }    //　月
        public int week { get; set; }    //　週
        public int turnCount { get; set; }    //　ターン数
    }
    [SerializeField]
    private TurnData m_turnData = default;

    //　プレイヤー基礎ステータス
    public PlayerBattleData m_playerBattleData = default;

    //　育成モード用ステータス
    public BreedData m_breedModeData = default;
    #endregion

    void Awake()
    {
        //ダミーデータの取得
        m_turnData = DummyDataTurnDataInput();
        m_breedModeData = DummyBreedDataInput();
        m_playerBattleData = DummyPlayerDataInput();

        m_menuType = MenuType.TRAINING;
        m_breedModeMenuSelect.SetButtonSetting(m_menuType);
        m_breedModeMenuSelect.SetMenuText(m_breedModeData.schoolName,m_turnData);
        m_breedModeMenuSelect.SetStatusText(m_playerBattleData);

    }

    void Update()
    {
        Debug.Log(m_playerBattleData.hp);
    }

    #region  //　ダミーデータ
    /// <summary>
    /// ダミーデータ挿入関数
    /// </summary>
    private TurnData DummyDataTurnDataInput()
    {
        TurnData data = new TurnData();
        
        data.year = 1;
        data.month = 1;
        data.week = 1;
        data.turnCount = 1;
        return  data;
    } 

    private PlayerBattleData DummyPlayerDataInput()
    {
        PlayerBattleData data = new PlayerBattleData();

        data.name = "おバカさん";
        data.attribute = Attribute.GOD;
        data.battleType = BattleType.SWORDSMANSHIP;
        data.hp = 100;
        data.mp = 2;
        data.physicalAttack = 120;
        data.physicalDefense = 200;
        data.magicAttack = 2;
        data.magicDefense = 10;
        data.speed = 56;
        data.physicalOperationForce = 20;
        data.magicOperationForce = 2;
        SkillData[] skillList = 
        {
            new SkillData(1,2),
            new SkillData(2,3),
        };
        data.skill = skillList;

        return data;
    }

    private BreedData DummyBreedDataInput()
    {
        BreedData data = new BreedData();

        data.schoolName = "おバカ専門学校";
        data.nature = Nature.GENIUS;
        data.status = BreedStatus.GREAT_CONDITION;
        data.hpMax = 100;
        data.hp = 60;
        BreedPoints breedPoints = new BreedPoints(100,100,100,100,100,100);
        
        return data;
    }
    #endregion

    /// <summary>
    /// シーン遷移関数
    /// </summary>
    public void OnChangeScene(string sceneName)
    {
        //　シーン遷移イベントを呼ぶ
        Debug.Log(sceneName + "にシーン遷移するよ〜");
    }

}
}
