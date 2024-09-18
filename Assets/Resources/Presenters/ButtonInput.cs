using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInput : MonoBehaviour
{
    [SerializeField] private Button _up;
    [SerializeField] private Button _down;
    [SerializeField] private Button _left;
    [SerializeField] private Button _rigth;

    public Action<float, float> DirectionEntered;

    private void OnEnable()
    {
        _up.onClick.AddListener(OnUpClick);
        _down.onClick.AddListener(OnDownClick);
        _left.onClick.AddListener(OnLeftClick);
        _rigth.onClick.AddListener(OnRigthClick);
    }

    private void OnDisable()
    {
        _up.onClick.RemoveListener(OnUpClick);
        _down.onClick.RemoveListener(OnDownClick);
        _left.onClick.RemoveListener(OnLeftClick);
        _rigth.onClick.RemoveListener(OnRigthClick);
    }

    private void OnUpClick() => DirectionEntered?.Invoke(1, 0);

    private void OnDownClick() => DirectionEntered?.Invoke(-1, 0);

    private void OnLeftClick() => DirectionEntered?.Invoke(0, 1);

    private void OnRigthClick() => DirectionEntered?.Invoke(0, -1);

}
