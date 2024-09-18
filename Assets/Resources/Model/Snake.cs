using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake
{
    private readonly List<SnakePart> _parts = new();

    private Vector2 _startPosition;

    public Snake(Vector2 startPosition)
    {
        _startPosition = startPosition;
    }

    public IReadOnlyList<SnakePart> Parts => _parts;

    public SnakePart Head => _parts.Count == 0 ? null : _parts[0];

    public SnakePart Tail => _parts.Count == 0 ? null : _parts.Last();

    public void AddPart()
    {
        if(Parts.Count == 0)
            _parts.Add(new SnakePart(_startPosition, null, Vector3.zero));
        else
            _parts.Add(new SnakePart(Tail.Position, Tail, Tail.Rotation));
    }
}