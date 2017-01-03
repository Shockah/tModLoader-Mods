using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Shockah.Affix
{
	public abstract class Affix
    {
		public readonly AffixFactory factory;
		public readonly string name;

		public Affix(AffixFactory factory, string name)
		{
			this.factory = factory;
			this.name = name;
		}

		public virtual string GetFormattedName(Item item, string oldName)
		{
			return oldName;
		}

		public virtual void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
		}

		public virtual void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
		{
		}
	}
}