using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace Common.Master {

/// <summary>
///　マス情報（レーン）
///</summary>
public class BreedFieldLaneMaster
{
    ///<summary>
    ///　マス情報（レーン）マスタ取得
    ///</summary>
    public BreedFieldLaneMaster()
    {
        TextAsset csv = Resources.Load("Master/BreedMode/breed_field_lane_mast") as TextAsset;
        StringReader reader = new StringReader(csv.text);
        Data = new List<BreedFieldLaneMasterData>();
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');
            Data.Add(new BreedFieldLaneMasterData(int.Parse(values[0]),int.Parse(values[1]),int.Parse(values[2]),int.Parse(values[3])));
        } 
    }

    [System.Serializable]
    public struct BreedFieldLaneMasterData
    {
        public BreedFieldLaneMasterData(int id, int fieldId, int lane, int massSize)
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

    //　格納用変数
    public List<BreedFieldLaneMasterData> Data { get; }
}
}
