using System.Collections.Generic;
using UnityEngine;

public class PartMovement : MonoBehaviour
{
    [SerializeField] private Transform _anchor;

    private SnakePart _part;
    private float _speed;
    private readonly Queue<(Vector2, Vector3)> targets = new();

    public void Init(SnakePart part, float speed)
    {
        _part = part;
        _speed = speed;
    }

    public Transform Anchor => _anchor;
    public SnakePart SnakePart => _part;

    private void FixedUpdate()
    {
        targets.Enqueue((_part.Target.Position, _part.Target.Rotation));

        var position = (Vector3)targets.Peek().Item1;
        var rotation = targets.Peek().Item2;

        if (transform.position == position)
        {
            targets.Dequeue();

            if (targets.Count > 0)
            {
                position = (Vector3)targets.Peek().Item1;
                rotation = targets.Peek().Item2;
            }
        }

        transform.SetPositionAndRotation(Vector2.MoveTowards(transform.position, position, _speed * Time.fixedDeltaTime), Quaternion.Euler(rotation));
        _part.SetPosition(transform.position);
        _part.SetRotation(transform.rotation.eulerAngles);
    }
}
