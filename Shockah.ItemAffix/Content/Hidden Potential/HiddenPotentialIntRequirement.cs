using Terraria;
using Terraria.ModLoader.IO;

namespace Shockah.Affix.Content
{
	public abstract class HiddenPotentialIntRequirement : HiddenPotentialRequirement
	{
		public int progress = 0;
		public readonly int required;

		public HiddenPotentialIntRequirement(int required)
		{
			this.required = required;
		}

		public abstract string GetRequirementTooltipName(Item item, HiddenPotentialAffix affix);

		public override string GetTooltip(Item item, HiddenPotentialAffix affix)
		{
			string formatted = "{name}: {progress}/{required}";
			formatted = formatted.Replace("{name}", GetRequirementTooltipName(item, affix));
			formatted = formatted.Replace("{progress}", progress.ToString());
			formatted = formatted.Replace("{required}", required.ToString());
			return formatted;
		}

		public override void SerializeData(TagCompound tag)
		{
			tag["progress"] = progress;
			tag["required"] = required;
		}

		public void Progress(int progress, Item item, HiddenPotentialAffix affix)
		{
			this.progress += progress;
			if (this.progress >= required)
				affix.OnRequirementDone(item);
		}
	}
}