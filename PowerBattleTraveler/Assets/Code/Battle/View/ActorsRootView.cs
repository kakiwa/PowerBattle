using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle {



/// <summary>
/// 戦闘するキャラクターをまとめるビュー
/// </summary>
public class ActorsRootView : MonoBehaviour
{

    [SerializeField]
    private  GameObject actorPrefab;

    [SerializeField]
    private Transform playerActorsRoot;

    [SerializeField]
    private Transform enemyActorsRoot;


    private Dictionary<uint, ActorView> actorViews = new Dictionary<uint, ActorView>();

    /// <summary>
    /// アクターを追加
    /// </summary>
    /// <param name="actorData"></param>
    public void AddActor(KeyValuePair<uint, ActorData> actorData) {
        var obj = Instantiate(
            actorPrefab,
            actorData.Value.ActorType == ActorType.PLAYER ? playerActorsRoot: enemyActorsRoot
        );
        var actor = obj.GetComponent<ActorView>();
        actor.setCo (
        actorData.Value.ActorType == ActorType.PLAYER ? Color.green : Color.red ); 
        actor.setName(actorData.Value.Name);
        actor.setHp(actorData.Value.Hp);
        actor.setAttack(actorData.Value.Attack);
        actorViews.Add(actorData.Key, actor);
    }

    public ActorView GetActorData(uint actorId)
    {
        return actorViews[actorId];
    }
}

} // Battle