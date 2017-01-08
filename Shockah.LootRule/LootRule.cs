using System;
using System.Collections.Generic;
using Terraria;
using Shockah.Utils;
using Shockah.Utils.Rule;

namespace Shockah.LootRule
{
	public interface ILootResult<in T>
	{
		void Perform(T input);
	}

	public class ItemLootResult : ILootResult<Entity>
	{
		public readonly Item item;

		public ItemLootResult(int itemID) : this(itemID, 1)
		{
		}

		public ItemLootResult(int itemID, Dynamic<int> stack)
		{
			item = new Item();
			item.SetDefaults(itemID, true);
			item.stack = stack;
		}

		public ItemLootResult(Item item)
		{
			this.item = item;
		}

		public virtual void Perform(Entity input)
		{
			Item.NewItem(input.position, input.Size, item.netID, item.stack, false, item.prefix);
		}
	}

	public class DelegateLootResult<T> : ILootResult<T>
	{
		public readonly Action<T> @delegate;

		public DelegateLootResult(Action<T> @delegate)
		{
			this.@delegate = @delegate;
		}

		public void Perform(T input)
		{
			@delegate(input);
		}
	}

	public abstract class LootRule<T> : IRule<T, ILootResult<T>>
	{
		public abstract object Clone();

		public abstract List<ILootResult<T>> GetOutput(T input, Random random);
	}

	public class ItemLootRule<T> : LootRule<T> where T : Entity
	{
		public readonly Dynamic<Item> item;

		public ItemLootRule(Dynamic<Item> item)
		{
			this.item = item;
		}

		public ItemLootRule(Dynamic<int> itemID) : this(itemID, 1)
		{
		}

		public ItemLootRule(Dynamic<int> itemID, Dynamic<int> stack)
		{
			item = (Func<Item>)(() =>
			{
				Item item = new Item();
				item.SetDefaults(itemID);
				item.stack = stack;
				return item;
			});
		}

		public override object Clone()
		{
			return new ItemLootRule<T>(item);
		}

		public override List<ILootResult<T>> GetOutput(T input, Random random)
		{
			List<ILootResult<T>> output = new List<ILootResult<T>>();
			output.Add(new ItemLootResult(item));
			return output;
		}

		public static implicit operator ItemLootRule<T>(short itemID)
		{
			return new ItemLootRule<T>(itemID);
		}

		public static implicit operator ItemLootRule<T>(Dynamic<int> itemID)
		{
			return new ItemLootRule<T>(itemID);
		}
	}

	public class DelegateLootRule<T> : LootRule<T> where T : Entity
	{
		public readonly Action<T> @delegate;

		public DelegateLootRule(Action<T> @delegate)
		{
			this.@delegate = @delegate;
		}

		public override object Clone()
		{
			return new DelegateLootRule<T>(@delegate);
		}

		public override List<ILootResult<T>> GetOutput(T input, Random random)
		{
			List<ILootResult<T>> output = new List<ILootResult<T>>();
			output.Add(new DelegateLootResult<T>(@delegate));
			return output;
		}

		public static implicit operator DelegateLootRule<T>(Action<T> @delegate)
		{
			return new DelegateLootRule<T>(@delegate);
		}
	}
}