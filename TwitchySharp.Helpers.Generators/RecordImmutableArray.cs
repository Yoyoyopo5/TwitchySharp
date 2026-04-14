using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace TwitchySharp.Helpers.Generators;

/// <summary>
/// Equal maps to SequenceEqual on the underlying <see cref="ImmutableArray"/>.
/// </summary>
internal class RecordImmutableArray<T>(ImmutableArray<T> array) : IEquatable<RecordImmutableArray<T>>, IReadOnlyList<T>
{
    public RecordImmutableArray() : this(ImmutableArray<T>.Empty) { }
    private readonly ImmutableArray<T> _array = array;
    public int Count => ((IReadOnlyCollection<T>)_array).Count;
    public T this[int index] => ((IReadOnlyList<T>)_array)[index];
    public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)_array).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_array).GetEnumerator();
    public bool Equals(RecordImmutableArray<T> other)
        => _array.SequenceEqual(other._array);
    public override bool Equals(object obj)
        => obj is RecordImmutableArray<T> other && Equals(other);
    public static bool operator ==(RecordImmutableArray<T> left, RecordImmutableArray<T> right)
        => left.Equals(right);
    public static bool operator !=(RecordImmutableArray<T> left, RecordImmutableArray<T> right)
        => !left.Equals(right);
    public override int GetHashCode()
    {
        HashCode hc = new();
        foreach (T item in _array)
            hc.Add(item);
        return hc.ToHashCode();
    }
}

internal static class ImmutableArrayExtensions
{
    public static RecordImmutableArray<T> ToRecordImmutableArray<T>(this IEnumerable<T> source)
        => new(source.ToImmutableArray());

    public static RecordImmutableArray<T> ToRecordImmutableArray<T>(this ImmutableArray<T> array)
        => new(array);
}
