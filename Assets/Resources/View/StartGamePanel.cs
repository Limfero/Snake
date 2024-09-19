using System;

public class StartGamePanel : GamePanel
{
    public event Action Started;

    protected override void OnButtonClick() => Started?.Invoke();
}
