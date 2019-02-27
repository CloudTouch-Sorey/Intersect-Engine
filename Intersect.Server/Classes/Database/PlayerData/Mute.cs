﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
using JetBrains.Annotations;

// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace Intersect.Server.Database.PlayerData
{
    public class Mute
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        public Guid PlayerId { get; private set; }
        public virtual User Player { get; private set; }
        public string Ip { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; set; }
        public string Reason { get; private set; }
        public string Muter { get; private set; }

        public Mute()
        {
        }

        public Mute(User player, string ip, string reason, int durationDays, string muter)
        {
            Player = player;
            Ip = ip;
            StartTime = DateTime.UtcNow;
            Reason = reason;
            EndTime = StartTime.AddDays(durationDays);
            Muter = muter;
        }

        //Bans and Mutes
        public static void AddMute([NotNull] Client player, int duration, [NotNull] string reason,
            [NotNull] string muter, string ip)
        {
            var mute = new Mute(player.User, ip, reason, duration, muter);
            PlayerContext.Current.Mutes.Add(mute);
            player.User.SetMuted(true,
                Strings.Account.mutestatus.ToString(mute.StartTime, mute.Muter, mute.EndTime, mute.Reason));
        }

        public static void DeleteMute([NotNull] User user)
        {
            var mute = PlayerContext.Current.Mutes.SingleOrDefault(p => p.Player == user);
            if (mute != null)
            {
                PlayerContext.Current.Mutes.Remove(mute);
            }
            user.SetMuted(false, "");
        }

        public static string CheckMute([NotNull] User user, string ip)
        {
            var mute = PlayerContext.Current.Mutes.SingleOrDefault(p => p.Player == user) ??
                       PlayerContext.Current.Mutes.SingleOrDefault(p => p.Ip == ip);

            if (mute == null)
            {
                return null;
            }

            user.SetMuted(true,
                Strings.Account.mutestatus.ToString(mute.StartTime, mute.Muter, mute.EndTime, mute.Reason));
            return user.GetMuteReason();
        }
    }
}