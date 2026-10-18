using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;



public class TopDownPlayer : TopDownCharacter
{

    public float interactionRadius = 1.0f;

    public LayerMask interactionLayerMask;


    //input
    private InputActionMap inpAsset;
    private InputAction mvAction;
    private InputAction interactAction;

    private Interactable hoveringInteractable;

    public static TopDownPlayer player;

    public void Awake()
    {
        if(player == null)
        {
            player = this;
        }
        else
        {
            Debug.LogError("TWO PLAYERS EXIST");
        }
    }

    public override void Start() 
    {
        base.Start();

        inpAsset = GetComponent<PlayerInput>().currentActionMap;
        mvAction = inpAsset.FindAction("Movement");
        interactAction = inpAsset.FindAction("Interact");
    }
    public override Vector2 CalcMoveVector()
    {
        return mvAction.ReadValue<Vector2>() * speed;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

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
                if(hoveringInteractable != null)
                {
                    hoveringInteractable.OnHover(false);
                }
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
