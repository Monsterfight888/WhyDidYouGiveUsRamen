using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    public float fallSpeed = 5f;
    public float destroyYBoundary = -6f;

    private void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        if (transform.position.y < destroyYBoundary)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BowlMovement bowl))
        {
            WaterCatchGameManager manager = FindFirstObjectByType<WaterCatchGameManager>();
            if (manager != null)
            {
                manager.AddScore();
            }
            
            Destroy(gameObject);
        }
    }
}
