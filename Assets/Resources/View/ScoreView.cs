using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private CollisionDetector _detector;

    private void OnEnable()
    {
        _detector.CoinCollected += OnCoinCollected;
    }

    private void OnDisable()
    {
        _detector.CoinCollected -= OnCoinCollected;
    }

    private void OnCoinCollected(Coin coin)
    {
        _text.text = $"{int.Parse(_text.text) + coin.Cost}";
    }
}
