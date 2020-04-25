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
    public int MaxHp{get;set;}
    public int Attack {get;set;}
    public int Speed {get;set;}
    public int Mp{get;set;}
    public int MaxMp{get;set;}

    // TODO: リソースデータもここにおくa？
}

/// <summary>
/// 戦闘時に使うデータ類をまとめたクラス
/// </summary>
public class BattleDataManager
{
    public TurnData TurnData = new TurnData();

    /// <summary>
    /// インゲームの画面に出ている奴らのデータ
    /// </summary>
    /// <typeparam name="uint">アクターID</typeparam>
    /// <typeparam name="ActorData">データ</typeparam>
    /// <returns></returns>
    public Dictionary<uint, ActorData> Actors = new Dictionary<uint, ActorData>();

    /// <summary>
    /// バトルをするのに必要なデータをキャッシュ
    /// </summary>
    public void SetupBattleData(
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
            data.MaxHp = 456;
            data.Hp = data.MaxHp;
            data.Speed = 2;
            data.Attack = 3;
            data.MaxMp = 123456;
            data.Mp = data.MaxHp;
            Actors.Add(i, data);
        }
        const uint enStartNum = plNum + 1;
        const uint enNum = enStartNum + 3;
        for (uint i = enStartNum; i < enNum ; ++i)
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
