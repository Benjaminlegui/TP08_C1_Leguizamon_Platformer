using UnityEngine;

public class UIQuitButton : UIMenuButton
{
    #if UNITY_WEBGL && !UNITY_EDITOR
        private void Awake() => gameObject.SetActive(false);
    #endif
    
    protected override void OnClick()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
