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
        }

        public void onStart()
        {
            var now = DateTime.Now;
            API.setTime(now.Hour, now.Minute);
        }

        public void onPlayerConnected(Client player)
        {
            API.sendChatMessageToAll("Игрок " + player.name + " вошел на сервер!");

            if (API.isPlayerLoggedIn(player))
            {
                player.sendChatMessage("Вы авторизованы как " + API.getPlayerAclGroup(player) + "!");
                player.freeze(false);
            }
            else
            {
                player.sendChatMessage("Вы не авторизованы. (" + player.socialClubName + ")");
                player.freeze(true);
                API.triggerClientEvent(player, "main.presentStartWindow");
            }

            loadPlayerView(player);
        }

        public void loadPlayerView(Client player)
        {
            player.setSkin(API.pedNameToModel("FreeModeMale01"));
        }
    }

}
