using Microsoft.AspNetCore.SignalR;

namespace SignalR_WebAPI.Hub;

public class ViewHub : Hub<IView>
{
    public static int ViewCount { get; set; } = 0;


    public async Task NotifyWatching()
    {
        ViewCount++;

        // Notify EVERYON about new view count
        await Clients.All.BroadcastWatching(ViewCount);
    }

}
