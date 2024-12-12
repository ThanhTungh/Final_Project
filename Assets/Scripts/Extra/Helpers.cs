using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    public static IEnumerator IEFade(CanvasGroup canvas, float desiredValue, float fadeTime)
    {
        float timer = 0f;
        float initialValue = canvas.alpha;
        while (timer < fadeTime)
        {
            canvas.alpha = Mathf.Lerp(initialValue, desiredValue, timer / fadeTime);
            timer += Time.deltaTime;
            yield return null;
        }

        canvas.alpha = desiredValue;
    }
}
