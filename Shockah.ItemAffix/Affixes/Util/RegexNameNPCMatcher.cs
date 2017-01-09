using System.Text.RegularExpressions;
using Shockah.Utils;
using Terraria;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix.Content
{
	public class RegexNameNPCMatcher : NPCMatcher
	{
		public static readonly TagDeserializer<RegexNameNPCMatcher> DESERIALIZER = new TagDeserializer<RegexNameNPCMatcher>(tag =>
		{
			return new RegexNameNPCMatcher(
				tag.GetString("lang"),
				tag.GetString("regex")
			);
		});

		public readonly string regex;

		public RegexNameNPCMatcher(string lang, string regex) : base(lang)
		{
			this.regex = regex;
		}

		public override void SerializeData(TagCompound tag)
		{
			base.SerializeData(tag);
			tag["regex"] = regex;
		}

		public override bool Matches(NPC npc)
		{
			return Regex.Match(npc.displayName, regex).Success;
		}
	}
}
