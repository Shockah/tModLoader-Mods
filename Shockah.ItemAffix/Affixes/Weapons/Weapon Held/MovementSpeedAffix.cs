using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System;
using Shockah.Utils;

namespace Shockah.ItemAffix.Content
{
	public class MovementSpeedAffix : WeaponHeldAffix
	{
		public static readonly List<Tuple<float, string>> affixNames = new List<Tuple<float, string>>
		{
			Tuple.Create(0.15f, "Quick"),
			Tuple.Create(0.10f, "Hasty"),
			Tuple.Create(0.05f, "Fleeting"),
			Tuple.Create(0.00f, "Brisk")
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

		public static readonly Func<TagCompound, MovementSpeedAffix> DESERIALIZER = tag =>
		{
			return new MovementSpeedAffix(
				tag.GetFloat("movementSpeed")
			);
		};

		public MovementSpeedAffix(float movementSpeed) : base(GetNameForMovementSpeedValue(movementSpeed))
		{
			this.movementSpeed = movementSpeed;
		}

		public override TagCompound SerializeData()
		{
			TagCompound tag = base.SerializeData();
			tag["movementSpeed"] = movementSpeed;
			return tag;
		}

		[CallOrder(-900)]
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine line = new TooltipLine(ModLoader.GetMod(AffixMod.ModName), GetType().FullName, $"+{(movementSpeed * 100):F0}% movement speed while held");
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