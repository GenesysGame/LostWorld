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
            API.onPlayerFinishedDownload += loadPlayerView;
        }

        public void onStart()
        {
            var now = DateTime.Now;
            API.setTime(now.Hour, now.Minute);
        }

        public void onPlayerConnected(Client player)
        {
            API.sendChatMessageToAll("Игрок " + player.name + " вошел на сервер!");
        }

        public void loadPlayerView(Client player)
        {
            var isAuthorized = true;// API.isPlayerLoggedIn(player);
            
            player.setSkin(API.pedNameToModel("FreeModeMale01"));
            player.dimension = isAuthorized ? 0 : 1;
            player.freeze(!isAuthorized);

            if (isAuthorized)
            {
                player.sendChatMessage("Вы авторизованы как " + API.getPlayerAclGroup(player) + "!");
            }
            else
            {
                player.sendChatMessage("Вы не авторизованы. (" + player.socialClubName + ")");
                API.triggerClientEvent(player, "client:presentStartWindow");
            }

        }
    }

}
