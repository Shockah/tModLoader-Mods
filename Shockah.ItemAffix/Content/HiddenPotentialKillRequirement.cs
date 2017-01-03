using Shockah.Affix.Utils;
using Terraria;
using Terraria.ModLoader.IO;

namespace Shockah.Affix.Content
{
	public class HiddenPotentialKillRequirement : HiddenPotentialIntRequirement
	{
		public static readonly TagDeserializer<HiddenPotentialKillRequirement> DESERIALIZER = new TagDeserializer<HiddenPotentialKillRequirement>(tag =>
		{
			HiddenPotentialKillRequirement requirement = new HiddenPotentialKillRequirement(tag.GetInt("required"));
			requirement.progress = tag.GetInt("progress");
			return requirement;
		});

		public HiddenPotentialKillRequirement(int required) : base(required)
		{
		}

		public override string GetRequirementTooltipName(Item item, HiddenPotentialAffix affix)
		{
			return "Kills";
		}

		public virtual void OnNPCKilled(NPC npc, Player player, Item item, HiddenPotentialAffix affix)
		{
			Progress(1, item, affix);
		}
	}
}