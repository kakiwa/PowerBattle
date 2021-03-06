﻿using System.Collections;
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

    /// <summary>
    /// インゲームの画面に出ている奴らのビュー
    /// </summary>
    private Dictionary<uint, ActorView> m_ActorViews = new Dictionary<uint, ActorView>();

    public Dictionary<uint, ActorView> ActorViews
    {
        get
        {
            return this.m_ActorViews;
        }
        private set
        {
            this.m_ActorViews = value;
        }
    }

    /// <summary>
    /// アクターを追加
    /// </summary>
    public void AddActor(KeyValuePair<uint, ActorData> actorData) {
        // インスタンス生成
        var obj = Instantiate(
            actorPrefab,
            actorData.Value.ActorType == ActorType.PLAYER ? playerActorsRoot : enemyActorsRoot
        );

        // TODO:いい感じに座標設定したい


        // 各種設定
        var actor = obj.GetComponent<ActorView>();
        actor.setCo (
            actorData.Value.ActorType == ActorType.PLAYER ? Color.green : Color.red
        );
        // タッチ要素にID登録
        var touchable = obj.GetComponent<TouchableElement>();
        touchable.setId(actorData.Key);
        // リストに追加
        m_ActorViews.Add(actorData.Key, actor);
    }

    public void RemoveActor(uint actorId)
    {
        Destroy(m_ActorViews[actorId].gameObject);
        m_ActorViews.Remove(actorId);
    }

    /// <summary>
    /// アクタービュー取得
    /// </summary>
    public ActorView GetActorData(uint actorId)
    {
        return m_ActorViews[actorId];
    }
}

} // Battle