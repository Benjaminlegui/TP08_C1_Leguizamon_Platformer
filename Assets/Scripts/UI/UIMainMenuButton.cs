using UnityEngine;

public class UIMainMenuButton : UIMenuButton
{
    protected override void OnClick() => SceneLoader.LoadMainMenu();
}
