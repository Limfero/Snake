using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class HeadMovement : MonoBehaviour
{
    private readonly Quaternion _rigth = Quaternion.Euler(0, 0, -90);
    private readonly Quaternion _left = Quaternion.Euler(0, 0, 90);
    private readonly Quaternion _up = Quaternion.Euler(0, 0, 0);
    private readonly Quaternion _down = Quaternion.Euler(0, 0, 180);

    [SerializeField] private Transform _anchor;
    [SerializeField] private RotationView _prefabRotation;
    [SerializeField] private UserInput _input;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationDelay;

    private SnakePart _head;
    private Map _map;
    private Snake _snake;
    private Vector2 _target;
    private bool _canRotation = true;

    public void Init(SnakePart head, Map map, Snake snake)
    {
        _head = head;
        _map = map;
        (int, int) position = _map.GetPositionInMap(transform.position);
        _target = _map.Positions[position.Item1 + (int)transform.up.x, position.Item2 - (int)transform.up.y];
        _snake = snake;
    }

    public Transform Anchor => _anchor;
    public float Speed => _speed;

    private void OnEnable()
    {
        _input.Direction += TryRotate;
    }

    private void OnDisable()
    {
        _input.Direction -= TryRotate;
    }

    private void FixedUpdate()
    {
        if (transform.position == (Vector3)_target)
        {
            (int, int) position = _map.GetPositionInMap(transform.position);
            _target = _map.Positions[position.Item1 + (int)transform.up.x, position.Item2 - (int)transform.up.y];
        }

        transform.position = Vector2.MoveTowards(transform.position, _target, Time.fixedDeltaTime * _speed);
        _head.SetPosition(transform.position);
        _head.SetRotation(transform.rotation.eulerAngles);
    }

    public void Rotate(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
    
    private void TryRotate(Vector2 direction)
    {
        if (_canRotation == false)
            return;

        if (direction.x > 0 && transform.rotation != _left && transform.rotation != _rigth)
            CreateRotationTail(_rigth);
        else if(direction.x < 0 && transform.rotation != _left && transform.rotation != _rigth)
            CreateRotationTail(_left);
        else if(direction.y > 0 && transform.rotation != _down && transform.rotation != new Quaternion(_down.x, _down.y, -_down.z, _down.w) &&  transform.rotation != _up)
            CreateRotationTail(_up);
        else if (direction.y < 0 && transform.rotation != _down && transform.rotation != new Quaternion(_down.x, _down.y, -_down.z, _down.w) && transform.rotation != _up)
            CreateRotationTail(_down);
    }

    private void CreateRotationTail(Quaternion rotation)
    {
        RotationView rotationView = Instantiate(_prefabRotation, _target, Quaternion.identity);
        rotationView.Init(_snake, rotation, this);

        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        _canRotation = false;

        yield return new WaitForSeconds(_rotationDelay);

        _canRotation = true;
    }
}
