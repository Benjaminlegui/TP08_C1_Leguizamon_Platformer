using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class UISplashScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup content;
    [Min(0f)]
    [SerializeField] private float fadeDuration = 0.8f;
    [Min(0f)]
    [SerializeField] private float holdDuration = 1.5f;

    private IDisposable anyButtonListener;
    private bool skipRequested;

    private void OnEnable()
    {
        anyButtonListener = InputSystem.onAnyButtonPress.CallOnce(_ => skipRequested = true);
    }

    private void OnDisable()
    {
        anyButtonListener?.Dispose();
    }

    private IEnumerator Start()
    {
        content.alpha = 0f;

        yield return Fade(0f, 1f);

        float elapsed = 0f;
        while (elapsed < holdDuration && !skipRequested)
        {
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        yield return Fade(content.alpha, 0f);

        SceneLoader.LoadMainMenu();
    }

    private IEnumerator Fade(float from, float to)
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            if (skipRequested && to > from)
                yield break;

            elapsed += Time.unscaledDeltaTime;
            content.alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            yield return null;
        }

        content.alpha = to;
    }
}
