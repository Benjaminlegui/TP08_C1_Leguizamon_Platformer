using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button retryButton;

    private void OnEnable()
    {
        gameManager.Changed += Refresh;
        retryButton.onClick.AddListener(gameManager.RestartGame);
        Refresh();
    }

    private void OnDisable()
    {
        gameManager.Changed -= Refresh;
        retryButton.onClick.RemoveListener(gameManager.RestartGame);
    }

    private void Refresh()
    {
        gameOverPanel.SetActive(gameManager.State == GameManager.GameState.GameOver);
    }
}
