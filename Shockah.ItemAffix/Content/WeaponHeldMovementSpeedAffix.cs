﻿using Shockah.Affix.Utils;
using Terraria;
using Terraria.ModLoader.IO;

namespace Shockah.Affix.Content
{
	public class WeaponHeldMovementSpeedAffix : WeaponHeldAffix
	{
		public readonly float movementSpeed;

		public static readonly WeaponHeldMovementSpeedAffix MoveSpeed5 = new WeaponHeldMovementSpeedAffix("Brisk", 0.05f);
		public static readonly WeaponHeldMovementSpeedAffix MoveSpeed10 = new WeaponHeldMovementSpeedAffix("Fleeting", 0.1f);
		public static readonly WeaponHeldMovementSpeedAffix MoveSpeed15 = new WeaponHeldMovementSpeedAffix("Hasty", 0.15f);
		public static readonly WeaponHeldMovementSpeedAffix MoveSpeed20 = new WeaponHeldMovementSpeedAffix("Quick", 0.2f);

		public static readonly TagDeserializer<WeaponHeldMovementSpeedAffix> DESERIALIZER = new TagDeserializer<WeaponHeldMovementSpeedAffix>(tag =>
		{
			return new WeaponHeldMovementSpeedAffix(
				tag.GetString("name"),
				tag.GetFloat("movementSpeed")
			);
		});

		public WeaponHeldMovementSpeedAffix(string name, float movementSpeed) : base(name)
		{
			this.movementSpeed = movementSpeed;
		}

		public override void SerializeData(TagCompound tag)
		{
			base.SerializeData(tag);
			tag["movementSpeed"] = movementSpeed;
		}

		public override void UpdateWeaponHeld(Item item, Player player)
		{
			player.moveSpeed += movementSpeed;
		}
	}
}