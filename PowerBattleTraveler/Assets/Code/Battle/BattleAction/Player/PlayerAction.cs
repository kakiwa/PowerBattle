using UnityEngine;
using UniRx.Async;

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

        var battleMenu = viewManager.GetBattleMenu();

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

    public async UniTask ActionAsync(BattleDataManager dataManager, BattleViewManager viewManager)
    {
        await viewManager.AnimationAsync();
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
        var battleMenu = viewManager.GetBattleMenu();
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
}
} // Battle