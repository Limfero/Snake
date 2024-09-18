using UnityEngine;
using UnityEngine.SceneManagement;

public class Root : MonoBehaviour
{
    [SerializeField] private HeadMovement _headMovement;
    [SerializeField] private SnakeExpander _expander;
    [SerializeField] private CollisionDetector _detector;
    [SerializeField] private CoinSpawner _spawner;
    [SerializeField] private Transform _mapSprite;
    [SerializeField] private Transform _wallSprite;
    [SerializeField] private Vector2 _startSnakePosition;
    [SerializeField] private Vector2 _mapOffset;
    [SerializeField] private int _countParts = 2;
    [SerializeField] private int _size;
    [SerializeField] private float _shift;

    private Snake _snake;
    private Map _map;

    private void OnEnable()
    {
        _detector.Died += Die;
    }

    private void OnDisable()
    {
        _detector.Died -= Die;
    }

    private void Awake()
    {
        _map = new Map(_size, _shift, _mapOffset.x, _mapOffset.y);
        DrowMap();

        _snake = new Snake(_headMovement.transform.position);
        _snake.AddPart();
    }

    private void Start()
    {
        _spawner.Init(_map);

        Vector2 position = _map.Positions[(int)_startSnakePosition.x, (int)_startSnakePosition.y];
        _headMovement.transform.position = position;
        _headMovement.Init(_snake.Head, _map, _snake);

        _expander.Init(_headMovement.Anchor, _snake, _headMovement.Speed);

        for (int i = 0; i < _countParts; i++)
            _expander.Expend();
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void DrowMap()
    {
        for (int i = 0; i < _map.Positions.GetLength(0); i++)
        {
            Instantiate(_wallSprite, _map.Positions[i, 0], Quaternion.identity, transform);
            Instantiate(_wallSprite, _map.Positions[i, _map.Positions.GetLength(1) - 1], Quaternion.identity, transform);

            for (int j = 0; j < _map.Positions.GetLength(1); j++)
            {
                Instantiate(_mapSprite, _map.Positions[i, j], Quaternion.identity, transform);

                Instantiate(_wallSprite, _map.Positions[0, j], Quaternion.identity, transform);
                Instantiate(_wallSprite, _map.Positions[_map.Positions.GetLength(0) - 1, j], Quaternion.identity, transform);
            }
        }

    }
}
