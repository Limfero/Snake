using System.Collections.Generic;
using UnityEngine;

public class SnakeExpander : MonoBehaviour
{
    [SerializeField] private PartMovement _prefab;

    private Transform _spawnPosition;
    private Snake _snake;
    private float _speed;

    public void Init(Transform position, Snake snake, float speed)
    {
        _spawnPosition = position;
        _snake = snake;
        _speed = speed;
    }

    public void Expend()
    {
        _snake.AddPart();

        PartMovement movement = Instantiate(_prefab, _spawnPosition.position, Quaternion.identity);
        movement.Init(_snake.Tail, _speed);

        _spawnPosition = movement.Anchor;
    }
}
