using Terraria;
using Terraria.ModLoader.IO;
using System;

namespace Shockah.ItemAffix.Content
{
	public class HiddenPotentialKillRequirement : HiddenPotentialNPCIntRequirement
	{
		public static readonly Func<TagCompound, HiddenPotentialKillRequirement> DESERIALIZER = tag =>
		{
			HiddenPotentialKillRequirement requirement = new HiddenPotentialKillRequirement(tag.GetInt("required"), tag.GetString("npcFamilyName"));
			requirement.progress = tag.GetInt("progress");
			if (tag.ContainsKey("matches"))
				requirement.matches.AddRange(tag.GetList<NPCMatcher>("matches"));
			return requirement;
		};

		public HiddenPotentialKillRequirement(int required, string npcFamilyName) : base(required, npcFamilyName)
		{
		}

		public override string GetRequirementTooltipName(Item item, HiddenPotentialAffix affix)
		{
			return $"{npcFamilyName ?? "Enemies"} killed";
		}

		public virtual void OnNPCKilled(NPC npc, Player player, Item item, HiddenPotentialAffix affix)
		{
			if (!Matches(npc))
				return;
			Progress(1, item, affix);
		}
	}
}