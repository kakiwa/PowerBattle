using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx.Async;
using UniRx.Triggers;

namespace Battle {

/// <summary>
/// バトルメニューの項目のベース
/// </summary>
public class BattleCommand : MonoBehaviour
{
    [SerializeField]
    private Button m_Button = default;

    [SerializeField]
    private Text m_Text = default;

    public void SetName(string name)
    {
        m_Text.text = name;
    }
    
    public async UniTask OnSelected(CancellationToken token, BattleMenuView parent)
    {
        // クリックされるまでまつ
        await m_Button.OnClickAsync(token);

        ExecuteEvents.Execute<ICommand>(
            parent.gameObject,
            null,
            (x,data)=>x.Battle()
        );
    }
}
} // Battle
