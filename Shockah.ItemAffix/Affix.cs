using Shockah.Utils;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix
{
	public abstract class Affix : TagSerializable
	{
		public readonly string name;

		protected Affix(string name)
		{
			this.name = name;
		}

		public virtual void SerializeData(TagCompound tag)
		{
			tag["name"] = name;
		}

		public virtual string GetFormattedName(Item item, string oldName)
		{
			return oldName;
		}

		public virtual void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
		}

		public virtual void OnHitNPC(Item affixedItem, Item item, Player player, NPC target, int damage, float knockBack, bool crit)
		{
		}

		public virtual void OnHitNPC(Item affixedItem, Item item, Player player, Projectile projectile, NPC target, int damage, float knockBack, bool crit)
		{
		}

		public virtual void UpdateEquip(Item item, Player player)
		{
		}

		public virtual void ModifyHitByItem(Item item, Player player, NPC npc, ref int damage, ref float knockback, ref bool crit)
		{
		}

		public virtual void ModifyHitByProjectile(Item item, Projectile projectile, Player player, NPC npc, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
		}
	}
}