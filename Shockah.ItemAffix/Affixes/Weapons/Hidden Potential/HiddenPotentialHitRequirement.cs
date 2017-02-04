using System;
using Terraria;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix.Content
{
	public class HiddenPotentialHitRequirement : HiddenPotentialNPCIntRequirement
	{
		public static readonly Func<TagCompound, HiddenPotentialHitRequirement> DESERIALIZER = tag =>
		{
			HiddenPotentialHitRequirement requirement = new HiddenPotentialHitRequirement(tag.GetInt("required"), tag.GetString("npcFamilyName"));
			requirement.progress = tag.GetInt("progress");
			if (tag.ContainsKey("matches"))
				requirement.matches.AddRange(tag.GetList<NPCMatcher>("matches"));
			return requirement;
		};

		public HiddenPotentialHitRequirement(int required, string npcFamilyName) : base(required, npcFamilyName)
		{
		}

		public override string GetRequirementTooltipName(Item item, HiddenPotentialAffix affix)
		{
			return $"{npcFamilyName ?? "Enemies"} hit";
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