using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : Component
{
    [SerializeField] protected T Prefab;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private ObjectPool<T> _pool;

    public int GetCountActiveObject => _pool.CountActive;
    public int GetCountAllObject => _pool.CountAll;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
        createFunc: () => Spawn(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    public void GetObject(Vector3 positon)
    {
        T obj = _pool.Get();
        obj.transform.position = positon;
    }

    public void Relese(T obj) => _pool.Release(obj);

    protected abstract T Spawn();

    private void ActionOnGet(T obj)
    {
        obj.gameObject.SetActive(true);
    }
}
