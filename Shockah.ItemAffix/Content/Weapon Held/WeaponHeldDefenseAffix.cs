using Shockah.Affix.Utils;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System;

namespace Shockah.Affix.Content
{
	public class WeaponHeldDefenseAffix : WeaponHeldAffix
	{
		public static readonly List<Tuple<int, string>> affixNames = new List<Tuple<int, string>>
		{
			new Tuple<int, string>(10, "Warding"),
			new Tuple<int, string>(7, "Armored"),
			new Tuple<int, string>(4, "Guarding"),
			new Tuple<int, string>(1, "Hard")
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

		public static readonly TagDeserializer<WeaponHeldDefenseAffix> DESERIALIZER = new TagDeserializer<WeaponHeldDefenseAffix>(tag =>
		{
			return new WeaponHeldDefenseAffix(
				tag.GetInt("defense")
			);
		});

		public WeaponHeldDefenseAffix(int defense) : base(GetNameForDefenseValue(defense))
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
			TooltipLine line = new TooltipLine(ModLoader.GetMod(AffixMod.ModName), GetType().FullName, string.Format("+{0} defense while held", defense));
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