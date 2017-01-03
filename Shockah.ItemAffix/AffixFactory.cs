using System;
using Terraria;
using Terraria.ModLoader.IO;

namespace Shockah.Affix
{
	public abstract class AffixFactory
	{
		public readonly string internalName;

		public AffixFactory(string internalName)
		{
			this.internalName = internalName;
		}

		public abstract Affix Create(Item item, TagCompound tag);

		public abstract TagCompound Store(Item item, Affix affix);
	}

	public class DelegateAffixFactory : AffixFactory
	{
		public readonly Func<Item, TagCompound, Affix> CreateDelegate;
		public readonly Func<Item, Affix, TagCompound> StoreDelegate;

		public DelegateAffixFactory(string internalName, Func<Item, TagCompound, Affix> createDelegate, Func<Item, Affix, TagCompound> storeDelegate) : base(internalName)
		{
			CreateDelegate = createDelegate;
			StoreDelegate = storeDelegate;
		}

		public override Affix Create(Item item, TagCompound tag)
		{
			return CreateDelegate(item, tag);
		}

		public override TagCompound Store(Item item, Affix affix)
		{
			return StoreDelegate(item, affix);
		}
	}
}