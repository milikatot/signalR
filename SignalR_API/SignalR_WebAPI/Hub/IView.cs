namespace SignalR_WebAPI.Hub;

public interface IView
{
    public Task BroadcastWatching(int viewCount);
}
