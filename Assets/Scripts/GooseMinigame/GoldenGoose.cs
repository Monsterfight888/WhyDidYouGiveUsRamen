using System.Threading;
using UnityEngine;

public class GoldenGoose : Goose
{
    public GameObject goosePref;

    public float fastTimer;
    public float fastTimeAmount = 1f;

    public override void Start()
    {
        base.Start();
        timeBetweenDrop = 5f;
    }
    public override void Catch()
    {
        speed = 5;
        fastTimer = fastTimeAmount;
        for (int i = 0; i < 5; i++)
        {
            Goose spawnedGoose = Instantiate<GameObject>(goosePref, transform.position, Quaternion.identity).GetComponent<Goose>();
            spawnedGoose.moveDir = Random.insideUnitCircle.normalized;
        }
    }

    public override void Update()
    {
        base.Update();

        fastTimer -= Time.deltaTime;
        if(fastTimer < 0 )
        {
            speed = baseSpeed;
        }

        if(timer < 0)
        {
            speed = 5;
            fastTimer = fastTimeAmount;
        }
    }

    public override Vector2 CalcMoveVector()
    {
        return Vector2.Normalize(transform.position - TopDownPlayer.player.transform.position) * speed;
    }
}
