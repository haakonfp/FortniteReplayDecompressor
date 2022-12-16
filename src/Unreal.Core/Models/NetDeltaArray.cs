﻿using System.Collections.Generic;

namespace Unreal.Core.Models;

public class NetDeltaArray<T>
{
	public ICollection<T> Items => _items.Values;
	public int Count => _items.Count;

	private Dictionary<int, T> _items = new();

	public bool DeleteIndex(int index)
	{
		return _items.Remove(index);
	}

	public bool TryAddItem(int index, T item)
	{
		return _items.TryAdd(index, item);
	}

	public bool TryGetItem(int index, out T item)
	{
		return _items.TryGetValue(index, out item);
	}

	public void Clear()
	{
		_items.Clear();
	}
}
