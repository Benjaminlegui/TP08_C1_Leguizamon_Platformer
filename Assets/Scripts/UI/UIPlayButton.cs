using UnityEngine;

public class UIPlayButton : UIMenuButton
{
    protected override void OnClick() => SceneLoader.LoadGameplay();
}
