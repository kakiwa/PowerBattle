using UnityEngine;
using UniRx.Async;
using DG.Tweening;
using Common;

namespace Battle {

public class PlayerAction : IAction
{
    uint ownId;

    uint targetId;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="ownId">アクターID</param>
    public PlayerAction(uint ownId)
    {
        this.ownId = ownId;
    }

    /// <summary>
    /// 行動選択
    /// </summary>
    public async UniTask SelectAsync(BattleDataManager dataManager, BattleViewManager viewManager)
    {
        Debug.Log(dataManager.Actors[ownId].Name + "の行動");

        var battleMenu = viewManager.BattleMenu;

        // バトルメニューを有効化
        battleMenu.SetEnable(true);

        // コマンドリスト用意
        // todo: キャラクターの行動できる一覧データを渡す
        battleMenu.SetupCommandList();

        // 行動決定タスク開始
        await MainCommandMenu(viewManager);

        // 行動が決定されたら選択タスク終了
        // コマンドリストリセット
        battleMenu.ClearCommandList();
        // バトルメニューを無効化
        battleMenu.SetEnable(false);
    }


    public void Calc(BattleDataManager dataManager)
    {
        var targetData = dataManager.Actors[targetId];
        targetData.Hp -= dataManager.Actors[ownId].Attack;

        // リストに戻す
        dataManager.Actors[targetId] = targetData;
    }

    /// <summary>
    /// コマンド選択タスク
    /// </summary>
    private async UniTask MainCommandMenu(BattleViewManager viewManager)
    {
        var battleMenu = viewManager.BattleMenu;
        var result = await battleMenu.OnBattleMenuSelected();

        switch (result)
        {
            case BattleCommandType.ATTACK:
                Debug.Log("攻撃");
                await TargetSelect(viewManager);
                break;
            case BattleCommandType.DEFENCE:
                Debug.Log("防護油");
                await MainCommandMenu(viewManager);
                break;
            case BattleCommandType.ITEM:
                Debug.Log("アイテム使用");
                await MainCommandMenu(viewManager);
                break;
            case BattleCommandType.ESCAPE:
                Debug.Log("逃げる");
                break;
        }
    }

    /// <summary>
    /// 対象選択タスク
    /// </summary>
    private async UniTask TargetSelect(BattleViewManager viewManager)
    {
        var targetSelector = new TargetSelector();
        // 選択対象になりうるキャラクターのデータセット
        targetSelector.SetupTarget(viewManager);

        // 選択待ち（キャンセルを含む
        var result = await targetSelector.Select(viewManager);

        if (!result.Key)
        {
            await MainCommandMenu(viewManager);
        }

        targetId = result.Value;
    }



#region Animations

    public async UniTask ActionAsync(BattleDataManager dataManager, BattleViewManager viewManager)
    {
        var ownTransform = viewManager.ActorsRootView.ActorViews[ownId].transform;
        var pos = ownTransform.position.x;
        // 前に
        await ownTransform.DOMoveX(pos + 1.0f, 0.5f).SetEase(Ease.InQuart);

        // ぐわぁ
        var targetTransform = viewManager.ActorsRootView.ActorViews[targetId].transform;
        await targetTransform.DOShakePosition(0.5f).SetEase(Ease.InQuart);

        // もどる
        await ownTransform.DOMoveX(pos, 0.5f).SetEase(Ease.InQuart);
    }

#endregion Animations
}
} // Battle