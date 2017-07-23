using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Shockah.Utils.OwnedGlobals
{
	public class OwnedGlobalNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;
		public NPC owner { get; private set; }
		public IDictionary<Player, IList<Item>> participants { get; private set; }

		public override GlobalNPC NewInstance(NPC npc)
		{
			OwnedGlobalNPC global = (OwnedGlobalNPC)base.NewInstance(npc);
			global.owner = npc;
			global.participants = new Dictionary<Player, IList<Item>>();
			return global;
		}

		public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
		{
			AddParticipant(player, item);
		}

		public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
		{
			Item weapon = projectile.GetWeapon();
			if (weapon != null)
				AddParticipant(projectile.GetOwner(), weapon);
		}

		public void AddParticipant(Player participant, Item weapon)
		{
			if (!participants.ContainsKey(participant))
				participants[participant] = new List<Item>();

			IList<Item> items = participants[participant];
			if (!items.Contains(weapon))
				items.Add(weapon);
		}
	}
}
