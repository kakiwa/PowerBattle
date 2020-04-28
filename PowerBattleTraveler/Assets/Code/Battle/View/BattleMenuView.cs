using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx.Async;
using UniRx.Async.Triggers;

using d = System.Collections.Generic.KeyValuePair<string, Battle.BattleCommandType>;

namespace Battle {

/// <summary>
/// 戦闘メニューのビューを管理するクラス
/// </summary>
public class BattleMenuView : MonoBehaviour
{
    [SerializeField] private Transform m_CommandRoot = default;      //< 戦闘コマンドはこの下にくっつけてゆく

    [SerializeField] private GameObject m_CommandPrefab = default;   //< 戦闘コマンドのプレファブ

    private void Awake()
    {
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
    /// コマンドメニュー用意
    /// </summary>
    public void SetupCommandList() {
        // TODO: どっかからデータ持ってくる
        var commandDataList = new List<d>();
        commandDataList.Add(new d("攻撃", BattleCommandType.ATTACK));
        commandDataList.Add(new d("農業", BattleCommandType.DEFENCE));
        commandDataList.Add(new d("アイテム", BattleCommandType.ITEM));
        commandDataList.Add(new d("逃げる", BattleCommandType.ESCAPE));

        foreach (var it in commandDataList)
        {
            var obj = Instantiate(m_CommandPrefab, m_CommandRoot);
            var command = obj.GetComponent<BattleCommand>();
            command.SetName(it.Key);
            command.SetCommand(it.Value);
        }

        // todo: 各項目のサブメニュー用意



    }

    /// <summary>
    /// コマンドリストのクリア
    /// </summary>
    public void ClearCommandList()
    {
        foreach (Transform t in m_CommandRoot)
        {
            Destroy(t.gameObject);
        }

        m_CommandRoot.DetachChildren();
    }

    /// <summary>
    /// 選択された項目を返す
    /// </summary>
    public async UniTask<BattleCommandType> OnBattleMenuSelected()
    {
        // 一つのボタンが押されたら全てのタスクを止めるためにトークン生成
        var cts = new CancellationTokenSource();

        // コマンドのリストからタスクを抽出して登録
        var commandList = m_CommandRoot.GetComponentsInChildren<BattleCommand>();
        var tasks = new List<UniTask<BattleCommandType>>();
        foreach (var command in commandList)
        {
            tasks.Add(command.OnSelected(cts.Token, this));
        }

        // ボタンイベント待つ
        var result = await UniTask.WhenAny(tasks.ToArray());

        // 押されたボタン以外のタスクをキャンセル
        cts.Cancel();

        return result.result;
    }
}
} // Battle
