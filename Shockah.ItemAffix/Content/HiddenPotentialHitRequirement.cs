using Shockah.Affix.Utils;
using Terraria;
using Terraria.ModLoader.IO;

namespace Shockah.Affix.Content
{
	public class HiddenPotentialHitRequirement : HiddenPotentialIntRequirement
	{
		public static readonly TagDeserializer<HiddenPotentialHitRequirement> DESERIALIZER = new TagDeserializer<HiddenPotentialHitRequirement>(tag =>
		{
			HiddenPotentialHitRequirement requirement = new HiddenPotentialHitRequirement(tag.GetInt("required"));
			requirement.progress = tag.GetInt("progress");
			return requirement;
		});

		public HiddenPotentialHitRequirement(int required) : base(required)
		{
		}

		public override string GetRequirementTooltipName(Item item, HiddenPotentialAffix affix)
		{
			return "Hits";
		}

		public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit, HiddenPotentialAffix affix)
		{
			Progress(1, item, affix);
		}
	}
}