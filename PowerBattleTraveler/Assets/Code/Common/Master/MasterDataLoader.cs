using UnityEngine;
using System.IO;

namespace Common.Master
{
public class MasterDataLoader : MonoBehaviour
{
    //------------------------------------------------------------------------------------//
    //　マスタ格納変数定義　：　新規追加のマスタがある場合ここに追加（定義はMasterDataDefine.csで行う）
    //------------------------------------------------------------------------------------//

    //　マス情報（レーン）
    BreedFieldLaneMaster[] breedFieldLaneMaster = default;

    //　マス情報（マス内部）
    BreedFieldMassMaster[] breedFieldMassMaster = default;

    //-----------------------------------------------------------------------//
    //　データ管理処理
    //-----------------------------------------------------------------------//

    // GameControllerインスタンスの実体
    private static MasterDataLoader instance;
    // GameControllerインスタンスのプロパティーは、実体が存在しないとき（＝初回参照時）実体を探して登録する
    public static MasterDataLoader Instance
    {
        get
        {
            //　実体があるかどうか
            if (instance == null)
            {
                //　実体がないので検索して登録
                instance = GameObject.FindWithTag("MasterDataLoader").GetComponent<MasterDataLoader>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    //初期化
    private void Awake()
    {
        // もしインスタンスが複数存在するなら、自らを破棄する
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        // 唯一のインスタンスなら、シーン遷移しても残す
        DontDestroyOnLoad(this.gameObject);

        LoadMaster();

    }
    //消える時の処理
    private void OnDestroy()
    {
        // 破棄時に、登録した実体の解除を行う
        if (this == Instance)
        {
            instance = null;
        }
    }


    //-----------------------------------------------------------------------//
    //　マスタデータ取得処理　：　新規追加のマスタがある場合ここに追加
    //-----------------------------------------------------------------------//

    ///<summary>
    ///　マスタデータ単体取得処理
    ///</summary>
    public void LoadMaster()
    {
        GetBreedFieldLaneMaster();

    }

    ///<summary>
    ///　マスタデータ全取得処理
    ///</summary>
    public void LoadMasterAll()
    {
        //　マスタデータの初期化
        MasterCacheReset();

        //　各種マスタの取得処理
        GetBreedFieldLaneMaster();
        GetBreedFieldMassMaster();
    }

    ///<summary>
    ///　マスタデータキャッシュ初期化処理
    ///</summary>
    public void MasterCacheReset()
    {
        breedFieldLaneMaster = default;
        breedFieldMassMaster = default;
    }

    ///<summary>
    ///　マス情報（レーン）マスタ取得
    ///</summary>
    void GetBreedFieldLaneMaster()
    {
        int i = 0, j;
        TextAsset csv = Resources.Load("Master/BreedMode/breed_field_lane_mast") as TextAsset;
        StringReader reader = new StringReader(csv.text);
        breedFieldLaneMaster = new BreedFieldLaneMaster[reader.Peek()];
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');
            for (j = 0; j < values.Length; ++j)
            {
                breedFieldLaneMaster[i] = new BreedFieldLaneMaster(int.Parse(values[0]),int.Parse(values[1]),int.Parse(values[2]),int.Parse(values[3]));
            }
            ++i;
        }
    }

    ///<summary>
    ///　マス情報（マス内部）マスタ取得
    ///</summary>
    void GetBreedFieldMassMaster()
    {
        int i = 0, j;
        TextAsset csv = Resources.Load("Master/BreedMode/breed_field_mass_mast") as TextAsset;
        StringReader reader = new StringReader(csv.text);
        breedFieldMassMaster = new BreedFieldMassMaster[reader.Peek()];
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');
            for (j = 0; j < values.Length; ++j)
            {
                breedFieldMassMaster[i] = new BreedFieldMassMaster(int.Parse(values[0]),int.Parse(values[1]),int.Parse(values[2]),values[3],values[4],int.Parse(values[5]),int.Parse(values[6]),int.Parse(values[7]),int.Parse(values[8]));              
            }
            ++i;
        }
    }



}
}
