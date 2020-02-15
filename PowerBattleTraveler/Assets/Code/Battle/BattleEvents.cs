using UnityEngine.EventSystems;
/// <summary>
/// バトルで起こりうるイベント
/// </summary>
public interface IBattleEvents : IEventSystemHandler
{
    void OnMenu();
    void OnPause();
}
