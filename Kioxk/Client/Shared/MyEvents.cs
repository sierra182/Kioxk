using Microsoft.AspNetCore.Components;

namespace Kioxk.Client.Shared;
    public class MyEvents
    {
    public delegate void SubscriptionHandler();
    public event SubscriptionHandler ?AddSubscriptionSync;
    public event Action? RemoveSubscriptionSync;
    public Action? MyPointer;

    public void RiseAddSubscriptionSyncEvent()=> AddSubscriptionSync?.Invoke();
    public void RiseRemoveSubscriptionSyncEvent() => RemoveSubscriptionSync?.Invoke();
    public void RisePointerEvent()=> MyPointer?.Invoke();
}

