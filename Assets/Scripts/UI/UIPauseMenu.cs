using UnityEngine;
using UnityEngine.InputSystem;

public class UIPauseMenu : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIPanel pausePanel;
    [SerializeField] private UIPanel[] subPanels;
    [SerializeField] private InputActionReference pauseAction;

    private void OnEnable()
    {
        pauseAction.action.performed += OnPause;
        pauseAction.action.Enable();
    }

    private void OnDisable()
    {
        pauseAction.action.performed -= OnPause;
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (!gameManager.IsPlaying)
            return;

        foreach (UIPanel panel in subPanels)
        {
            if (panel.IsOpen)
            {
                panel.Close();
                return;
            }
        }

        if (pausePanel.IsOpen)
            pausePanel.Close();
        else
            pausePanel.Open();
    }
}
