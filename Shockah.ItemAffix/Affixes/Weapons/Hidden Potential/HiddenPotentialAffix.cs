using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Shockah.Utils;
using System;

namespace Shockah.ItemAffix.Content
{
	public class HiddenPotentialAffix : Affix
	{
		public readonly Affix hiddenAffix;
		public readonly HiddenPotentialRequirement requirement;

		public static readonly Func<TagCompound, HiddenPotentialAffix> DESERIALIZER = tag =>
		{
			return new HiddenPotentialAffix(
				tag.Get<Affix>("hiddenAffix"),
				tag.Get<HiddenPotentialRequirement>("requirement")
			);
		};

		public HiddenPotentialAffix(Affix hiddenAffix, HiddenPotentialRequirement requirement) : base("Hidden Potential")
		{
			this.hiddenAffix = hiddenAffix;
			this.requirement = requirement;
		}

		public override TagCompound SerializeData()
		{
			TagCompound tag = base.SerializeData();
			tag["hiddenAffix"] = hiddenAffix;
			tag["requirement"] = requirement;
			return tag;
		}

		[CallOrder(100)]
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine line;

			line = new TooltipLine(ModLoader.GetMod(AffixMod.ModName), $"{GetType().FullName}.Title", $"Hidden Potential: {hiddenAffix.name}");
			line.overrideColor = new Color(1.0f, 0.5f, 0.0f);
			tooltips.Add(line);

			line = new TooltipLine(ModLoader.GetMod(AffixMod.ModName), $"{GetType().FullName}.Requirements", requirement.GetTooltip(item, this));
			line.overrideColor = new Color(1.0f, 0.5f, 0.0f);
			tooltips.Add(line);
		}

		public override void OnHitNPC(Item affixedItem, Item item, Player player, NPC target, int damage, float knockBack, bool crit)
		{
			requirement.OnHitNPC(item, player, target, damage, knockBack, crit, this);
		}

		public override void OnHitNPC(Item affixedItem, Item item, Player player, Projectile projectile, NPC target, int damage, float knockBack, bool crit)
		{
			requirement.OnHitNPC(item, player, projectile, target, damage, knockBack, crit, this);
		}

		public void OnRequirementDone(Item item)
		{
			item.RemoveAffix(this);
			item.ApplyAffix(hiddenAffix);
		}
	}
}
