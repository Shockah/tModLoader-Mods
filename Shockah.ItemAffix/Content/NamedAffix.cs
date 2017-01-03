using Terraria;

namespace Shockah.Affix.Content
{
	public abstract class NamedAffix : Affix
	{
		public readonly string format;

		public NamedAffix(NamedAffixFactory factory, string name, bool prefixedName = true) : this(factory, name, prefixedName ? "{affix} {item}" : "{item} {affix}")
		{
		}

		public NamedAffix(NamedAffixFactory factory, string name, string format) : base(factory, name)
		{
			this.format = format;
		}

		public override string GetFormattedName(Item item, string oldName)
		{
			string format = this.format;
			if (item.name.Contains(format.Replace("{item}", "").Replace("{affix}", name)))
				format = "{item}";
			format = format.Replace("{affix}", name);
			format = format.Replace("{item}", oldName);
			return format;
		}
	}

	public abstract class NamedAffixFactory : AffixFactory
	{
		public readonly string name;
		public readonly string format;

		public NamedAffixFactory(string internalName, string name, bool prefixedName = true) : this(internalName, name, prefixedName ? "{affix} {item}" : "{item} {affix}")
		{
		}

		public NamedAffixFactory(string internalName, string name, string format) : base(internalName)
		{
			this.name = name;
			this.format = format;
		}
	}
}