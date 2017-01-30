using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System;
using Shockah.Utils;

namespace Shockah.ItemAffix.Content
{
	public class DefenseAffix : WeaponHeldAffix
	{
		public static readonly List<Tuple<int, string>> affixNames = new List<Tuple<int, string>>
		{
			Tuple.Create(10, "Warding"),
			Tuple.Create(7, "Armored"),
			Tuple.Create(4, "Guarding"),
			Tuple.Create(1, "Hard")
		};

		public readonly int defense;

		private static string GetNameForDefenseValue(int defense)
		{
			foreach (Tuple<int, string> tuple in affixNames)
			{
				if (defense >= tuple.Item1)
					return tuple.Item2;
			}
			return null;
		}

		public static readonly Func<TagCompound, DefenseAffix> DESERIALIZER = tag =>
		{
			return new DefenseAffix(
				tag.GetInt("defense")
			);
		};

		public DefenseAffix(int defense) : base(GetNameForDefenseValue(defense))
		{
			this.defense = defense;
		}

		public override void SerializeData(TagCompound tag)
		{
			tag["defense"] = defense;
		}

		[CallOrder(-900)]
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine line = new TooltipLine(ModLoader.GetMod(AffixMod.ModName), GetType().FullName, $"+{defense} defense while held");
			line.isModifier = true;
			line.isModifierBad = false;
			tooltips.Add(line);
		}

		public override void UpdateWeaponHeld(Item item, Player player)
		{
			player.statDefense += defense;
		}
	}
}