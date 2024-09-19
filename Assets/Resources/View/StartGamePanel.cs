using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartGamePanel : GamePanel
{
    protected override void OnButtonClick() => SceneManager.LoadScene(1);
}
