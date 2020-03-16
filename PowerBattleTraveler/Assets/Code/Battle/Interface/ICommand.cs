using UnityEngine.EventSystems;

public interface ICommand : IEventSystemHandler
{
    void Battle();

    void Protect();

    void Escape();
}
