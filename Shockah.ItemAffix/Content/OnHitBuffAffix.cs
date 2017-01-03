using Shockah.Affix.Utils;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Shockah.Affix.Content
{
	public class OnHitBuffAffix : NamedAffix
	{
		public static readonly AffixFactory Fiery = new OnHitBuffAffixFactory("Fiery", BuffID.OnFire, 60 * 5, "Puts enemies on fire");
		public static readonly AffixFactory Poisoned = new OnHitBuffAffixFactory("Poisoned", BuffID.Poisoned, 60 * 5, "Poisons enemies");

		public OnHitBuffAffix(OnHitBuffAffixFactory factory, string name, bool prefixedName = true) : base(factory, name, prefixedName)
		{
		}

		public OnHitBuffAffix(OnHitBuffAffixFactory factory, string name, string format) : base(factory, name, format)
		{
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			OnHitBuffAffixFactory factory = this.factory as OnHitBuffAffixFactory;
			if (factory.tooltip != null)
			{
				TooltipLine line = new TooltipLine(ModLoader.GetMod("Shockah.Affix"), factory.internalName, factory.tooltip);
				line.isModifier = true;
				line.isModifierBad = false;
				tooltips.Add(line);
			}
		}

		public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
		{
			OnHitBuffAffixFactory factory = this.factory as OnHitBuffAffixFactory;
			target.AddBuff(factory.buffType, factory.buffTime);
		}
	}

	public class OnHitBuffAffixFactory : NamedAffixFactory
	{
		public readonly int buffType;
		public readonly Dynamic<int> buffTime;
		public readonly string tooltip;

		public OnHitBuffAffixFactory(string name, int buffType, Dynamic<int> buffTime, string tooltip) : this(name, true, buffType, buffTime, tooltip)
		{
		}

		public OnHitBuffAffixFactory(string name, bool prefixedName, int buffType, Dynamic<int> buffTime, string tooltip) : base(string.Format("{0}{1}", typeof(OnHitBuffAffixFactory).FullName, buffType), name, prefixedName)
		{
			this.buffType = buffType;
			this.buffTime = buffTime;
			this.tooltip = tooltip;
		}

		public OnHitBuffAffixFactory(string name, string format, int buffType, Dynamic<int> buffTime, string tooltip) : base(string.Format("{0}{1}", typeof(OnHitBuffAffixFactory).FullName, buffType), name, format)
		{
			this.buffType = buffType;
			this.buffTime = buffTime;
			this.tooltip = tooltip;
		}

		public override Affix Create(Item item, TagCompound tag)
		{
			return new OnHitBuffAffix(this, name, format);
		}

		public override TagCompound Store(Item item, Affix affix)
		{
			return null;
		}
	}
}