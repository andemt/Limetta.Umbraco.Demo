using System;
using System.Collections.Generic;
using System.Linq;

namespace Limetta.Umbraco.Demo.Common.Extensions
{
	public static class StringExtensions
	{
		public static bool IsValid(this string text, int minLength = 0)
		{
			return !text.IsInvalid(minLength);
		}

		public static bool IsInvalid(this string text, int minLength = 0)
		{
			if (string.IsNullOrWhiteSpace(text))
				return true;

			text = text.Trim();

			if (text.Length < minLength)
				return true;

			return false;
		}

		public static string SubstringSafe(this string text, int startIndex, int length, bool addEllipsis = false)
		{
			if (text.IsInvalid())
				return text;

			if (startIndex > text.Length)
				startIndex = text.Length;

			if (length > text.Length - startIndex)
				length = text.Length - startIndex;

			return text.Substring(startIndex, length);
		}

		public static bool EqualsAny(this string text, params string[] sameAsTexts)
		{
			return EqualsAny(text, sameAsTexts, false);
		}

		public static bool EqualsAnyIgnoreCase(this string text, params string[] sameAsTexts)
		{
			return EqualsAny(text, sameAsTexts, true);
		}

		private static bool EqualsAny(this string text, string[] sameAsTexts, bool ignoreCase)
		{
			foreach (var sameAsText in sameAsTexts)
			{
				if (ignoreCase && text.EqualsIgnoreCase(sameAsText))
					return true;
				if (text.Equals(sameAsText))
					return true;
			}

			return false;
		}

		public static bool EqualsIgnoreCase(this string text, string sameAsText)
		{
			return text.IsValid() && text.Equals(sameAsText, StringComparison.InvariantCultureIgnoreCase);
		}

		public static bool EndsWithIgnoreCase(this string text, string sameAsText)
		{
			return text.IsValid() && text.EndsWith(sameAsText, StringComparison.InvariantCultureIgnoreCase);
		}

		public static bool ContainsAny(this string text, params string[] anyTexts)
		{
			if (text == null || anyTexts.IsEmpty())
				return false;

			return anyTexts.FirstOrDefault(x => text.Contains(x)) != null;
		}

		public static string Fallback(this string text, string fallbackText, int minLength = 0)
		{
			return !text.IsInvalid(minLength) ? text : fallbackText;
		}

		public static int ToInt(this string input, int fallback = 0)
		{
			if (input == null)
				return fallback;

			if (int.TryParse(input, out var integer))
				return integer;

			return fallback;
		}

		public static string MergeWithComma(this string value, params object[] values)
		{
			return MergeWithSeparator(value, ", ", values);
		}

		public static string MergeWithDash(this string value, params object[] values)
		{
			return MergeWithSeparator(value, "-", values);
		}

		public static string MergeWithUnderscore(this string value, params object[] values)
		{
			return MergeWithSeparator(value, "_", values);
		}

		public static string MergeWithSpace(this string value, params object[] values)
		{
			return MergeWithSeparator(value, " ", values);
		}

		public static string MergeWithDot(this string value, params object[] values)
		{
			return MergeWithSeparator(value, ". ", values);
		}

		public static string MergeWithSeparator(this string value, string separator, params object[] values)
		{
			var list = values.IsEmpty()
				? new List<object>()
				: values.ToList();

			if (value.IsValid())
				list.Insert(0, value);

			if (list.IsEmpty())
				return string.Empty;

			return string.Join(separator, list.Where(x => x?.ToString().IsValid() ?? false));
		}
	}
}
