using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Async;
using UniRx.Async.Triggers;

using TargetResult = System.Collections.Generic.KeyValuePair<bool, uint>;

namespace Battle {

[RequireComponent(typeof(ActorView))]
public class TouchableElement
    : MonoBehaviour
{
    private AsyncPointerClickTrigger asyncPointerClickTrigger;
    private uint m_ActorId;

    void Start()
    {
        asyncPointerClickTrigger = this.GetAsyncPointerClickTrigger();
    }

    public void setId(uint id) {
        m_ActorId = id;
    }

    /// <summary>
    /// 選択待ち
    /// </summary>
    public async UniTask<TargetResult> OnSelectAsync(CancellationToken token)
    {
        await asyncPointerClickTrigger.OnPointerClickAsync(token);
        return new TargetResult(true, m_ActorId);
    }
}
} // Battle
