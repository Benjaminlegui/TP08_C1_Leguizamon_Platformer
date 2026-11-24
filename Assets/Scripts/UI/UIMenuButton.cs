using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class UIMenuButton : MonoBehaviour, IPointerEnterHandler
{
    private Button button;
    private AudioManager audioManager;

    protected virtual AudioManager.UISound ClickSound => AudioManager.UISound.Click;

    // Warning: FindAnyObjectByType se ejecuta una vez por cada boton de la escena (y lo mismo hace UIPanel.Awake).
    // Inyecta el AudioManager por inspector o exponelo con una referencia unica en vez de buscarlo en runtime.
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
