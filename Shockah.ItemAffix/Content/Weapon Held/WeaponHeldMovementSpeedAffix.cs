using Shockah.Affix.Utils;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System;

namespace Shockah.Affix.Content
{
	public class WeaponHeldMovementSpeedAffix : WeaponHeldAffix
	{
		public static readonly List<Tuple<float, string>> affixNames = new List<Tuple<float, string>>
		{
			new Tuple<float, string>(0.15f, "Quick"),
			new Tuple<float, string>(0.10f, "Hasty"),
			new Tuple<float, string>(0.05f, "Fleeting"),
			new Tuple<float, string>(0.00f, "Brisk")
		};

		public readonly float movementSpeed;

		private static string GetNameForMovementSpeedValue(float movementSpeed)
		{
			foreach (Tuple<float, string> tuple in affixNames)
			{
				if (movementSpeed > tuple.Item1)
					return tuple.Item2;
			}
			return null;
		}

		public static readonly TagDeserializer<WeaponHeldMovementSpeedAffix> DESERIALIZER = new TagDeserializer<WeaponHeldMovementSpeedAffix>(tag =>
		{
			return new WeaponHeldMovementSpeedAffix(
				tag.GetFloat("movementSpeed")
			);
		});

		public WeaponHeldMovementSpeedAffix(float movementSpeed) : base(GetNameForMovementSpeedValue(movementSpeed))
		{
			this.movementSpeed = movementSpeed;
		}

		public override void SerializeData(TagCompound tag)
		{
			tag["movementSpeed"] = movementSpeed;
		}

		[CallOrder(-900)]
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine line = new TooltipLine(ModLoader.GetMod(AffixMod.ModName), GetType().FullName, string.Format("+{0:0}% movement speed while held", movementSpeed * 100));
			line.isModifier = true;
			line.isModifierBad = false;
			tooltips.Add(line);
		}

		public override void UpdateWeaponHeld(Item item, Player player)
		{
			player.moveSpeed += movementSpeed;
		}
	}
}