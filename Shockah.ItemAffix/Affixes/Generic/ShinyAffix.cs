using System.Collections.Generic;
using Shockah.Utils;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix.Content
{
	public class ShinyAffix : NamedItemAffix
	{
		public readonly float valueMod;

		public static readonly TagDeserializer<ShinyAffix> DESERIALIZER = new TagDeserializer<ShinyAffix>(tag =>
		{
			return new ShinyAffix(
				tag.GetFloat("valueMod")
			);
		});

		public ShinyAffix(float valueMod = 3.0f) : base(valueMod >= 1.0f ? "Shiny" : "Imitation", valueMod >= 1.0f ? PrefixFormat : SuffixFormat)
		{
			this.valueMod = valueMod;
		}

		public override void SerializeData(TagCompound tag)
		{
			base.SerializeData(tag);
			tag["valueMod"] = valueMod;
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
			TooltipLine line = new TooltipLine(ModLoader.GetMod(AffixMod.ModName), GetType().FullName, string.Format("{0:0}% shop price", valueMod * 100));
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