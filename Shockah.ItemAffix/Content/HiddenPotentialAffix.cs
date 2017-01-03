using Microsoft.Xna.Framework;
using Shockah.Affix.Utils;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Shockah.Affix.Content
{
	public class HiddenPotentialAffix : Affix
	{
		public static readonly HiddenPotentialAffixFactory Self = new HiddenPotentialAffixFactory();

		public Affix hiddenAffix;
		public int progress = 0;
		public int required;
		public string requirementFormat = "Kills: {progress}/{required}";
		public HiddenPotentialProgressType progressType = HiddenPotentialProgressType.Kills;

		public HiddenPotentialAffix(HiddenPotentialAffixFactory factory) : base(factory, "Hidden Potential")
		{
		}

		protected bool HasRequiredData()
		{
			return hiddenAffix != null && required > 0 && requirementFormat != null;
		}

		[CallOrder(100)]
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (!HasRequiredData())
				return;
			TooltipLine line;
			string formatted;


			formatted = string.Format("Hidden Potential: {0}", hiddenAffix.name);
			line = new TooltipLine(ModLoader.GetMod("Shockah.Affix"), string.Format("{0}.Title", factory.internalName), formatted);
			line.overrideColor = new Color(1.0f, 0.5f, 0.0f);
			tooltips.Add(line);

			formatted = requirementFormat;
			formatted = formatted.Replace("{progress}", progress.ToString());
			formatted = formatted.Replace("{required}", required.ToString());
			line = new TooltipLine(ModLoader.GetMod("Shockah.Affix"), string.Format("{0}.Requirements", factory.internalName), formatted);
			line.overrideColor = new Color(1.0f, 0.5f, 0.0f);
			tooltips.Add(line);
		}

		public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (!HasRequiredData())
				return;
			if (progressType != HiddenPotentialProgressType.Hits)
				return;

			progress++;
			if (progress >= required)
			{
				item.RemoveAffix(this);
				item.ApplyAffix(hiddenAffix);
			}
		}
	}

	public class HiddenPotentialAffixFactory : AffixFactory
	{
		public HiddenPotentialAffixFactory() : base(typeof(OnHitBuffAffixFactory).FullName)
		{
		}

		public override Affix Create(Item item, TagCompound tag)
		{
			HiddenPotentialAffix affix = new HiddenPotentialAffix(this);
			if (tag != null)
			{
				string hiddenAffix = tag.GetString("hiddenAffix");
				AffixFactory factory = (ModLoader.GetMod("Shockah.Affix") as AffixMod).GetAffixFactory(hiddenAffix);
				if (factory == null)
					factory = new UnloadedAffixFactory(hiddenAffix);
				affix.hiddenAffix = factory.Create(item, tag["hiddenAffixTag"] as TagCompound);
				affix.progress = tag.GetInt("progress");
				affix.required = tag.GetInt("required");
				affix.requirementFormat = tag.GetString("requirementFormat");
				affix.progressType = (HiddenPotentialProgressType)Enum.Parse(typeof(HiddenPotentialProgressType), tag.GetString("progressType"));
			}
			return affix;
		}

		public override TagCompound Store(Item item, Affix affix)
		{
			TagCompound tag = new TagCompound();
			HiddenPotentialAffix myAffix = affix as HiddenPotentialAffix;

			tag["hiddenAffix"] = myAffix.factory.internalName;
			tag["progress"] = myAffix.progress;
			tag["required"] = myAffix.required;
			tag["requirementFormat"] = myAffix.requirementFormat;
			tag["progressType"] = Enum.GetName(typeof(HiddenPotentialProgressType), myAffix.progressType);

			TagCompound affixTag = myAffix.factory.Store(item, myAffix);
			if (affixTag != null)
				tag["hiddenAffixTag"] = affixTag;
			else
				tag["hiddenAffixTag"] = false;

			return tag;
		}
	}

	public enum HiddenPotentialProgressType
	{
		Hits,
		Kills
	}
}