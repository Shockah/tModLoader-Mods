using System.Linq;
using Shockah.Utils;
using Terraria;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix.Content
{
	public class HiddenPotentialKillRequirement : HiddenPotentialNPCIntRequirement
	{
		public static readonly TagDeserializer<HiddenPotentialKillRequirement> DESERIALIZER = new TagDeserializer<HiddenPotentialKillRequirement>(tag =>
		{
			HiddenPotentialKillRequirement requirement = new HiddenPotentialKillRequirement(tag.GetInt("required"), tag.GetString("npcFamilyName"));
			requirement.progress = tag.GetInt("progress");
			if (tag.HasTag("matches"))
				requirement.matches.AddRange(tag.GetList<TagCompound>("matches").Select(matchTag => TagSerializables.Deserialize<NPCMatcher>(matchTag)));
			return requirement;
		});

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