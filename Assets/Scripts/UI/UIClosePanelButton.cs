using UnityEngine;

public class UIClosePanelButton : UIMenuButton
{
    [SerializeField] private UIPanel panel;

    protected override AudioManager.UISound ClickSound => AudioManager.UISound.Back;

    protected override void OnClick() => panel.Close();
}
