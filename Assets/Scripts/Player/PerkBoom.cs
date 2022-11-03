using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkBoom : BasePerk
{

    [SerializeField] List<UbhShotCtrl> listShotCtrl;

    public override void Attack()
    { 
        if (level <= 0)
        {
            return;
        }
        listShotCtrl[level - 1].StartShotRoutine();

    }
}
