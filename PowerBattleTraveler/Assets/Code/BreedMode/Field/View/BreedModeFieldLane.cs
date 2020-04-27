using UnityEngine;
using UnityEngine.UI;
using FancyScrollView;

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
        for　(int i = 0;i< m_massSize; ++i)
        {
            m_mass[i].m_button.onClick.AddListener(() => Context.OnCellClicked?.Invoke(Index));
        }
    }

    /// <summary>
    ///　コンテンツの更新
    /// </summary>
    public override void UpdateContent(BreedModeFieldDataList massdata)
    {
        m_massSize = massdata.m_massSize;

        for　(int i = 0;i< m_massSize; ++i)
        {
            m_mass[i].m_obj.SetActive(true);
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

        for　(int i = 0; i<m_massSize;++i)
        {
            m_mass[i].m_animation.Play(AnimatorHash.Scroll, -1,position);
            m_mass[i].m_animation.speed = 0;
        }
    }

    // GameObject が非アクティブになると Animator がリセットされてしまうため
    // 現在位置を保持しておいて OnEnable のタイミングで現在位置を再設定する
    float m_currentPosition = 0;
    void OnEnable() => UpdatePosition(m_currentPosition);


}
}
