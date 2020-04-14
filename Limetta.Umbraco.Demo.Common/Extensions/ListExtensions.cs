using System.Collections.Generic;
using System.Linq;

namespace Limetta.Umbraco.Demo.Common.Extensions
{
	public static class ListExtensions
	{
		public static bool IsValid<T>(this IEnumerable<T> list, int minItems = 0)
		{
			if (list == null)
				return false;

			if (minItems <= 1)
				return list.Any();

			return list.Count() >= minItems;
		}

		public static bool IsInvalid<T>(this IEnumerable<T> list, int minItems = 0)
		{
			return !list.IsValid(minItems);
		}

		public static bool HasItems<T>(this IEnumerable<T> list, int minItems = 1)
		{
			return list.IsValid(minItems);
		}

		public static bool IsEmpty<T>(this IEnumerable<T> list)
		{
			return !list.IsValid(1);
		}
	}
}
