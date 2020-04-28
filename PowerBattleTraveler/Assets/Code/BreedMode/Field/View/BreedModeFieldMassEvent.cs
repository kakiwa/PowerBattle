using UnityEngine.EventSystems;
/// <summary>
/// 育成モードフィールドマスイベント
/// </summary>
public interface IFieldMassEvents : IEventSystemHandler
{
    void OpenPopUp(string sceneName);
}
