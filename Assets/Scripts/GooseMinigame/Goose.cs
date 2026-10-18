using UnityEngine;

public class Goose : TopDownCharacter
{
    public override Vector2 CalcMoveVector()
    {
        return Vector2.Normalize(transform.position - TopDownPlayer.player.transform.position) * speed;
    }

    public void Catch()
    {

    }
}
