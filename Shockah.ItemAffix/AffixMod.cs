using Shockah.ItemAffix.Utils;
using System;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix
{
	public class AffixMod : Mod
	{
		public const string ModName = "Shockah.ItemAffix";

		public override string Name => ModName;

		public AffixMod()
		{
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
			};
		}

		public Affix Deserialize(TagCompound tag)
		{
			try
			{
				return TagSerializables.Deserialize<Affix>(tag);
			}
			catch (TypeUnloadedException)
			{
				TagCompound dataTag = tag.HasTag("data") ? tag.GetCompound("data") : null;
				return new UnloadedAffix(tag.GetString("type"), dataTag);
			}
		}

		public TagCompound Serialize(Affix affix)
		{
			UnloadedAffix unloadedAffix = affix as UnloadedAffix;
			if (unloadedAffix == null)
			{
				return TagSerializables.Serialize(affix);
			}
			else
			{
				TagCompound tag = new TagCompound();
				tag["type"] = unloadedAffix.typeName;
				if (unloadedAffix.tag != null)
					tag["data"] = unloadedAffix.tag;
				return tag;
			}
		}
	}
}