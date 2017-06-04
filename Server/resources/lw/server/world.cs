using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTANetworkServer;
using GTANetworkShared;

namespace lw
{

    public class World : Script
    {
        public World()
        {
            API.onResourceStart += onStart;
            API.onPlayerConnected += onPlayerConnected;
            API.onPlayerFinishedDownload += onPlayerFinishedDownload;

            API.onClientEventTrigger += onClientEvent;
        }

        public void onStart()
        {
            var now = DateTime.Now;
            API.setTime(now.Hour, now.Minute);
        }

        public void onPlayerConnected(Client player)
        {
            API.sendChatMessageToAll(player.name + " вошел на сервер. Всего: " + API.getAllPlayers().Count);
        }

        public void onPlayerFinishedDownload(Client player)
        {
            player.freeze(true);
            player.dimension = 1;
            player.position = new Vector3(-268.9566, 6643.172, 7.5);

            API.triggerClientEvent(player, "client:presentStartWindow");
        }

        public void onClientEvent(Client player, string eventName, params object[] arguments)
        {
            switch (eventName)
            {
                case "server:readyForPlay":
                    onPlayerReadyForPlay(player);
                    break;
                default: break;
            }
        }

        public void onPlayerReadyForPlay(Client player)
        {
            player.freeze(false);
            player.dimension = 0;
            player.setSkin(API.pedNameToModel("FreeModeMale01"));

            API.triggerClientEvent(player, "client:readyForPlay");
        }
    }

}
