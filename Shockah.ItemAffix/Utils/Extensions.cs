﻿using System;
using Terraria;

namespace Shockah.Affix.Utils
{
	public static class Extensions
	{
		public static Player GetOwner(this Projectile self)
		{
			return self.owner < Main.player.Length - 1 && !self.trap && !self.npcProj ? Main.player[self.owner] : null;
		}

		public static T As<T>(this object obj, Action<T> @delegate = null) where T : class
		{
			T t = obj as T;
			if (t != null && @delegate != null)
				@delegate(t);
			return t;
		}
	}
}