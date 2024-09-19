using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private HeadMovement _headMovement;
    [SerializeField] private SnakeExpander _expander;
    [SerializeField] private CollisionDetector _detector;
    [SerializeField] private CoinSpawner _spawner;
    [SerializeField] private EndGamePanel _endGamePanel;
    [SerializeField] private StartGamePanel _startGamePanel;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private Vector2 _startSnakePosition;
    [SerializeField] private Vector2 _mapOffset;
    [SerializeField] private int _countParts = 2;
    [SerializeField] private Vector2 _size;
    [SerializeField] private float _shift;
    [SerializeField] List<Transform> _mapTails;
    [SerializeField] List<Transform> _mapWalls;

    private Snake _snake;
    private Map _map;

    private void OnEnable()
    {
        _detector.Died += Die;
        _endGamePanel.Restarted += Restart;
        _startGamePanel.Started += OnStartGameClick;
    }

    private void OnDisable()
    {
        _detector.Died -= Die;
        _endGamePanel.Restarted -= Restart;
        _startGamePanel.Started -= OnStartGameClick;
    }

    private void Awake()
    {
        _endGamePanel.Disable();
        Time.timeScale = 0;

        _map = new Map((int)_size.x,(int)_size.y, _shift, _mapOffset.x, _mapOffset.y);
        DrowMap();
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        _snake = new Snake(_headMovement.transform.position);
        _snake.AddPart();

        _spawner.Init(_map);

        Vector2 position = _map.Positions[(int)_startSnakePosition.x, (int)_startSnakePosition.y];
        _headMovement.transform.SetPositionAndRotation(position, Quaternion.Euler(Vector3.up));
        _headMovement.Init(_snake.Head, _map, _snake);

        _expander.Init(_headMovement.Anchor, _snake, _headMovement.Speed);

        for (int i = 0; i < _countParts; i++)
            _expander.Expend();

        _scoreView.Clear();
    }

    private void OnStartGameClick()
    {
        Time.timeScale = 1;
        _startGamePanel.Disable();
    }

    private void Die()
    {
        Time.timeScale = 0;
        _endGamePanel.Enable();
    }

    private void Restart()
    {
        _endGamePanel.Disable();
        OnStartGameClick();

        foreach (var part in _expander.Parts)
            Destroy(part.gameObject);

        StartGame();
    }

    private void DrowMap()
    {
        int tailsCount = 0;
        int wallsCount = 0;

        for (int i = 0; i < _map.Positions.GetLength(0); i++)
        {
            _mapWalls[wallsCount++].position = _map.Positions[i, 0];
            _mapWalls[wallsCount++].position = _map.Positions[i, _map.Positions.GetLength(1) - 1];

            for (int j = 0; j < _map.Positions.GetLength(1); j++)
                _mapTails[tailsCount++].position = _map.Positions[i, j];
        }

        for (int i = 1; i < _map.Positions.GetLength(1) - 1; i++)
        {
            _mapWalls[wallsCount++].position = _map.Positions[0, i];
            _mapWalls[wallsCount++].position = _map.Positions[_map.Positions.GetLength(0) - 1, i];
        }
    }
}
