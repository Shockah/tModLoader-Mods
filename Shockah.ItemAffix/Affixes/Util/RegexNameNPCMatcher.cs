using System;
using System.Text.RegularExpressions;
using Shockah.Utils;
using Terraria;
using Terraria.ModLoader.IO;

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

		public override void SerializeData(TagCompound tag)
		{
			tag["regex"] = regex;
		}

		public override bool Matches(NPC npc)
		{
			return Regex.Match(npc.displayName, regex).Success;
		}
	}
}
