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

		public void ApplyAffix(Item item, string internalName)
		{
			ApplyAffix<Affix>(item, internalName, null);
		}

		public void ApplyAffix(Item item, AffixFactory factory)
		{
			ApplyAffix<Affix>(item, factory, null);
		}

		public void ApplyAffix<T>(Item item, string internalName, Action<T> createDelegate) where T : Affix
		{
			AffixFactory factory = (mod as AffixMod).GetAffixFactory(internalName);
			if (factory == null)
				throw new Exception(string.Format("Unknown AffixFactory '{0}'.", factory.internalName));
			ApplyAffix(item, factory, createDelegate);
		}

		public void ApplyAffix<T>(Item item, AffixFactory factory, Action<T> createDelegate) where T : Affix
		{
			Affix affix = factory.Create(item, null);
			createDelegate?.Invoke((T)affix);
			ApplyAffix(item, affix);
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
	}
}