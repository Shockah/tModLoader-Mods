using System.Linq;
using Shockah.Utils;
using Terraria;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix.Content
{
	public class HiddenPotentialHitRequirement : HiddenPotentialNPCIntRequirement
	{
		public static readonly TagDeserializer<HiddenPotentialHitRequirement> DESERIALIZER = new TagDeserializer<HiddenPotentialHitRequirement>(tag =>
		{
			HiddenPotentialHitRequirement requirement = new HiddenPotentialHitRequirement(tag.GetInt("required"), tag.GetString("npcFamilyName"));
			requirement.progress = tag.GetInt("progress");
			if (tag.HasTag("matches"))
				requirement.matches.AddRange(tag.GetList<TagCompound>("matches").Select(matchTag => TagSerializables.Deserialize<NPCMatcher>(matchTag)));
			return requirement;
		});

		public HiddenPotentialHitRequirement(int required, string npcFamilyName) : base(required, npcFamilyName)
		{
		}

		public override string GetRequirementTooltipName(Item item, HiddenPotentialAffix affix)
		{
			return string.Format("{0} hit: ", npcFamilyName ?? "Enemies");
		}

		public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit, HiddenPotentialAffix affix)
		{
			if (!Matches(target))
				return;
			Progress(1, item, affix);
		}

		public override void OnHitNPC(Item item, Player player, Projectile projectile, NPC target, int damage, float knockBack, bool crit, HiddenPotentialAffix affix)
		{
			OnHitNPC(item, player, target, damage, knockBack, crit, affix);
		}
	}
}