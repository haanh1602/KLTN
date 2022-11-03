using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionEnemy : BaseEnemy
{

    // Start is called before the first frame update
    public override void Attack()
    {
        base.Attack();
        //PlayerMovement._ins.speed -= 2f;
    }
}
