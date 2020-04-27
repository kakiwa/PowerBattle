using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using FancyScrollView;
using Common.Master;

namespace BreedMode
{
/// <summary>
/// 育成モードフィールド基盤
/// </summary>
public class BreedModeFieldController : MonoBehaviour
{
    //　スクロールビューデータ
    [SerializeField]
    BreedModeFieldScrollView m_scrollView = default;
    
    //　スクロールビュー インデックス番号
    [SerializeField]
    int m_indexNum = default;

    //　現在のマス番号
    public int m_nowIndex = default;

    //　マス情報
    public BreedModeFieldDataList[] m_massDatas = default;

    //
    private List<MassData> m_massList = default; 

    private int m_massSize = 20;

    private int m_fieldNum = 1;

    public void Start()
    {
        var masterData = MasterDataManager.Instance.breedFieldLaneMasterData.Data;
        m_massSize = masterData.Count;
        Debug.Log(m_massSize);

        List<BreedModeFieldDataList> data = new List<BreedModeFieldDataList>();

        foreach　(var n in MasterDataManager.Instance.breedFieldLaneMasterData.Data)
        {
            if　(n.fieldId == m_fieldNum)
            {
                var laneData =  new BreedModeFieldDataList(n.massSize);
                data.Add(laneData);
            }
        }
        
        m_scrollView.UpdateData(data);
        m_scrollView.SelectCell(m_indexNum);
        m_nowIndex = m_indexNum;

    }
    
    public void Init(List<MassData> massDatas)
    {
        foreach (var i in massDatas)
        {           
            var mass = new MassData();
            m_massList.Add(mass);
        }
    }

    /// <summary>
    /// シーン遷移関数
    /// </summary>
    public void OpenPopUp(MassData massData)
    {
        //　シーン遷移イベントを呼ぶ
        Debug.Log(massData.massType.ToString() + "にシーン遷移するよ〜");
    }
}
}
