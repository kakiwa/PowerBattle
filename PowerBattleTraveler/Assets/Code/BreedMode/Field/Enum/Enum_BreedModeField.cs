/// <summary>
/// 育成モード用Enum
/// </summary>
namespace BreedMode
{
//　マス種別
public enum MassType
{
    NONE,
    BATTLE,    //　バトル
    BOSSBATTLE,     //　ボスバトル
    EVENT,    //　イベント
    TREASUREBOX,    //　宝箱
    EXTRA    //　特殊
}

//　レアリティ
public enum Rarely
{
    NONE,
    NORMAL,    //　ノーマル
    RARE,    //　レア
    SUPRERARE,    //　スーパーレア
    ULTRARARE    //　ウルトラレア
}

//　マス表示画像ベース
public enum　MassTextureMount
{
    NONE,
    KUSA,
    MORI,
    IWAYAMA,
    KARI
}




}