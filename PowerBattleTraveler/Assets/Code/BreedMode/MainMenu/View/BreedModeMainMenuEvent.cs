using UnityEngine.EventSystems;
/// <summary>
/// 育成モードメインメニューイベント
/// </summary>
public interface IMainMenuEvents : IEventSystemHandler
{
    void OnChangeScene(string sceneName);
}
