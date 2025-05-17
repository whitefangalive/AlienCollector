using UnityEngine;

public static class Elastic
{
    /// <summary>
    /// Ease-Out Elastic function (t from 0→1) → output also 0→1.
    /// </summary>
    public static float EaseOutElastic(float t)
    {
        t = Mathf.Clamp01(t);
        if (t <= 0f) return 0f;
        if (t >= 1f) return 1f;

        const float p = 0.2f;
        float s = p / 4f;
        return Mathf.Pow(2f, -8f * t) * Mathf.Sin((t - s) * (2f * Mathf.PI) / p) + 1f;
    }

    /// <summary>
    /// Just like Mathf.Lerp(a, b, t) but with an elastic (bounce-y) easing on t.
    /// </summary>
    public static float Lerp(float a, float b, float t)
    {
        return Mathf.Lerp(a, b, EaseOutElastic(t));
    }

    /// <summary>
    /// Elastic Lerp for Vector3 (component-wise).
    /// </summary>
    public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
    {
        float e = EaseOutElastic(t);
        return Vector3.Lerp(a, b, e);
    }
}
