namespace Kioxk.Client.Shared;
    public class MySync
    {
    public event Action myEvent;

    public void RiseEvent()
    {
        myEvent.Invoke();
    }
    }

