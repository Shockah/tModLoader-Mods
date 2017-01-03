using Shockah.Affix.Utils;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Shockah.Affix.Content
{
	public class WeaponHeldDefenseAffix : WeaponHeldAffix
	{
		public readonly int defense;

		public static readonly WeaponHeldDefenseAffix Defense3 = new WeaponHeldDefenseAffix("Hard", 3);
		public static readonly WeaponHeldDefenseAffix Defense6 = new WeaponHeldDefenseAffix("Guarding", 6);
		public static readonly WeaponHeldDefenseAffix Defense9 = new WeaponHeldDefenseAffix("Armored", 9);
		public static readonly WeaponHeldDefenseAffix Defense12 = new WeaponHeldDefenseAffix("Warding", 12);

		public static readonly TagDeserializer<WeaponHeldDefenseAffix> DESERIALIZER = new TagDeserializer<WeaponHeldDefenseAffix>(tag =>
		{
			return new WeaponHeldDefenseAffix(
				tag.GetString("name"),
				tag.GetInt("defense")
			);
		});

		public WeaponHeldDefenseAffix(string name, int defense) : base(name)
		{
			this.defense = defense;
		}

		public override void SerializeData(TagCompound tag)
		{
			base.SerializeData(tag);
			tag["defense"] = defense;
		}

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