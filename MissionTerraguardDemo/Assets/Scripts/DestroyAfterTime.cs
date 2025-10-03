using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float lifeTime = 10f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AddScore(10); // +10 points per rescue
            Destroy(gameObject); 
        }
    }
}
