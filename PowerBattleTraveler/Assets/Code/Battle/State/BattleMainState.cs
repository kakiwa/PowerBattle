using System.Threading;
using System.Collections;
using System.Collections.Generic;
using IceMilkTea.Core;
using UnityEngine;
using UniRx.Async;

namespace Battle {
public partial class BattleStateManager
{

    public interface IAction
    {
        UniTask SelectAsync(BattleDataManager dataManager, BattleViewManager viewManager);

        UniTask ActionAsync(BattleDataManager dataManager, BattleViewManager viewManager);

        void Calc(BattleDataManager dataManager);

    }

    public class PlayerDiside : IAction
    {
        uint ownId;

        public PlayerDiside(uint ownId)
        {
            this.ownId = ownId;
        }

        public async UniTask SelectAsync(BattleDataManager dataManager, BattleViewManager viewManager)
        {
            Debug.Log(dataManager.Actors[ownId].Name + "の行動");
            
            await viewManager.CommandAsync();

        }

        public async UniTask ActionAsync(BattleDataManager dataManager, BattleViewManager viewManager)
        {
            await viewManager.AnimationAsync();
        }

        public void Calc(BattleDataManager dataManager)
        {

        }
    }

    public class EnemyDiside : IAction
    {
        ActorData ownData;

        uint ownId;

        uint targetId;

        public EnemyDiside(uint ownId)
        {
            this.ownId = ownId;
        }

        public async UniTask SelectAsync(BattleDataManager dataManager, BattleViewManager viewManager)
        {
            Debug.Log(ownData.Name + "の行動");
            await UniTask.Delay(System.TimeSpan.FromSeconds(1));

            targetId = 1;
        }



        public async UniTask ActionAsync(BattleDataManager dataManager, BattleViewManager viewManager)
        {


            var acView = viewManager.GetActorsView().GetActorData(targetId);
            acView.setHp(dataManager.Actors[targetId].Hp);

            if (dataManager.Actors[targetId].Hp <= 0) {
                acView.gameObject.SetActive(false);
            }

            await viewManager.AnimationAsync();

        }

        public void Calc( BattleDataManager dataManager)
        {
            var targetData = dataManager.Actors[targetId];
            targetData.Hp -= dataManager.Actors[ownId].Attack;

            // リストに戻す
            dataManager.Actors[targetId] = targetData;
        }
    }

    private class BattleState : ImtStateMachine<BattleStateManager>.State
    {
        CancellationTokenSource cancellationTokenSource;
        protected override void Enter()
        {
            Debug.Log("戦闘開始");

            cancellationTokenSource = new CancellationTokenSource();

            TurnStart();
        }

        protected override void Exit()
        {

        }

        /// <summary>
        /// 一ターンの流れをつらつらする
        /// </summary>
        private async void TurnStart()
        {
            Debug.Log("ターン開始");

            // 開始ターンセット
            ++Context.m_BattleDataManager.TurnData.turnCount;
            Context.m_ViewManager.setTurn(Context.m_BattleDataManager.TurnData.turnCount);

            SortActors();

            await Turn();

        }

        private async UniTask Turn()
        {
            await ActionAsync();

            Debug.Log("ターン終了");

            bool check = EndCheck();
            if (check)
            {
                cancellationTokenSource.Cancel();
                StateMachine.SendEvent((int)StateType.RESULT);
            }
            else
            {
                TurnStart();
            }
        }

        private async UniTask ActionAsync()
        {
            Debug.Log("アクションリスト:"+m_ActionList.Count.ToString());
            // 行動
            foreach (var it in m_ActionList)
            {
                await it.SelectAsync(Context.m_BattleDataManager, Context.m_ViewManager);

                it.Calc(Context.m_BattleDataManager);

                await it.ActionAsync(Context.m_BattleDataManager, Context.m_ViewManager);

            }

            m_ActionList.Clear();


        }

        List<IAction> m_ActionList = new List<IAction>();

        private async UniTask WaitCommand()
        {

            await Context.m_ViewManager.CommandAsync();
        }

        private async UniTask WaitAnimation()
        {
            await Context.m_ViewManager.AnimationAsync();
        }

        // 行動順番を決定
        private void SortActors()
        {
            var actors = Context.m_BattleDataManager.Actors;
            foreach (var it in actors)
            {
                IAction actionCommand = it.Value.ActorType == ActorType.PLAYER ?
                    (IAction)new PlayerDiside(it.Key) :
                    (IAction)new EnemyDiside(it.Key) ;
                m_ActionList.Add(actionCommand);
            }

            // TODO: ソート
        }

    }

    private class EndCheckState : ImtStateMachine<BattleStateManager>.State
    {
        protected override void Enter()
        {
            int playerCount = 0;
            int enemyCount  = 0;

            foreach (var actor in Context.m_BattleDataManager.Actors) {
                if (actor.Value.Hp <= 0)
                {
                    continue;
                }
                var _ = actor.Value.ActorType == ActorType.PLAYER ? ++playerCount : ++enemyCount;
            }

            var nextState = (playerCount == 0) || (enemyCount == 0) ? StateType.RESULT : StateType.SELECTION;

            StateMachine.SendEvent((int)nextState);
        }
    }

    private class ThinkState : ImtStateMachine<BattleStateManager>.State
    {
        protected override void Enter()
        {

        }
    }

    private class ActState : ImtStateMachine<BattleStateManager>.State
    {
        protected override void Enter()
        {

        }
    }
}

} // Battle
