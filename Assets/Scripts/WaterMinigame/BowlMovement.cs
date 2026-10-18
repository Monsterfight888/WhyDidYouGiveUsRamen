using UnityEngine;
using UnityEngine.InputSystem;

public class BowlMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float screenLimitX = 8f;

    private void Update()
    {
        float moveInput = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            {
                moveInput = -1f;
            }
            else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            {
                moveInput = 1f;
            }
        }

        Vector3 newPosition = transform.position + new Vector3(moveInput * moveSpeed * Time.deltaTime, 0, 0);

        newPosition.x = Mathf.Clamp(newPosition.x, -screenLimitX, screenLimitX);
        transform.position = newPosition;
    }
}
