using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreedMode
{
/// <summary>
/// 育成モードフィールドベースデータ
/// </summary>
[System.Serializable]
public struct MassData
{
        public MassData(int number, MassType massType, Rarely rarely, MassTextureMount massTextureMount)
        {
            this.number = number;
            this.massType = massType;
            this.rarely = rarely;
            this.massTextureMount = massTextureMount;
        }
    [SerializeField]
    public int number { get; set; }    //　マス番号
    public MassType massType{ get; set; }     //　マス種別 
    public Rarely rarely{ get; set; }    //　レアリティ
    public MassTextureMount massTextureMount{ get; set; }    //　マス表示画像ベース
}

}
