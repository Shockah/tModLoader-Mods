using System;
using Terraria;

namespace Shockah.ItemAffix.Utils
{
	public static class Extensions
	{
		public static Player GetOwner(this Projectile self)
		{
			return self.owner < Main.player.Length - 1 && !self.trap && !self.npcProj ? Main.player[self.owner] : null;
		}

		public static T As<O, T>(this O obj, Action<T> @delegate = null, Action<O> elseDelegate = null) where O : class where T : class
		{
			T t = obj as T;
			if (t != null)
			{
				@delegate?.Invoke(t);
			}
			else
			{
				@elseDelegate?.Invoke(obj);
			}
			return t;
		}

		public static bool Between(this int self, int a, int b)
		{
			return self >= Math.Min(a, b) && self <= Math.Max(a, b);
		}

		public static bool Between(this float self, float a, float b)
		{
			return self >= Math.Min(a, b) && self <= Math.Max(a, b);
		}

		public static bool Between(this double self, double a, double b)
		{
			return self >= Math.Min(a, b) && self <= Math.Max(a, b);
		}
	}
}