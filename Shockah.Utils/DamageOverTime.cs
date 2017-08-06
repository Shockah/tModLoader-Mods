using System;
using Terraria.ModLoader.IO;

namespace Shockah.Utils
{
	public class DamageOverTime : TagSerializable
	{
		public readonly float damage;
		public readonly int totalTime;
		public int currentTime = 0;

		public DamageOverTime(float damage, int totalTime)
		{
			this.damage = damage;
			this.totalTime = totalTime;
		}

		public static readonly Func<TagCompound, DamageOverTime> DESERIALIZER = tag =>
		{
			DamageOverTime self = new DamageOverTime(
				tag.GetFloat("damage"),
				tag.GetInt("totalTime")
			);
			self.currentTime = tag.GetInt("currentTime");
			return self;
		};

		public TagCompound SerializeData()
		{
			TagCompound tag = new TagCompound();
			tag["damage"] = damage;
			tag["totalTime"] = totalTime;
			tag["currentTime"] = currentTime;
			return tag;
		}
	}
}