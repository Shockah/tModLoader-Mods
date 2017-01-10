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
		public readonly string npcFamilyName;

		public BaneAffix(string npcFamilyName) : this(BaneName(npcFamilyName), PrefixFormat, npcFamilyName)
		{
		}

		public BaneAffix(string name, string npcFamilyName) : this(name, PrefixFormat, npcFamilyName)
		{
		}

		public BaneAffix(string name, string format, string npcFamilyName) : base(name, format)
		{
			this.npcFamilyName = npcFamilyName;
		}

		public override void SerializeData(TagCompound tag)
		{
			base.SerializeData(tag);
			tag["format"] = format;
			tag["npcFamilyName"] = npcFamilyName;
			if (matches.Count != 0)
				tag["matches"] = matches.Select(match => TagSerializables.Serialize(match)).ToList();
		}

		public virtual string FormatTooltip(string tooltip)
		{
			tooltip = tooltip.Replace("{family}", npcFamilyName);
			return tooltip;
		}

		public virtual bool Matches(NPC npc)
		{
			if (matches.Count == 0)
				return true;
			foreach (NPCMatcher match in matches)
			{
				if (match.Matches(npc))
					return true;
			}
			return false;
		}

		public BaneAffix WithMatches(params NPCMatcher[] matches)
		{
			this.matches.AddRange(matches);
			return this;
		}

		public virtual HiddenPotentialAffix AsHiddenPotentialWithHitRequirement(int required)
		{
			HiddenPotentialHitRequirement requirement = new HiddenPotentialHitRequirement(required, npcFamilyName);
			requirement.matches.AddRange(matches);
			return new HiddenPotentialAffix(this, requirement);
		}

		public virtual HiddenPotentialAffix AsHiddenPotentialWithKillRequirement(int required)
		{
			HiddenPotentialKillRequirement requirement = new HiddenPotentialKillRequirement(required, npcFamilyName);
			requirement.matches.AddRange(matches);
			return new HiddenPotentialAffix(this, requirement);
		}

		public static string BaneName(string npcFamilyName)
		{
			if (npcFamilyName.EndsWith("s"))
				npcFamilyName = npcFamilyName.Remove(npcFamilyName.Length - 1);
			return string.Format("{0}bane", npcFamilyName);
		}
	}
}