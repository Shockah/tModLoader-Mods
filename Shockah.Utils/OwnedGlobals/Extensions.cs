using System.Collections.Generic;
using Terraria;

namespace Shockah.Utils.OwnedGlobals
{
	public static class Extensions
	{
		public static Item GetWeapon(this Projectile projectile)
		{
			return projectile.GetGlobalProjectile<OwnedGlobalProjectile>().weapon;
		}

		public static IDictionary<Player, IList<Item>> GetParticipants(this NPC npc)
		{
			return npc.GetGlobalNPC<OwnedGlobalNPC>().participants;
		}
	}
}