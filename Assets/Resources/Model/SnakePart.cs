using UnityEngine;

public class SnakePart
{
    private Vector2 _position;
    private Vector3 _rotation;
    private readonly SnakePart _target;

    public SnakePart(Vector2 position, SnakePart target, Vector3 rotation)
    {
        _position = position;
        _target = target;
        _rotation = rotation;
    }

    public Vector2 Position => _position;
    public Vector3 Rotation => _rotation;
    public SnakePart Target => _target;

    public void SetPosition(Vector2 position) => _position = position;
    public void SetRotation(Vector3 rotation) => _rotation = rotation;
}