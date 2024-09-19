using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private CollisionDetector _detector;

    public string Score => _text.text;

    private void OnEnable()
    {
        _detector.CoinCollected += OnCoinCollected;
    }

    private void OnDisable()
    {
        _detector.CoinCollected -= OnCoinCollected;
    }

    public void Clear() => _text.text = "0";

    private void OnCoinCollected(Coin coin)
    {
        _text.text = $"{int.Parse(_text.text) + coin.Cost}";
    }
}
