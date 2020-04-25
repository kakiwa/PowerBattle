using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniRx.Async;
using DG.Tweening;
using System;

namespace Battle {

/// <summary>
/// 行動リスト
/// </summary>
public class ActionTimelineView : MonoBehaviour
{

    /// <summary>
    /// アクターのプレファブ
    /// </summary>
    [SerializeField] private GameObject m_ElementPrefab = default;

    /// <summary>
    /// アクター達のルート
    /// </summary>
    [SerializeField] private Transform m_ElementsRoot = default;

    /// <summary>
    /// 先頭のアクターの座標
    /// </summary>
    [SerializeField] private Transform m_TopElementPos = default;

    /// <summary>
    /// 自身のキャンバスグループ
    /// </summary>
    [SerializeField] private CanvasGroup m_CanvasGroup = default;

    /// <summary>
    /// 背景のキャンバスグループ
    /// </summary>
    [SerializeField] private CanvasGroup m_BackCanvasGroup = default;

    /// <summary>
    /// タイムラインに乗るアクターViewリスト
    /// </summary>
    private List<ActionTimelineElementView> m_ElementList = new List<ActionTimelineElementView>();

    void Awake()
    {

    }

    /// <summary>
    /// 要素を追加
    /// </summary>
    public void AddElement(ActorData actorData)
    {
        var obj = Instantiate(m_ElementPrefab, m_ElementsRoot);
        var element = obj.GetComponent<ActionTimelineElementView>();
        // 見てくれを設定
        element.SetCo(actorData.ActorType == ActorType.PLAYER ? Color.green : Color.red);

        // 前の要素の後ろにおく
        if (m_ElementList.Count != 0)
        {
            var previousElement = m_ElementList.Last().transform.localPosition;
            previousElement.x += 150.0f;
            obj.transform.localPosition = previousElement;
        }

        // リストに追加
        m_ElementList.Add(element);
    }

    public void ResetElementsAlpha()
    {
        m_CanvasGroup.alpha = 0.0f;
    }

    /// <summary>
    /// 先頭の要素を消し去る
    /// </summary>
    private void DropTopElement()
    {
        m_ElementList.RemoveAt(0);
    }



#region Animations

    /// <summary>
    /// 先頭の要素が動く演出
    /// </summary>
    public async UniTask TopElementMoveAnim()
    {
        const float MAX_TIME = 0.3f;
        var top = m_ElementList.First();
        // 演出リスト
        List<UniTask> tasks = new List<UniTask>();
        // 移動タスク
        tasks.Add(
            new Func<UniTask>(
                async () =>
                {
                    await top.transform.DOMoveX(m_TopElementPos.position.x,MAX_TIME).SetEase(Ease.OutQuint);
                }
            )()
        );

        // スケールタスク
        tasks.Add(
            new Func<UniTask>(
                async () =>
                {
                    await top.transform.DOScale(new Vector3(0.75f, 0.75f, 0),MAX_TIME).SetEase(Ease.OutQuint);
                }
            )()
        );

        // 全ての演出をまつ
        await UniTask.WhenAll(tasks);
    }

    /// <summary>
    /// 先頭の要素ドロップアウト演出
    /// </summary>
    /// <returns></returns>
    public async UniTask TopElementDropOutAnim()
    {
        const float MAX_TIME = 0.5f;
        List<UniTask> tasks = new List<UniTask>();
        var top = m_ElementList.First();

        // スケールタスク
        tasks.Add(
            new Func<UniTask>(
                async () =>
                {
                    await top.transform.DOScale(new Vector3(3.0f, 3.0f, 1.0f), MAX_TIME).SetEase(Ease.OutQuart);
                }
            )()
        );

        // アルファフェードタスク
        tasks.Add(
            new Func<UniTask>(
                async () =>
                {
                    var topCanvasGroup = top.GetComponent<CanvasGroup>();
                    await topCanvasGroup.DOFade(0.0f, MAX_TIME).SetEase(Ease.OutQuart);
                }
            )()
        );

        await UniTask.WhenAll(tasks);
        DropTopElement();
    }

    /// <summary>
    /// 前に詰める
    /// </summary>
    public async UniTask StuffOfFrontElementsAnim()
    {
        const float MAX_TIME = 0.3f;

        // 全要素移動
        // 演出リスト
        List<UniTask> tasks = new List<UniTask>();
        var targetSize = m_ElementList.Count;
        for (var index = 1 ; index < targetSize ; ++index)
        {
            var element = m_ElementList[index];
            var elementPosX = m_ElementsRoot.position.x;
            var targetPosX = elementPosX + element.Width * (index - 1);
            Debug.Log(element.Width);
            tasks.Add(
                new Func<UniTask>(
                    async () =>
                    {
                        await element.transform.DOMoveX(targetPosX, MAX_TIME).SetEase(Ease.OutQuint);
                    }
                )()
            );
        }
        await UniTask.WhenAll(tasks);
    }

    /// <summary>
    /// 戦闘開始時の演出
    /// </summary>
    public async UniTask StartAnim()
    {
        const float MAX_TIME = 0.4f;

        // 演出リスト
        List<UniTask> tasks = new List<UniTask>();

        // 移動タスク
        tasks.Add(
            new Func<UniTask>(
                async () =>
                {
                    var canvasTransform = m_BackCanvasGroup.transform;
                    float targetX = canvasTransform.position.x;
                    canvasTransform.Translate(450.0f, 0.0f, 0.0f);
                    await canvasTransform.DOMoveX(targetX, MAX_TIME).SetEase(Ease.InQuart);
                }
            )()
        );

        // アルファフェードタスク
        tasks.Add(
            new Func<UniTask>(
                async () =>
                {
                    m_BackCanvasGroup.alpha = 0.0f;
                    await m_BackCanvasGroup.DOFade(1.0f, MAX_TIME).SetEase(Ease.InQuart);
                }
            )()
        );

        // 登録した演出全ての完了をまつ
        await UniTask.WhenAll(tasks);
    }

    /// <summary>
    /// ターン開始時の演出
    /// </summary>
    public async UniTask TurnStartAnim()
    {
        const float MAX_TIME = 0.4f;

        // 演出リスト
        List<UniTask> tasks = new List<UniTask>();

        // 移動タスク
        tasks.Add(
            new Func<UniTask>(
                async () =>
                {
                    var canvasTransform = m_CanvasGroup.transform;
                    float targetX = canvasTransform.position.x;
                    canvasTransform.Translate(450.0f, 0.0f, 0.0f);
                    await canvasTransform.DOMoveX(targetX, MAX_TIME).SetEase(Ease.InQuart);
                }
            )()
        );

        // アルファフェードタスク
        tasks.Add(
            new Func<UniTask>(
                async () =>
                {
                    m_CanvasGroup.alpha = 0.0f;
                    await m_CanvasGroup.DOFade(1.0f, MAX_TIME).SetEase(Ease.InQuart);
                }
            )()
        );

        // 登録した演出全ての完了をまつ
        await UniTask.WhenAll(tasks);
    }

#endregion Animations
}
} // Battle
