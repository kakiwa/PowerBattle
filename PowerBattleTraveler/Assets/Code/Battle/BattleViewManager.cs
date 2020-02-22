using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx.Async;

namespace Battle {
/// <summary>
/// バトルの表示を各ビューに伝える
/// </summary>
public class BattleViewManager : MonoBehaviour
{

    #region 変数

    [SerializeField]
    private Text m_Text;    //< アニメーション代用表示テキスト
    #endregion 変数


    #region 公開関数
    public async UniTask<bool> AnimationAsync()
    {
        Debug.Log("演出開始");

        await SomeAnim();

        Debug.Log("演出終了");
        return true;
    }

    #endregion 公開関数

    #region 非公開関数
    public async UniTask SomeAnim()
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(1));
        Debug.Log("1");
        await UniTask.Delay(System.TimeSpan.FromSeconds(1));
        Debug.Log("2");
        await UniTask.Delay(System.TimeSpan.FromSeconds(1));
        Debug.Log("3");
        await UniTask.Delay(System.TimeSpan.FromSeconds(1));
    }
    #endregion 非公開関数
}

} // Battle
