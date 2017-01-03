using Shockah.Affix.Utils;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Shockah.Affix
{
	public class AffixItemInfo : ItemInfo
	{
		public readonly List<Affix> affixes = new List<Affix>();

		public override ItemInfo Clone()
		{
			AffixItemInfo clone = new AffixItemInfo();
			clone.affixes.AddRange(affixes);
			return clone;
		}

		public void ApplyAffix(Item item, Affix affix)
		{
			affixes.Add(affix);
		}

		public void RemoveAffix(Item item, Affix affix)
		{
			affixes.Remove(affix);
		}

		public string GetFormattedName(Item item, string oldName)
		{
			string name = oldName;
			foreach (var method in Hooks.Build<Affix, Func<Item, string, string>>(affixes, o => o.GetFormattedName))
			{
				name = method(item, name);
			}
			return name;
		}

		public void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			foreach (var method in Hooks.Build<Affix, Action<Item, List<TooltipLine>>>(affixes, o => o.ModifyTooltips))
			{
				method(item, tooltips);
			}
		}

		public void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
		{
			foreach (var method in Hooks.Build<Affix, Action<Item, Player, NPC, int, float, bool>>(affixes, o => o.OnHitNPC))
			{
				method(item, player, target, damage, knockBack, crit);
			}
		}

		public void OnHitNPC(Item item, Player player, Projectile projectile, NPC target, int damage, float knockBack, bool crit)
		{
			foreach (var method in Hooks.Build<Affix, Action<Item, Player, Projectile, NPC, int, float, bool>>(affixes, o => o.OnHitNPC))
			{
				method(item, player, projectile, target, damage, knockBack, crit);
			}
		}

		public void UpdateEquip(Item item, Player player)
		{
			foreach (var method in Hooks.Build<Affix, Action<Item, Player>>(affixes, o => o.UpdateEquip))
			{
				method(item, player);
			}
		}
	}
}