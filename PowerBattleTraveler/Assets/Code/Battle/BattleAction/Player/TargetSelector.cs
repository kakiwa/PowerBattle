using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx.Async;

using TargetResult = System.Collections.Generic.KeyValuePair<bool, uint>;

namespace Battle {

/// <summary>
/// ターゲット選択するクラス
/// </summary>
public class TargetSelector
{
    // 選択できる要素のリスト
    List<TouchableElement> m_TargetList = new List<TouchableElement>();

    /// <summary>
    /// 選択できる要素を用意
    /// </summary>
    public void SetupTarget(BattleViewManager viewManager)
    {
        var actors = viewManager.GetActorsView().ActorViews;
        foreach (var actor in actors) {
            var touchableElement = actor.Value.GetComponent<TouchableElement>();
            m_TargetList.Add(touchableElement);
        }
    }
    public async UniTask<TargetResult> Select(BattleViewManager viewManager)
    {
        // 一つのボタンが押されたら全てのタスクを止めるためにトークン生成
        var cts = new CancellationTokenSource();

        // ターゲットの数だけタスク生成
        var tasks = new List<UniTask<TargetResult>>();

        foreach (var target in m_TargetList)
        {
            tasks.Add(target.OnSelectAsync(cts.Token));
        }

        // 選択イベントを待つ
        var result = await UniTask.WhenAny(tasks.ToArray());

        // 選択されたアクター以外のタスクをキャンセル
        cts.Cancel();

        // 要素クリア
        m_TargetList.Clear();

        return result.result;
    }

}
} // Battle
