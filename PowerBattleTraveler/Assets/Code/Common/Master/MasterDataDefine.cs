
/// <summary>
///　マスターデータの構造体定義
///</summary>

namespace Common.Master
{

/// <summary>
///　マス情報（レーン）
///</summary>
[System.Serializable]
public struct BreedFieldLaneMaster
{
    public BreedFieldLaneMaster(int id, int fieldId, int lane, int massSize)
    {
        this.id = id;
        this.fieldId = fieldId;
        this.lane = lane;
        this.massSize = massSize;
    }

    //　ID
    public int id { get; }
    //　フィールドID
    public int fieldId { get; }
    //　レーン番号
    public int lane { get; }
    //　マス数
    public int massSize { get; }

}

/// <summary>
///　マス情報（マス内部）
///</summary>
[System.Serializable]
public struct BreedFieldMassMaster
{
    public BreedFieldMassMaster(int id, int fieldId, int laneId, string image, string text, int massDataNumber, int massDataType, int massDataRarely, int massDataTextureMount)
    {
        this.id = id;
        this.fieldId = fieldId;
        this.laneId = laneId;
        this.image = image;
        this.text = text;
        this.massDataNumber = massDataNumber;
        this.massDataType = massDataType;
        this.massDataRarely = massDataRarely;
        this.massDataTextureMount = massDataTextureMount;
    }
    //　ID
    int id { get; }
    //　フィールドID
    int fieldId { get; }
    //　レーンID
    int laneId { get; }
    //　マス画像
    public string  image { get; }
    //　マス表示テキスト
    public string text { get; }
    //　マス情報（マス番号）
    public int massDataNumber { get; }
    //　マス情報（マスの種類：０＝初期値、１＝バトル、２＝ボスバトル、３＝イベント、４＝宝箱、５＝特殊）
    public int massDataType { get; }
    //　マス情報（マスのレアリティ：０＝初期値、１＝ノーマル、２＝レア、３＝スーパーレア、４＝ウルトラレア）
    public int massDataRarely { get; }
    //　マス情報（マス表示画像ベース：０＝初期値、１＝草原、２＝荒野　現在は仮設定）
    public int massDataTextureMount { get; }

    
}


}
