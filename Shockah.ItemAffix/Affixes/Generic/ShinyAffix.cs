using System;
using System.Collections.Generic;
using Shockah.Utils;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System;

namespace Shockah.ItemAffix.Content
{
	public class ShinyAffix : NamedItemAffix
	{
		public readonly float valueMod;

		public static readonly Func<TagCompound, ShinyAffix> DESERIALIZER = tag =>
		{
			return new ShinyAffix(
				tag.GetFloat("valueMod")
			);
		};

		public ShinyAffix(float valueMod = 3.0f) : base(valueMod >= 1.0f ? "Shiny" : "Imitation", valueMod >= 1.0f ? PrefixFormat : SuffixFormat)
		{
			this.valueMod = valueMod;
		}

		public override TagCompound SerializeData()
		{
			TagCompound tag = base.SerializeData();
			tag["valueMod"] = valueMod;
			return tag;
		}

		public override void OnApply(Item item)
		{
			item.ResetToDefaultKeepingModInfo();
			item.GetAffixInfo().ApplyChanges(item);
		}

		public override void OnRemove(Item item)
		{
			item.ResetToDefaultKeepingModInfo();
			item.GetAffixInfo().ApplyChanges(item);
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine line = new TooltipLine(ModLoader.GetMod(AffixMod.ModName), GetType().FullName, $"{(valueMod * 100):F0}% shop price");
			line.isModifier = true;
			line.isModifierBad = false;
			tooltips.Add(line);
		}

		public override void ApplyChanges(Item item)
		{
			item.value = (int)(item.value * valueMod);
			Main.NewText($"Setting value to {item.value}");
		}
	}
}