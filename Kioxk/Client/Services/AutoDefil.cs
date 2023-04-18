namespace Kioxk.Client.Services;

public class AutoDefil : IDisposable
{
    private readonly Timer myTimer;
    static public Action? MyAutoDefilEvent;
    private bool isDisposed;
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
        if (!isDisposed)
        {
            isDisposed = true;
            myTimer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
