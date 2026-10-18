using System.Threading;
using UnityEngine;

public class Goose : TopDownCharacter
{
    public Vector2 moveDir;
    public GameObject dropPref;
    protected float timer = 0;
    public float timeBetweenDrop = .5f;
    public float baseSpeed;
    public SpriteRenderer sr;
    public virtual void Awake()
    {
        Destroy(gameObject, 10.0f);
    }
    public override void Start()
    {
        base.Start();
        baseSpeed = speed;
        timer = timeBetweenDrop;
    }
    public override Vector2 CalcMoveVector()
    {
        if(moveDir.x >= 0)
        {
            sr.flipX = true;
        }
        

        return moveDir * speed;
    }
    public override void Update()
    {
        base.Update();
        if(timer < 0)
        {
            timer = timeBetweenDrop;

            Instantiate<GameObject>(dropPref, transform.position, Quaternion.identity);
        }
        timer -= Time.deltaTime;
    }

    public virtual void Catch()
    {

    }
}
