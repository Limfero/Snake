using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class RotationView : MonoBehaviour
{
    private Snake _snake;
    private SpriteRenderer _spriteRenderer;
    private HeadMovement _headMovement;
    private Quaternion _rotation;

    public void Init(Snake snake, Quaternion rotation, HeadMovement movement)
    {
        _snake = snake;
        _rotation = rotation;
        _headMovement = movement;
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
    }

    private void Update()
    {
        if (transform.position == (Vector3)_snake.Head.Position)
        {
            _spriteRenderer.enabled = true;
            _headMovement.Rotate(_rotation);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PartMovement movement))
            if (movement.SnakePart == _snake.Tail && (Vector3)movement.SnakePart.Position == transform.position)
                Destroy(gameObject);
    }
}
