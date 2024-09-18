using UnityEngine;

public class Map
{
    private Vector2[,] _positions;
    private readonly float _offsetX;
    private readonly float _offsetY;

    public Map(int size, float shift, float offsetX, float offsetY)
    {
        Size = size;
        Shift = shift;
        _offsetX = offsetX;
        _offsetY = offsetY;

        GenerateMap();
    }

    public int Size { get; private set; }
    public float Shift { get; private set; }

    public Vector2[,] Positions => _positions;

    public Vector2 GetRandomPosition() => _positions[Random.Range(0, Size), Random.Range(0, Size)];

    public (int, int) GetPositionInMap(Vector2 position)
    {
        for (int i = 0; i < _positions.GetLength(0); i++)
            for (int j = 0; j < _positions.GetLength(1); j++)
                if (_positions[i, j] == position)
                    return (i, j);

        return (-1, -1);
    }

    private void GenerateMap()
    {
        float positionsX = _offsetX;
        float positionsY = _offsetY;
        float Xreset = positionsX;

        _positions = new Vector2[Size, Size];

        for (int y = 0; y < Size; y++)
        {
            positionsY -= Shift;

            for (int x = 0; x < Size; x++)
            {
                positionsX += Shift;
                _positions[x, y] = new Vector2(positionsX, positionsY);
            }

            positionsX = Xreset;
        }
    }
}
