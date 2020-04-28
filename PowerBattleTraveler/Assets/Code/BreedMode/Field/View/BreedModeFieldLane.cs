using UnityEngine;
using UnityEngine.UI;
using FancyScrollView;
using System;
using System.Linq;

namespace BreedMode
{
/// <summary>
/// 育成モードマス情報
/// </summary>
public class BreedModeFieldLane : FancyCell<BreedModeFieldDataList,BreedModeFieldContext>
{
    //　レーン番号
    [SerializeField]
    private int m_num = default;

    // マス数
    [SerializeField]
    private int m_massSize = default;

    //　マスアニメーション
    [SerializeField] Animator m_animator = default;

    //　ボタン情報
    [SerializeField] Button m_button = default;

    // マス情報
    [SerializeField] BreedModeFieldMass[] m_mass = default;
    
    /// <summary>
    ///　アニメーションの指定
    /// </summary>
    static class AnimatorHash
    {
        public static readonly int Scroll = Animator.StringToHash("scroll");
    }

    void Start()
    {
        foreach (var mass in m_mass)
        {
            mass.m_button.onClick.AddListener(() => Context.OnCellClicked?.Invoke(Index));
        }
    }

    /// <summary>
    ///　コンテンツの更新
    /// </summary>
    public override void UpdateContent(BreedModeFieldDataList massdata)
    {
        m_massSize = massdata.m_massSize;

        foreach (var mass in m_mass.Select((val, index) => new { val, index }))
        {
            if (IsOutOfRange(m_massSize,mass.index))
            {
                mass.val.m_obj.SetActive(true);
            }
            else
            {
                mass.val.m_obj.SetActive(false);
            }
        }
    }

    /// <summary>
    ///　セルの位置更新
    /// </summary>
    public override void UpdatePosition(float position)
    {
        m_currentPosition = position;
        m_animator.Play(AnimatorHash.Scroll, -1, position);
        m_animator.speed = 0;

        foreach (var mass in m_mass.Select((val, index) => new { val, index }))
        {
            if (IsOutOfRange(m_massSize,mass.index))
            {
                mass.val.m_animation.Play(AnimatorHash.Scroll, -1,position);
                mass.val.m_animation.speed = 0;
            }
        }
    }

    // GameObject が非アクティブになると Animator がリセットされてしまうため
    // 現在位置を保持しておいて OnEnable のタイミングで現在位置を再設定する
    float m_currentPosition = 0;
    void OnEnable() => UpdatePosition(m_currentPosition);

    bool IsOutOfRange(int size,int num)
    {
        if (num < size)
        {
            return true;
        }
        return false;
    }


}
}
