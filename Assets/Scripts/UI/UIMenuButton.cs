using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class UIMenuButton : MonoBehaviour, IPointerEnterHandler
{
    private Button button;
    private AudioManager audioManager;

    protected virtual AudioManager.UISound ClickSound => AudioManager.UISound.Click;

    private void Awake() => audioManager = FindAnyObjectByType<AudioManager>();

    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
    }

    private void OnDisable() => button.onClick.RemoveListener(HandleClick);

    public void OnPointerEnter(PointerEventData eventData) => PlaySound(AudioManager.UISound.Hover);

    private void HandleClick()
    {
        PlaySound(ClickSound);
        OnClick();
    }

    private void PlaySound(AudioManager.UISound sound)
    {
        if (audioManager != null && button.interactable) audioManager.PlayUI(sound);
    }

    protected abstract void OnClick();
}
