using System.Collections;
using UnityEngine;

public class CoinSpawner : Spawner<Coin>
{
    [SerializeField] private float _repeatRate = 1f;

    private Map _map;

    public void Init(Map map)
    {
        _map = map;
    }

    private void Start()
    {
        StartCoroutine(Countdown());
    }

    protected override Coin Spawn() => Instantiate(Prefab, transform);

    private IEnumerator Countdown()
    {
        var wait = new WaitForSeconds(_repeatRate);

        yield return wait;

        while (enabled)
        {
            GetObject(GetRandomSpawnPosition());

            yield return wait;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector2 position = _map.GetRandomPosition();

        if (Physics2D.Raycast(position, Vector3.up, 1) == true)
            GetRandomSpawnPosition();

        return position;
    }
}
