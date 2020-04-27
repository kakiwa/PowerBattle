using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasingCore;
using FancyScrollView;

namespace BreedMode
{
public class BreedModeFieldScrollView : FancyScrollView<BreedModeFieldDataList,BreedModeFieldContext>
{

    [SerializeField] Scroller m_scroller = default;
    [SerializeField] GameObject m_cellPrefab = default;

    protected override GameObject CellPrefab => m_cellPrefab;

    void Awake()
    {
        //　選択しているセルを取得および設定
        Context.OnCellClicked = SelectCell;
    }

    void Start()
    {
        
        m_scroller.OnValueChanged(UpdatePosition);
        m_scroller.OnSelectionChanged(UpdateSelection);
    }

    void UpdateSelection(int index)
    {
        if (Context.SelectedIndex == index)
        {
            return;
        }

        Context.SelectedIndex = index;
        Refresh();
    }

    public void UpdateData(IList<BreedModeFieldDataList> items)
    {
        UpdateContents(items);
        m_scroller.SetTotalCount(items.Count);
    }

    public void SelectCell(int index)
    {
        if (index < 0 || index >= ItemsSource.Count || index == Context.SelectedIndex)
        {
            return;
        }

        UpdateSelection(index);
        m_scroller.ScrollTo(index, 0.35f, Ease.OutCubic);
    }

    public int NowSelectCell()
    {
        return Context.SelectedIndex;

    }
    
}
}
