using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : BaseEnemy
{
    public UbhShotCtrl shotCtrl;

    void Update()
    {
        base.Update();
    }


    public override void Attack()
    {
        base.Attack();
        shotCtrl.StartShotRoutine();
    }
}
