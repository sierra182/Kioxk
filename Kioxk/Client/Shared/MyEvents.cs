using Microsoft.AspNetCore.Components;

namespace Kioxk.Client.Shared;
    public class MyEvents
    {
    public event Action? MySync;
    public Action? MyPointer;

    public void RiseSyncEvent()=> MySync?.Invoke();
    public void RisePointerEvent()=> MyPointer?.Invoke();
}

