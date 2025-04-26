namespace Ostomachion.Muon.Extensions;

internal static class EnumerableExtensions
{
    public static IEnumerable<T> Yield<T>(T item) { yield return item; }
}
