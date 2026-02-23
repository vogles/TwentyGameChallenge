namespace TwentyGameChallenge.Core;

public interface IEvent
{
}


public class EventManager
{
    public delegate void GenericCallback<T>(T obj) where T : IEvent;

    public void RegisterListener<T>(object callback)
    {
        if (callback != null)
        {
        }
    }

    public void UnregisterListener<T>(object callback)
    {
            
    }

    public void Fire(IEvent evt)
    {
    }
}