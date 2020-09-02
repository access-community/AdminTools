﻿using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace AdminTools.Commands.Size
{
    public class All : ICommand
    {
        public string Command { get; } = "all";

        public string[] Aliases { get; } = new string[] { "*" };

        public string Description { get; } = "Resizes all players to a specified x, y, and z value";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            EventHandlers.LogCommandUsed((CommandSender)sender, EventHandlers.FormatArguments(arguments, 0));
            if (!((CommandSender)sender).CheckPermission("at.size"))
            {
                response = "You do not have permission to use this command";
                return false;
            }

            if (arguments.Count != 3)
            {
                response = "Usage: size (all / *) (x value) (y value) (z value)";
                return false;
            }

            if (!float.TryParse(arguments.At(0), out float xval))
            {
                response = $"Invalid value for x size: {arguments.At(0)}";
                return false;
            }

            if (!float.TryParse(arguments.At(1), out float yval))
            {
                response = $"Invalid value for y size: {arguments.At(1)}";
                return false;
            }

            if (!float.TryParse(arguments.At(2), out float zval))
            {
                response = $"Invalid value for z size: {arguments.At(2)}";
                return false;
            }

            foreach (Player Ply in Player.List)
            {
                if (Ply.Role == RoleType.Spectator || Ply.Role == RoleType.None)
                    continue;

                EventHandlers.SetPlayerScale(Ply.GameObject, xval, yval, zval);
            }

            response = $"Everyone's scale has been set to {xval} {yval} {zval}";
            return true;
        }
    }
}