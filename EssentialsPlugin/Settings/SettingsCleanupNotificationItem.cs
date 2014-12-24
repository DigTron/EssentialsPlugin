﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EssentialsPlugin.Settings
{
	public class SettingsCleanupNotificationItem
	{
		private bool enabled;
		public bool Enabled
		{
			get { return enabled; }
			set { enabled = value; }
		}

		private int minutesBeforeCleanup;
		public int MinutesBeforeCleanup
		{
			get { return minutesBeforeCleanup; }
			set { minutesBeforeCleanup = Math.Max(1, value); }
		}

		private string message;
		public string Message
		{
			get { return message; }
			set { message = value; }
		}
	}
}
