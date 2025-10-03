using UnityEngine;

public class Pulse : MonoBehaviour
{
    public float amplitude = 0.03f;
    public float speed = 2f;

    Vector3 originalScale;
    float t;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        t += Time.deltaTime * speed;
        float scaleFactor = 1f + Mathf.Sin(t) * amplitude;
        transform.localScale = originalScale * scaleFactor;
    }
}