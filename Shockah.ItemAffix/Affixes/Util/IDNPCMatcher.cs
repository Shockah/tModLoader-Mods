using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader.IO;
using System;

namespace Shockah.ItemAffix.Content
{
	public class IDNPCMatcher : NPCMatcher
	{
		public static readonly Func<TagCompound, IDNPCMatcher> DESERIALIZER = tag =>
		{
			IDNPCMatcher matcher = new IDNPCMatcher();
			if (tag.ContainsKey("ids"))
				matcher.ids.AddRange(tag.GetList<int>("ids"));
			return matcher;
		};

		public readonly List<int> ids = new List<int>();

		public IDNPCMatcher With(IEnumerable<int> ids)
		{
			this.ids.AddRange(ids);
			return this;
		}

		public IDNPCMatcher With(params int[] ids)
		{
			this.ids.AddRange(ids);
			return this;
		}

		public override TagCompound SerializeData()
		{
			TagCompound tag = new TagCompound();
			if (ids.Count != 0)
				tag["ids"] = ids;
			return tag;
		}

		public override bool Matches(NPC npc)
		{
			return ids.Contains(npc.type);
		}
	}
}
