using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace Common.Master
{
public class BreedFieldMassMaster
{
    ///<summary>
    ///　マス情報（マス内部）マスタ取得
    ///</summary>
    public BreedFieldMassMaster()
    {
        TextAsset csv = Resources.Load("Master/BreedMode/breed_field_mass_mast") as TextAsset;
        StringReader reader = new StringReader(csv.text);
        Data = new List<BreedFieldMassMasterData>();
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');
            Data.Add(new BreedFieldMassMasterData(int.Parse(values[0]),int.Parse(values[1]),int.Parse(values[2]),values[3],values[4],int.Parse(values[5]),int.Parse(values[6]),int.Parse(values[7]),int.Parse(values[8])));
        }

    }


    /// <summary>
    ///　マス情報（マス内部）
    ///</summary>
    [System.Serializable]
    public struct BreedFieldMassMasterData
    {
        public BreedFieldMassMasterData(int id, int fieldId, int laneId, string image, string text, int massDataNumber, int massDataType, int massDataRarely, int massDataTextureMount)
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

    //　格納用変数
    public List<BreedFieldMassMasterData> Data { get; }

}
}
