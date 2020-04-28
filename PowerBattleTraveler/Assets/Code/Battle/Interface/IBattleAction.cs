using UniRx.Async;

namespace Battle {

public interface IAction
{
    UniTask SelectAsync(BattleDataManager dataManager, BattleViewManager viewManager);

    UniTask ActionAsync(BattleDataManager dataManager, BattleViewManager viewManager);

    void Calc(BattleDataManager dataManager);
}
}
