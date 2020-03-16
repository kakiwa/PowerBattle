using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx.Async;
using UniRx.Async.Triggers;

namespace Battle {

/// <summary>
/// 戦闘メニューのビューを管理するクラス
/// </summary>
public class BattleMenuView
    : MonoBehaviour
    , ICommand
{
    [SerializeField]
    private Transform m_CommandRoot = default;      //< 戦闘コマンドはこの下にくっつけてゆく

    [SerializeField]
    private GameObject m_CommandPrefab = default;   //< 戦闘コマンドのプレファブ

    [SerializeField]
    private Transform m_SubMenuRoot = default;      //< サブメニューのルート

    [SerializeField]
    private GameObject m_SubMenuPrefab = default;   //< サブメニューのプレファブ


    private void Awake()
    {
        // どっかからデータ持ってくる
        List<string> commandDataList = new List<string>();
        commandDataList.Add("たたかう");
        commandDataList.Add("まもる");
        commandDataList.Add("にげる");

        foreach (var it in commandDataList)
        {
            var obj = Instantiate(m_CommandPrefab, m_CommandRoot);
            obj.GetComponent<BattleCommand>().SetName(it);
        }
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 出すしまう
    /// </summary>
    /// <param name="enable"></param>
    public void SetEnable(bool enable)
    {
        this.gameObject.SetActive(enable);
    }

    /// <summary>
    /// 選択された項目を返す
    /// </summary>
    /// <returns></returns>
    public async UniTask OnBattleMenuSelected()
    {

        await UniTask.Yield();

        // メニューを開く
        SetEnable(true);

        // コマンドのリストを取得
        var comList = m_CommandRoot.GetComponentsInChildren<BattleCommand>();
        List<UniTask> list = new List<UniTask>();

        // 一つのボタンが押されたら全てのタスクを止める
        CancellationTokenSource cts = new CancellationTokenSource();

        // タスクを抽出して登録
        foreach (var it in comList)
        {
            list.Add(it.OnSelected(cts.Token, this));
        }

        // ボタンイベント待つ
        await UniTask.WhenAny(list.ToArray());

        // 押されたボタン以外のタスクをキャンセル
        cts.Cancel();

        // メニューを閉じる
        SetEnable(false);

    }

        public void Battle()
        {
            Debug.Log("Battle");
        }

        public void Protect()
        {
            Debug.Log("Protected");
        }

        public void Escape()
        {
            Debug.Log("Escape");
        }
    }

} // Battle
