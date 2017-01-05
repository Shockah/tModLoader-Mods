using Shockah.Affix.Utils;
using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Shockah.Affix
{
	public class AffixItemInfo : ItemInfo
	{
		public readonly List<Affix> affixes = new List<Affix>();

		private Func<Item, string, string>[] hooksGetFormattedName;
		private Action<Item, List<TooltipLine>>[] hooksModifyTooltips;
		private Action<Item, Player, NPC, int, float, bool>[] hooksOnHitNPC;
		private Action<Item, Player, Projectile, NPC, int, float, bool>[] hooksOnHitNPC2;
		private Action<Item, Player>[] hooksUpdateEquip;

		public override ItemInfo Clone()
		{
			AffixItemInfo clone = new AffixItemInfo();
			clone.affixes.AddRange(affixes);
			clone.SetupHooks();
			return clone;
		}

		public void ApplyAffix(Item item, Affix affix)
		{
			affixes.Add(affix);
			SetupHooks();
		}

		public void RemoveAffix(Item item, Affix affix)
		{
			affixes.Remove(affix);
			SetupHooks();
		}

		private IEnumerable<R> BuildHooks<R>(Func<Affix, R> func) where R : class
		{
			return Hooks.Build<Affix, R>(affixes, func);
		}

		private void SetupHooks()
		{
			hooksGetFormattedName = BuildHooks<Func<Item, string, string>>(o => o.GetFormattedName).ToArray();
			hooksModifyTooltips = BuildHooks<Action<Item, List<TooltipLine>>>(o => o.ModifyTooltips).ToArray();
			hooksOnHitNPC = BuildHooks<Action<Item, Player, NPC, int, float, bool>>(o => o.OnHitNPC).ToArray();
			hooksOnHitNPC2 = BuildHooks<Action<Item, Player, Projectile, NPC, int, float, bool>>(o => o.OnHitNPC).ToArray();
			hooksUpdateEquip = BuildHooks<Action<Item, Player>>(o => o.UpdateEquip).ToArray();
		}

		public string GetFormattedName(Item item, string oldName)
		{
			string name = oldName;
			foreach (var method in hooksGetFormattedName)
			{
				name = method(item, name);
			}
			return name;
		}

		public void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			foreach (var method in hooksModifyTooltips)
			{
				method(item, tooltips);
			}
		}

		public void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
		{
			foreach (var method in hooksOnHitNPC)
			{
				method(item, player, target, damage, knockBack, crit);
			}
		}

		public void OnHitNPC(Item item, Player player, Projectile projectile, NPC target, int damage, float knockBack, bool crit)
		{
			foreach (var method in hooksOnHitNPC2)
			{
				method(item, player, projectile, target, damage, knockBack, crit);
			}
		}

		public void UpdateEquip(Item item, Player player)
		{
			foreach (var method in hooksUpdateEquip)
			{
				method(item, player);
			}
		}
	}
}