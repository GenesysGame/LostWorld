using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTANetworkServer;
using GTANetworkShared;

namespace admin
{
    public class Console : Script
    {
        [Command("login", ACLRequired = true, SensitiveInfo = true)]
        public void AdminLogin(Client client, string password)
        {
            string reason;
            int result = API.loginPlayer(client, password);
            switch (result)
            {
                case 0:
                    reason = "~r~ERROR: No account found.";
                    break;
                case 1:
                case 3:
                    reason = "~g~SUCCESS: You have been logged in as " + client.name + ". Role: " + API.getPlayerAclGroup(client) + ".";
                    break;
                case 2:
                    reason = "~r~ERROR: Wrong password.";
                    break;
                case 4:
                    reason = "~r~ERROR: " + client.name + ", you're already logged in.";
                    break;
                case 5:
                    reason = "~r~ERROR: ACL is disabled.";
                    break;
                default:
                    reason = "~r~ERROR: Unknown error occurred.";
                    break;
            }
            API.sendChatMessageToPlayer(client, reason);
        }

        [Command("logout", ACLRequired = true)]
        public void LogoutCommand(Client sender)
        {
            if (!API.isPlayerLoggedIn(sender))
            {
                sender.sendChatMessage("~r~You are not logged in.");
                return;
            }
            API.logoutPlayer(sender);
            sender.sendChatMessage("~g~You have been logged out.");
        }

        [Command("stop", ACLRequired = true)]
        public void StopCommand(Client sender, string name)
        {
            var success = API.stopResource(name);
            if (success)
            {
                sender.sendChatMessage("~g~ Resource " + name + " stopped.");
            }
            else
            {
                sender.sendChatMessage("~r~ Couldn't stop resource " + name + ".");
            }
        }

        [Command("start", ACLRequired = true)]
        public void StartCommand(Client sender, string name)
        {
            var success = API.startResource(name);
            if (success)
            {
                sender.sendChatMessage("~g~ Resource " + name + " started.");
            }
            else
            {
                sender.sendChatMessage("~r~ Couldn't start resource " + name + ".");
            }
        }

        [Command("restart", ACLRequired = true)]
        public void RestartCommand(Client sender, string name)
        {
            StopCommand(sender, name);
            StartCommand(sender, name);
        }

        // MARK: - Helpers for admins

        [Command("vehicle", ACLRequired = true)]
        public void CreateVehicleCommand(Client sender, string name)
        {
            var model = API.vehicleNameToModel(name);
            var pos = sender.position;
            pos.X += 5;
            var rot = sender.rotation;
            API.createVehicle(model, pos, rot, 0, 0);
        }
    }
}
