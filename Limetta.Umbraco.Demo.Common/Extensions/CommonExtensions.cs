using System.Linq;

namespace Limetta.Umbraco.Demo.Common.Extensions
{
	public static class CommonExtensions
	{
		public static bool ToBool(this object value, bool fallback = false)
		{
			if (value == null)
				return fallback;

			return bool.TryParse(value.ToString().ToLower(), out var tryBool)
				? tryBool
				: fallback;
		}

		public static bool In<T>(this T type, params T[] compare)
			where T : class
		{
			if (compare.IsEmpty())
				return false;

			return compare.Any(x => x == type);
		}
	}
}
