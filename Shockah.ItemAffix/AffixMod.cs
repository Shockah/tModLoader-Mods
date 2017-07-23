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

		public static AffixMod Instance => (AffixMod)ModLoader.GetMod(ModName);

		public override void Load()
		{
			//ChestGenManager = new ChestAffixGenManager();
			//AccessoryChestGenManager = new AccessoryChestAffixGenManager();
		}

		public Affix Deserialize(TagCompound tag)
		{
			try
			{
				return TagIO.Deserialize<Affix>(tag);
			}
			catch (TypeUnloadedException)
			{
				return new UnloadedAffix(tag);
			}
		}

		public TagCompound Serialize(Affix affix)
		{
			UnloadedAffix unloadedAffix = affix as UnloadedAffix;
			return unloadedAffix == null ? (TagCompound)TagIO.Serialize(affix) : unloadedAffix.SerializeData();
		}
	}
}