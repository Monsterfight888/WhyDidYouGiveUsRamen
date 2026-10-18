using System.Threading;
using UnityEngine;

public class GoldenGoose : Goose
{
    public GameObject goosePref;

    public float fastTimer;
    public float fastTimeAmount = 1f;

    public Transform arrow;

    public override void Start()
    {
        base.Start();
        timeBetweenDrop = 5000.0f;
    }
    public override void Catch()
    {
        speed = 5;
        fastTimer = fastTimeAmount;
        Instantiate<GameObject>(dropPref, transform.position, Quaternion.identity);
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

        if (Vector2.Distance(transform.position, TopDownPlayer.player.transform.position) >= 8.0f)
        {
            Vector2 dirVec = transform.position - arrow.position;

            float angle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            arrow.rotation = targetRotation;

            arrow.gameObject.SetActive(true);
        }
        else
        {
            arrow.gameObject.SetActive(false);

        }
        
        
    }

    public override Vector2 CalcMoveVector()
    {
        return Vector2.Normalize(transform.position - TopDownPlayer.player.transform.position) * speed;
    }
}
