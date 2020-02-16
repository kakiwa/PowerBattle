using UnityEngine;

/// <summary>
/// 育成モードプレイヤーキャラクター基礎ステータス情報クラス
/// </summary>
namespace BreedMode
{
/// <summary>
/// プレイヤーキャラクター基礎バトルステータス
/// </summary>
[System.Serializable]
public struct PlayerBattleData
{
    public PlayerBattleData(string name, Attribute attribute, BattleType battleType, int hp, int mp, int physicalAttack, int physicalDefense, int magicAttack, int magicDefense, int speed, int physicalOperationForce, int magicOperationForce, SkillData[] skill)
    {
        this.name = name;
        this.attribute = attribute;
        this.battleType = battleType;
        this.hp = hp;
        this.mp = mp;
        this.physicalAttack = physicalAttack;
        this.physicalDefense = physicalDefense;
        this.magicAttack = magicAttack;
        this.magicDefense = magicDefense;
        this.speed = speed;
        this.physicalOperationForce = physicalOperationForce;
        this.magicOperationForce = magicOperationForce;
        this.skill = skill;
    }

    public string name { get; set; }    //　名前
    public Attribute attribute { get; set; }    // 属性
    public BattleType battleType { get; set; }    //　戦闘タイプ
    public int hp { get; set; }    //　体力
    public int mp { get; set; }     //　マジックパワー
    public int physicalAttack { get; set; }     //　物理攻撃力
    public int physicalDefense { get; set; }     //　物理防御力
    public int magicAttack { get; set; }     //　魔法攻撃力
    public int magicDefense { get; set; }     //　魔法防御力
    public int speed { get; set; }     //　スピード
    public int physicalOperationForce { get; set; }     //　物理操作力
    public int magicOperationForce { get; set; }     //　魔法操作力
    public SkillData[] skill { get; set; }    //　習得済みスキル
}

/// <summary>
/// スキルデータ構造体
/// </summary>
public struct SkillData
{
    public SkillData(int id, int level)
    {
        this.id = id;
        this.level = level;
    }

    public int id { get; set; }    //　スキルID
    public int level { get; set; }    //　スキルレベル
}

/// <summary>
/// 育成モード用ステータス
/// </summary>
public struct BreedData
{
    public BreedData(string schoolName, Nature nature, int hpMax, int hp, BreedStatus status, BreedPoints breedPoints)
    {
        this.schoolName = schoolName;
        this.nature = nature;
        this.hpMax = hpMax;
        this.hp = hp;
        this.status = status;
        this.breedPoints = breedPoints;
    }

    public string schoolName { get; set; }    //　学校名
    public Nature nature { get; set; }    //　素質
    public int hpMax { get; set; }    //　体力上限
    public int hp { get; set; }    //　体力
    public BreedStatus status { get; set; }   //　状態
    public BreedPoints breedPoints { get; set; }     //　育成ポイント

}

/// <summary>
/// 育成ポイント構造体
/// </summary>
public struct BreedPoints
{
    public BreedPoints(int physicalStrength, int magic, int aggression, int defense, int speed, int fortune)
    {
        this.physicalStrength = physicalStrength;
        this.magic = magic;
        this.aggression = aggression;
        this.defense = defense;
        this.speed = speed;
        this.fortune = fortune;
    }

    public int physicalStrength { get; set; }    //　体力
    public int magic { get; set; }    //　魔力
    public int aggression { get; set; }    //　攻力
    public int defense { get; set; }    //　守力
    public int speed { get; set; }    //　速力
    public int fortune { get; set; }    //　運力
}
}
