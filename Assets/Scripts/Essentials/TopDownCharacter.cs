using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;



public class TopDownCharacter : MonoBehaviour
{
    //configurable
    public float speed = 5.0f;
    public float movementDamping = 0.8f; //closer to 0 -- responsive, closer to 1 -- smoother

    public Vector2 moveVector;
    private Rigidbody2D rb;


    public virtual void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Update()
    {
        moveVector = Vector2.Lerp(CalcMoveVector(), moveVector, movementDamping);
    }

    public virtual Vector2 CalcMoveVector()
    {
        return Vector2.zero;
    }

    public virtual void FixedUpdate()
    {
        rb.linearVelocity = moveVector;

    }


    

}
