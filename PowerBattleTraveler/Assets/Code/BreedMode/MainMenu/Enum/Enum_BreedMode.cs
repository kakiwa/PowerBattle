/// <summary>
/// 育成モード用Enum
/// </summary>
namespace BreedMode
{
//　メニュー種別
public enum MenuType
{
    NONE = -1,    // default
    TRAINING,    // 訓練
    EXTRA_TRAINING,    // 特別訓練
    REST,    // 休息
    SKILL_UP,    // 能力振り分け
    ITEM,    //　アイテム一覧
    DATA,    //　データ確認
    OPTION    // 設定
}

//　属性
public enum Attribute
{
    NONE,
    FIRE,    //　火属性
    WATER,    //　水属性
    WOOD,    //　木属性
    LIGHT,    //　光属性
    DARKNESS,    //　闇属性
    GOD    //　神属性
}

//　素質
public enum Nature
{
    NONE,
    ORDINARY_PERSON,    //　凡才型
    EXCELLENCE,    //　秀才型
    GENIUS    //　天才型
}

//　状態
public enum BreedStatus
{
    NONE,
    GREAT_CONDITION,    //　絶好調
    FAVORABLE,    // 好調
    USUALLY,    //　普通
    UPSET,    //　不調
    ABSOLUTE_UPSET    //　絶不調
}

//　戦闘タイプ
public enum BattleType
{
    NONE,
    SWORDSMANSHIP,    //　剣術
    TAIJYUTU,    //　体術
    MAGIC,    //　魔法
    SUPPORT,    //　支援
}

}
