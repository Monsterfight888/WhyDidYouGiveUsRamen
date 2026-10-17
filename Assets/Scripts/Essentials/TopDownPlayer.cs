using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownPlayer : MonoBehaviour
{
    //configurable
    public float speed = 5.0f;
    public float movementDamping = .8f; //closer to 0 -- responsive, closer to 1 -- smoother
    public float interactionRadius = 1.0f;

    public LayerMask interactionLayerMask;

    private Vector2 moveVector;
    private Rigidbody2D rb;
    //input
    private InputActionMap inpAsset;
    private InputAction mvAction;
    private InputAction interactAction;

    private Interactable hoveringInteractable;

    public void Start() 
    {
        rb = GetComponent<Rigidbody2D>();

        inpAsset = GetComponent<PlayerInput>().currentActionMap;
        mvAction = inpAsset.FindAction("Movement");
        interactAction = inpAsset.FindAction("Interact");
    }

    public void Update()
    {
        Vector2 previousMv = moveVector;
        moveVector = Vector2.Lerp(mvAction.ReadValue<Vector2>() * speed, moveVector, movementDamping);


    }

    public void FixedUpdate()
    {
        rb.linearVelocity = moveVector;

        Collider2D[] interactables = Physics2D.OverlapCircleAll(transform.position, interactionRadius, interactionLayerMask);

        if (interactables.Length > 0)
        {
            int minDistIndex = 0;
            float minDist = 0;
            for(int i = 0; i < interactables.Length; i++)
            {
                float currentDist = Vector2.Distance(transform.position, interactables[i].transform.position);
                if(currentDist < minDist)
                {
                    minDist = currentDist;
                    minDistIndex = i;
                }
            }

            Interactable nextInteractable = interactables[minDistIndex].GetComponent<Interactable>();
            if(nextInteractable != hoveringInteractable)
            {
                hoveringInteractable = nextInteractable;
                hoveringInteractable.OnHover(true);
            }
        }
        else
        {
            if(hoveringInteractable != null)
            {
                hoveringInteractable.OnHover(false);
            }
            hoveringInteractable = null;
        }


    }

    public void OnInteract()
    {
        if(hoveringInteractable != null)
        {
            hoveringInteractable.Interact(this);
        }
        else
        {
            //interacted w/o an interacable
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }

}
