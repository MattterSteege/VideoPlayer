namespace ChemCourses.Utils;

public static class LinqExtensions
{
    /// <summary>
    /// Shuffles the source enumerable
    /// </summary>
    /// <param name="source">The source enumerable</param>
    /// <typeparam name="T">The type of the source enumerable</typeparam>
    /// <returns>The source enumerable shuffled</returns>
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        return source.OrderBy(x => Guid.NewGuid());
    }
    
    /// <summary>
    /// Duplicates the source enumerable a given amount of times
    /// A list 1, 2, 3 with 2 times will return 1, 2, 3, 1, 2, 3
    /// </summary>
    /// <param name="source">The source enumerable</param>
    /// <param name="times">The amount of times to duplicate the source</param>
    /// <typeparam name="T">The type of the source enumerable</typeparam>
    /// <returns>The source enumerable duplicated a given amount of times</returns>
    public static IEnumerable<T> Duplicate<T>(this IEnumerable<T> source, int times)
    {
        //a list 1, 2, 3 with 2 times will return 1, 2, 3, 1, 2, 3
        return Enumerable.Range(0, times).SelectMany(_ => source);
    }
    
    /// <summary>
    /// Duplicates the source enumerable a given amount of times and shuffles the result
    /// A list 1, 2, 3 with 2 times will return (for example) 1, 3, 2, 1, 2, 3
    /// </summary>
    /// <param name="source">The source enumerable</param>
    /// <param name="times">The amount of times to duplicate the source</param>
    /// <typeparam name="T">The type of the source enumerable</typeparam>
    /// <returns>A shuffled enumerable with the source duplicated a given amount of times</returns>
    public static IEnumerable<T> DuplicateRandom<T>(this IEnumerable<T> source, int times)
    {
        //a list 1, 2, 3 with 2 times will return 1, 3, 2, 1, 2, 3
        return Enumerable.Range(0, times).SelectMany(_ => source.Shuffle());
    }
    
    /// <summary>
    /// Returns a random subset of the source
    /// </summary>
    /// <param name="source">The source to take the subset from</param>
    /// <param name="count">The amount of elements to take</param>
    /// <typeparam name="T">The type of the source enumerable</typeparam>
    /// <returns>A random subset of the source</returns>
    public static IEnumerable<T> RandomSubset<T>(this IEnumerable<T> source, int count)
    {
        return source.Shuffle().Take(count);
    }
}