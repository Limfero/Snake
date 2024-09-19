using UnityEngine;
using UnityEngine.UI;

public abstract class GamePanel : MonoBehaviour
{
    [SerializeField] private Button _button;

    public virtual void Enable() => gameObject.SetActive(true);

    public void Disable() => gameObject.SetActive(false);

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    protected abstract void OnButtonClick();
}
