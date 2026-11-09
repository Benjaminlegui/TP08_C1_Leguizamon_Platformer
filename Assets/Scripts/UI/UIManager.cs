using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private RunTimer runTimer;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text bestTimeText;

    private void OnEnable()
    {
        gameManager.Changed += Refresh;
        runTimer.Changed += Refresh;
        Refresh();
    }

    private void OnDisable()
    {
        gameManager.Changed -= Refresh;
        runTimer.Changed -= Refresh;
    }

    private void Refresh()
    {
        endPanel.SetActive(!gameManager.IsPlaying);

        if (gameManager.IsPlaying)
            return;

        titleText.text = gameManager.State == GameManager.GameState.Won ? "LEVEL COMPLETE" : "GAME OVER";
        timeText.text = $"Time: {RunTimer.FormatPrecise(runTimer.ElapsedTime)}";

        if (runTimer.IsNewBestTime)
            bestTimeText.text = $"New best time: {RunTimer.FormatPrecise(runTimer.BestTime)}";
        else if (runTimer.HasBestTime)
            bestTimeText.text = $"Best time: {RunTimer.FormatPrecise(runTimer.BestTime)}";
        else
            bestTimeText.text = "Best time: --:--";
    }
}
