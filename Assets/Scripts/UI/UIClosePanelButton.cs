using UnityEngine;

public class UIClosePanelButton : UIMenuButton
{
    [SerializeField] private UIPanel panel;

    protected override void OnClick() => panel.Close();
}
