using UnityEngine;

public class UIPanel : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private bool pausesGame = true;

    private bool isOpen;
    private float previousTimeScale = 1f;

    public bool IsOpen => isOpen;

    private void Awake() => content.SetActive(false);

    public void Open()
    {
        if (isOpen) return;
        isOpen = true;
        if (pausesGame)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }
        content.SetActive(true);
    }

    public void Close()
    {
        if (!isOpen) return;
        isOpen = false;
        content.SetActive(false);
        if (pausesGame) Time.timeScale = previousTimeScale;
    }

    private void OnDisable() => Close();
}
