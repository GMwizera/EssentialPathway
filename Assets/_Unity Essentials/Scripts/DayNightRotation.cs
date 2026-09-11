using UnityEngine;

[RequireComponent(typeof(Light))]
public class DayNightRotation : MonoBehaviour
{
    [Tooltip("Real-world seconds for one full day cycle.")]
    [Min(0.01f)]
    public float secondsPerDay = 120f;

    [Tooltip("Time of day at start. 0 = midnight, 0.25 = sunrise, 0.5 = noon, 0.75 = sunset.")]
    [Range(0f, 1f)]
    public float startTimeOfDay = 0.25f;

    [Tooltip("Rotates the whole arc around the vertical axis, so you can pick which way the sun travels.")]
    [Range(0f, 360f)]
    public float compassDirection = 0f;

    [Tooltip("Uncheck to freeze the sun in place.")]
    public bool running = true;

    // Current position in the cycle, 0 to 1.
    public float TimeOfDay { get; private set; }

    void Start()
    {
        TimeOfDay = startTimeOfDay;
        ApplyRotation();
    }

    void Update()
    {
        if (!running) return;

        TimeOfDay += Time.deltaTime / secondsPerDay;
        TimeOfDay %= 1f;
        ApplyRotation();
    }

    void ApplyRotation()
    {
        // At 0.25 the light sits on the horizon, at 0.5 it points straight down.
        float sunAngle = TimeOfDay * 360f - 90f;
        transform.rotation = Quaternion.Euler(sunAngle, compassDirection, 0f);
    }
}