using Microsoft.AspNetCore.SignalR;

namespace TopoLocoApi.Services
{
    public class TopoLocoHub : Hub
    {
        public async void NuevaPuntuacion()
        {
            await Clients.All.SendAsync("NuevaPuntuacion");
        }
    }
}
