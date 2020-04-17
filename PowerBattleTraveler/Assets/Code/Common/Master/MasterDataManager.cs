using UnityEngine;

namespace Common.Master {

public class MasterDataManager : MonoBehaviour
{
    //------------------------------------------------------------------------------------//
    //　マスタ格納変数定義　：　新規追加のマスタがある場合ここに追加（定義はMasterDataDefine.csで行う）
    //------------------------------------------------------------------------------------//

    //　マス情報（レーン）
    BreedFieldLaneMaster breedFieldLaneMaster = default;

    //　マス情報（マス内部）
    BreedFieldMassMaster breedFieldMassMaster = default;

    //-----------------------------------------------------------------------//
    //　データ管理処理
    //-----------------------------------------------------------------------//

    // GameControllerインスタンスの実体
    private static MasterDataManager instance;
    
    // GameControllerインスタンスのプロパティーは、実体が存在しないとき（＝初回参照時）実体を探して登録する
    public static MasterDataManager Instance
    {
        get
        {
            //　実体があるかどうか
            if (!instance)
            {
                //　実体がないので検索して登録
                instance = GameObject.FindWithTag("MasterDataManager").GetComponent<MasterDataManager>();
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
        if (this == instance)
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
        breedFieldLaneMaster = new BreedFieldLaneMaster();
        foreach(var data in breedFieldLaneMaster.Data)
        {
            Debug.Log(data.id.ToString());
        }
    }

    ///<summary>
    ///　マスタデータ全取得処理
    ///</summary>
    public void LoadMasterAll()
    {
        //　マスタデータの初期化
        MasterCacheReset();

        //　各種マスタの取得処理
        breedFieldLaneMaster = new BreedFieldLaneMaster();
        breedFieldMassMaster = new BreedFieldMassMaster();
    }

    ///<summary>
    ///　マスタデータキャッシュ初期化処理
    ///</summary>
    public void MasterCacheReset()
    {
        breedFieldLaneMaster = default;
        breedFieldMassMaster = default;
    }
}
}
