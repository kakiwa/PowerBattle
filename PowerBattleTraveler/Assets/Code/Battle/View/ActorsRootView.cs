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
    private  GameObject actorPrefab = default;

    [SerializeField]
    private Transform playerActorsRoot = default;

    [SerializeField]
    private Transform enemyActorsRoot = default;


    private Dictionary<uint, ActorView> actorViews = new Dictionary<uint, ActorView>();

    /// <summary>
    /// アクターを追加
    /// </summary>
    /// <param name="actorData"></param>
    public void AddActor(KeyValuePair<uint, ActorData> actorData) {
        // インスタンス生成
        var obj = Instantiate(
            actorPrefab,
            actorData.Value.ActorType == ActorType.PLAYER ? playerActorsRoot : enemyActorsRoot
        );
        // 各種設定
        var actor = obj.GetComponent<ActorView>();
        actor.setCo (
            actorData.Value.ActorType == ActorType.PLAYER ? Color.green : Color.red
        );
        // リストに追加
        actorViews.Add(actorData.Key, actor);
    }

    public ActorView GetActorData(uint actorId)
    {
        return actorViews[actorId];
    }
}

} // Battle