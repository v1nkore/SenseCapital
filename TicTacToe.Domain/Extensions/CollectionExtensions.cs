namespace TicTacToe.Domain.Extensions
{
	public static class CollectionExtensions
	{
		public static ICollection<TSource> InitStructIfNullAndAdd<TSource>(this ICollection<TSource>? collection, TSource source)
		{
			if (collection is null)
			{
				collection = new List<TSource>();
			}

			collection.Add(source);

			return collection;
		}

		public static ICollection<TSource> InitStructIfNullAndAddRange<TSource>(this ICollection<TSource>? collection, IEnumerable<TSource> source)
		{
			if (collection is null)
			{
				collection = new List<TSource>();
			}

			foreach (var src in source)
			{
				collection.Add(src);
			}

			return collection;
		}

		public static void InitIfNullAndAdd<TSource>(this ICollection<TSource>? collection, TSource source) where TSource : class
		{
			if (collection is null)
			{
				collection = new List<TSource>();
			}

			collection.Add(source);
		}

		public static void InitIfNullAndAddRange<TSource>(this ICollection<TSource>? collection, IEnumerable<TSource> source) where TSource : class
		{
			if (collection is null)
			{
				collection = new List<TSource>();
			}

			foreach (var src in source)
			{
				collection.Add(src);
			}
		}
	}
}
