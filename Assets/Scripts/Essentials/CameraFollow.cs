using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float followDamping = .9f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 newPos = Vector2.Lerp(transform.position, target.position, followDamping);
        transform.position = new Vector3(newPos.x, newPos.y, -10);
    }
}
