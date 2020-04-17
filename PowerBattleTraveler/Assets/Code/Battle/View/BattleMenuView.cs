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
public class BattleMenuView : MonoBehaviour
{
    [SerializeField]
    private Transform m_CommandRoot = default;      //< 戦闘コマンドはこの下にくっつけてゆく

    [SerializeField]
    private GameObject m_CommandPrefab = default;   //< 戦闘コマンドのプレファブ

    private void Awake()
    {
        // どっかからデータ持ってくる
        List<string> commandDataList = new List<string>();
        commandDataList.Add("攻撃");
        commandDataList.Add("農業");
        commandDataList.Add("アイテム");
        commandDataList.Add("逃げる");

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
    public void SetEnable(bool enable)
    {
        this.gameObject.SetActive(enable);
    }

    /// <summary>
    /// 選択された項目を返す
    /// </summary>
    public async UniTask<int> OnBattleMenuSelected()
    {
        await UniTask.Yield();

        // メニューを開く
        SetEnable(true);

        // コマンドのリストを取得
        var comList = m_CommandRoot.GetComponentsInChildren<BattleCommand>();
        List<UniTask<string>> list = new List<UniTask<string>>();

        // 一つのボタンが押されたら全てのタスクを止める
        CancellationTokenSource cts = new CancellationTokenSource();

        // タスクを抽出して登録
        foreach (var it in comList)
        {
            list.Add(it.OnSelected(cts.Token, this));
        }

        // ボタンイベント待つ
        var ret = await UniTask.WhenAny(list.ToArray());

        // 押されたボタン以外のタスクをキャンセル
        cts.Cancel();

        Debug.Log("選択コマンド:" + ret.result);
        if (ret.winArgumentIndex == 0) {
            // なんだここ
        }

        // メニューを閉じる
        SetEnable(false);

        return 0;
    }
}

} // Battle
