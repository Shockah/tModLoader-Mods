using System.Collections.Generic;
using System.Linq;
using Shockah.Utils;
using Terraria;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix.Content
{
	public abstract class BaneAffix : NamedItemAffix
	{
		public readonly List<NPCMatcher> matches = new List<NPCMatcher>();

		public BaneAffix(string name, string format = PrefixFormat) : base(name, format)
		{
		}

		public override void SerializeData(TagCompound tag)
		{
			base.SerializeData(tag);
			tag["format"] = format;
			if (matches.Count != 0)
				tag["matches"] = matches.Select(match => TagSerializables.Serialize(match)).ToList();
		}

		public virtual string FormatTooltip(string tooltip)
		{
			string lang = string.Join(", ", matches.Where(match => match.lang != null).Select(match => match.lang));
			tooltip = tooltip.Replace("{lang}", lang);
			return tooltip;
		}

		public virtual bool Matches(NPC npc)
		{
			foreach (NPCMatcher match in matches)
			{
				if (match.Matches(npc))
					return true;
			}
			return false;
		}
	}
}