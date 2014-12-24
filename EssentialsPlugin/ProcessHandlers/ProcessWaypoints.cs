﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Sandbox.ModAPI;
using Sandbox.Common.ObjectBuilders;
using VRageMath;

using SEModAPIInternal.API.Common;
using SEModAPIInternal.API.Entity;
using SEModAPIInternal.API.Entity.Sector.SectorObject;
using SEModAPIInternal.API.Entity.Sector.SectorObject.CubeGrid;
using SEModAPIInternal.API.Entity.Sector.SectorObject.CubeGrid.CubeBlock;

using EssentialsPlugin.Utility;
using EssentialsPlugin.Settings;

namespace EssentialsPlugin.ProcessHandler
{
	public class ProcessWaypoints : ProcessHandlerBase
	{
		private List<ulong> m_waypointAdd = new List<ulong>();

		public ProcessWaypoints()
		{

		}

		public override int GetUpdateResolution()
		{
			return 5000;
		}

		public override void Handle()
		{
			lock (m_waypointAdd)
			{
				if(m_waypointAdd.Count < 1)
					return;
			}

			if (MyAPIGateway.Players == null)
				return;

			List<IMyPlayer> players = new List<IMyPlayer>();
			bool result = false;
			Wrapper.GameAction(() =>
			{
				try
				{
					MyAPIGateway.Players.GetPlayers(players, null);
					result = true;
				}
				catch (Exception ex)
				{
					Logging.WriteLineAndConsole(string.Format("Waypoints(): Unable to get player list: {0}", ex.ToString()));
				}
			});

			if (!result)
				return;

			lock (m_waypointAdd)
			{
				for (int r = m_waypointAdd.Count - 1; r >= 0; r--)
				{
					ulong steamId = m_waypointAdd[r];

					IMyPlayer player = players.FirstOrDefault(x => x.SteamUserId == steamId && x.Controller != null && x.Controller.ControlledEntity != null);
					if (player != null)
					{
						Logging.WriteLineAndConsole("Player in game, creating waypoints");
						m_waypointAdd.Remove(steamId);

						// Add defaults
						if (Waypoints.Instance.Get(steamId).Count < 1)
						{
							foreach (ServerWaypointItem item in PluginSettings.Instance.WaypointDefaultItems)
							{
								WaypointItem newItem = new WaypointItem();
								newItem.Name = item.Name;
								newItem.Text = item.Name;
								newItem.WaypointType = WaypointTypes.Neutral;
								newItem.Position = new Vector3D(item.X, item.Y, item.Z);
								newItem.SteamId = steamId;
								Waypoints.Instance.Add(newItem);
							}
						}

						List<WaypointItem> waypointItems = Waypoints.Instance.Get(steamId);
						string waypoints = "";
						foreach (WaypointItem item in waypointItems)
						{
							if(waypoints != "")
								waypoints += "\r\n";

							waypoints += string.Format("/waypoint add \"{0}\" \"{1}\" {2} {3} {4} {5}", item.Name.ToLower(), item.Text, item.WaypointType.ToString(), item.Position.X, item.Position.Y, item.Position.Z);
						}

						// Add server waypoints
						foreach (ServerWaypointItem item in PluginSettings.Instance.WaypointServerItems)
						{
							if (!item.Enabled)
								continue;

							if (waypoints != "")
								waypoints += "\r\n";

							waypoints += string.Format("/waypoint add \"{0}\" \"{0}\" Neutral {1} {2} {3}", item.Name, item.X, item.Y, item.Z);
						}

						if(waypoints != "")
							Communication.SendClientMessage(steamId, waypoints);
					}
				}
			}

			base.Handle();
		}

		public override void OnPlayerJoined(ulong remoteUserId)
		{
			if (!PluginSettings.Instance.WaypointsEnabled)
				return;

			lock(m_waypointAdd)
			{
				//if (Waypoints.Instance.Get(remoteUserId).Count < 1)
					//return;

				m_waypointAdd.Add(remoteUserId);
			}

			base.OnPlayerJoined(remoteUserId);
		}

		public override void OnPlayerLeft(ulong remoteUserId)
		{
			lock (m_waypointAdd)
			{
				m_waypointAdd.RemoveAll(x => x == remoteUserId);
			}

			base.OnPlayerLeft(remoteUserId);
		}

	}
}

