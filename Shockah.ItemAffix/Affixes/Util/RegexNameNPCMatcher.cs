using System.Text.RegularExpressions;
using Terraria;
using Terraria.ModLoader.IO;
using System;

namespace Shockah.ItemAffix.Content
{
	public class RegexNameNPCMatcher : NPCMatcher
	{
		public static readonly Func<TagCompound, RegexNameNPCMatcher> DESERIALIZER = tag =>
		{
			return new RegexNameNPCMatcher(tag.GetString("regex"));
		};

		public readonly string regex;

		public RegexNameNPCMatcher(string regex)
		{
			this.regex = regex;
		}

		public override TagCompound SerializeData()
		{
			TagCompound tag = new TagCompound();
			tag["regex"] = regex;
			return tag;
		}

		public override bool Matches(NPC npc)
		{
			return Regex.Match(npc.displayName, regex).Success;
		}
	}
}
