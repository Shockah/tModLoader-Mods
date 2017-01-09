﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using Shockah.Utils;
using Terraria;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix.Content
{
	public class IDNPCMatcher : NPCMatcher
	{
		public static readonly TagDeserializer<IDNPCMatcher> DESERIALIZER = new TagDeserializer<IDNPCMatcher>(tag =>
		{
			IDNPCMatcher matcher = new IDNPCMatcher(tag.GetString("lang"));
			if (tag.HasTag("ids"))
				matcher.ids.AddRange(tag.GetList<int>("ids"));
			return matcher;
		});

		public readonly List<int> ids = new List<int>();

		public IDNPCMatcher(string lang) : base(lang)
		{
		}

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

		public override void SerializeData(TagCompound tag)
		{
			base.SerializeData(tag);
			if (ids.Count != 0)
				tag["ids"] = ids;
		}

		public override bool Matches(NPC npc)
		{
			return ids.Contains(npc.type);
		}
	}
}
