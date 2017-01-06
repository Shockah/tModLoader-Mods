using Terraria;

namespace Shockah.ItemAffix
{
	public abstract class NamedItemAffix : Affix
	{
		public readonly string format;

		public NamedItemAffix(string name, bool prefixedName = true) : this(name, prefixedName ? "{affix} {item}" : "{item} {affix}")
		{
		}

		public NamedItemAffix(string name, string format) : base(name)
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
}