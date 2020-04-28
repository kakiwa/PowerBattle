using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle {

/// <summary>
/// 味方の情報UIのルート
/// </summary>
public class BattleAllyInfoRootView : MonoBehaviour
{

    [SerializeField]
    private GameObject m_BattleAllyInfoPrefab = default;

    private Dictionary<uint, BattleAllyInfoView> m_BattleAllyInfos = new Dictionary<uint, BattleAllyInfoView>();
    public void AddAlly(KeyValuePair<uint, ActorData> actorData) {
        var obj = Instantiate(
            m_BattleAllyInfoPrefab,
            this.transform
        );

        var allyInfo = obj.GetComponent<BattleAllyInfoView>();
        allyInfo.setHp(actorData.Value.Hp);
    }
}
} //Battle
