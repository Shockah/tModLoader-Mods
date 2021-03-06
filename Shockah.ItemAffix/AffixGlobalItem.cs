﻿using System.Collections.Generic;
using System.Linq;
using Shockah.Affix.Utils;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Shockah.Affix
{
	class AffixGlobalItem : GlobalItem
	{
		public override bool NeedsSaving(Item item)
		{
			return item.CanApplyAffixes();
		}

		public override TagCompound Save(Item item)
		{
			AffixItemInfo info = item.GetAffixInfo(mod);
			TagCompound tag = new TagCompound();

			if (info != null && info.affixes.Count != 0)
				tag["affixes"] = info.affixes.Select(affix => (mod as AffixMod).Serialize(affix)).ToList();

			return tag;
		}

		public override void Load(Item item, TagCompound tag)
		{
			if (tag.HasTag("affixes"))
				item.GetAffixInfo(mod)?.affixes.AddRange(tag.GetList<TagCompound>("affixes").Select(affixTag => (mod as AffixMod).Deserialize(affixTag)));
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			AffixItemInfo info = item.GetAffixInfo(mod);
			if (info == null)
				return;

			TooltipLine nameLine = tooltips.Find(line => line.Name == "ItemName");
			if (nameLine != null)
				nameLine.text = info.GetFormattedName(item, nameLine.text);

			info.ModifyTooltips(item, tooltips);
		}

		public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
		{
			item.GetAffixInfo(mod)?.OnHitNPC(item, player, target, damage, knockBack, crit);
		}

		public override void UpdateEquip(Item item, Player player)
		{
			item.GetAffixInfo(mod)?.UpdateEquip(item, player);
		}
	}
}