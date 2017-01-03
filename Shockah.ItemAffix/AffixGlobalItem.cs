using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Shockah.Affix
{
	class AffixGlobalItem : GlobalItem
	{
		public override bool NeedsSaving(Item item)
		{
			return (mod as AffixMod).CanApplyAffixes(item);
		}

		public override TagCompound Save(Item item)
		{
			AffixItemInfo info = item.GetModInfo<AffixItemInfo>(mod);
			TagCompound tag = new TagCompound();

			if (info.affixes.Count != 0)
			{
				TagCompound affixesTag = new TagCompound();
				foreach (Affix affix in info.affixes)
				{
					TagCompound affixTag = affix.factory.Store(item, affix);
					if (affixTag != null)
						affixesTag[affix.factory.internalName] = affixTag;
					else
						affixesTag[affix.factory.internalName] = false;
				}
				tag["affixes"] = affixesTag;
			}

			return tag;
		}

		public override void Load(Item item, TagCompound tag)
		{
			if (tag.HasTag("affixes"))
			{
				AffixItemInfo info = item.GetModInfo<AffixItemInfo>(mod);

				TagCompound affixesTag = tag["affixes"] as TagCompound;
				foreach (KeyValuePair<string, object> kvp in affixesTag)
				{
					AffixFactory factory = (mod as AffixMod).GetAffixFactory(kvp.Key);
					if (factory == null)
						factory = new UnloadedAffixFactory(kvp.Key);
					info.affixes.Add(factory.Create(item, kvp.Value as TagCompound));
				}
			}
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			AffixItemInfo info = item.GetModInfo<AffixItemInfo>(mod);

			TooltipLine nameLine = tooltips.Find(line => line.Name == "ItemName");
			if (nameLine != null)
				nameLine.text = info.GetFormattedName(item, nameLine.text);

			info.ModifyTooltips(item, tooltips);
		}

		public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
		{
			AffixItemInfo info = item.GetModInfo<AffixItemInfo>(mod);
			info.OnHitNPC(item, player, target, damage, knockBack, crit);
		}
	}
}