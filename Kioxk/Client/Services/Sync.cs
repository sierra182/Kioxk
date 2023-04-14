namespace Kioxk.Client.Services;

using System.Threading;
public class Sync : IDisposable
{
    private readonly Timer myTimer; 
    public delegate void MyEventHandler();
    static public event MyEventHandler ?MyEvent;  
    public Sync()
    {
        myTimer = new(_ => OnTimerElapsed(), null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
    }

    static private void OnTimerElapsed()
    {
        SyncDataWithServer();
    }

    static private void SyncDataWithServer()=> 
        MyEvent?.Invoke();  
    
        
    public void Dispose()
    {
        myTimer.Dispose();
        GC.SuppressFinalize(this);
    }
}

