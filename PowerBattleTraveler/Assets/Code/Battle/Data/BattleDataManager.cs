using System.Collections;
using System.Collections.Generic;
using UniRx.Async;

namespace Battle {
public enum ActorType
{
    DEFAULT,
    PLAYER,
    ENEMY,
}

public struct ActorData{
    public ActorType ActorType {get;set;}
    public string Name {get;set;}
    public int Hp {get; set;}
    public int Attack {get;set;}
    public int Speed {get;set;}


    // TODO: リソースデータもここにおくa？


}

/// <summary>
/// 戦闘時に使うデータ類をまとめたクラス
/// </summary>
public class BattleDataManager 
{
    public TurnData TurnData = new TurnData();

    public Dictionary<uint, ActorData> Actors = new Dictionary<uint, ActorData>();

    /// <summary>
    /// バトルをするのに必要なデータをキャッシュ
    /// </summary>
    public async UniTask SetupBattleData(
        int someData
    )
    {
        // ダミーデータ生成
        const uint plNum = 1;
        for (uint i = 1; i <= plNum ; ++i)
        {
            var data = new ActorData();
            data.ActorType = ActorType.PLAYER;
            data.Name = "味方" + i.ToString();
            data.Hp = 5;
            data.Speed = 2;
            data.Attack = 1;
            Actors.Add(i, data);
        }
        const uint enStartNum = plNum + 1;
        for (uint i = enStartNum; i <= plNum + 3 ; ++i)
        {
            var data = new ActorData();
            data.ActorType = ActorType.ENEMY;
            data.Name = "敵" + i.ToString();
            data.Hp = 5;
            data.Speed = (int)i;
            data.Attack = 1;
            Actors.Add(i, data);
        }

        
    }

}

} // Battle
