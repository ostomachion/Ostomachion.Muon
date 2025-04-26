namespace Ostomachion.Muon.Extensions;

internal static class EnumeratorExtensions
{
    public static IEnumerator<T> Singleton<T>(T item) { yield return item; }
}
