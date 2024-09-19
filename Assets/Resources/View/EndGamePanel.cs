using System;
using TMPro;
using UnityEngine;

public class EndGamePanel : GamePanel
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private ScoreView _scoreView;

    public event Action Restarted;

    public override void Enable()
    {
        _score.text = _scoreView.Score;
        base.Enable();
    }

    protected override void OnButtonClick() => Restarted?.Invoke();
}
