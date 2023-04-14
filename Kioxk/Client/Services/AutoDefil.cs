namespace Kioxk.Client.Services;

public class AutoDefil : IDisposable
{
    private readonly Timer myTimer;
    static public Action? MyAutoDefilEvent;
    public AutoDefil()
    {
        myTimer = new(_ => OnTimerElapsed(), null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
    }

    static private void OnTimerElapsed()
    {
        MyAutoDefilEvent?.Invoke();
    }
    public void Dispose()
    {
        myTimer.Dispose();
        GC.SuppressFinalize(this);
    }
}
