using System;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private SnakeExpander _expander;
    [SerializeField] private CoinSpawner _spawner;

    public event Action Died;
    public event Action<Coin> CoinCollected;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            CoinCollected?.Invoke(coin);
            _expander.Expend();
            _spawner.Relese(coin);
            return;
        }

        if (collision.gameObject.TryGetComponent(out RotationView _))
            return;

        Died?.Invoke();
    }
}
