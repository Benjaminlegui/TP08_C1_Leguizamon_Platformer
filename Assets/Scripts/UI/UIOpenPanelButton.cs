using UnityEngine;

public class UIOpenPanelButton : UIMenuButton
{
    [SerializeField] private UIPanel panel;

    protected override void OnClick() => panel.Open();
}
