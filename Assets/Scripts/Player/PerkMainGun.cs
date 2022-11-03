using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkMainGun : BasePerk
{
    [SerializeField] List<UbhShotCtrl> ubhShotCtrls;
   

    public override void UpgradePerk()
    {
        BasePlayer._ins.playerAttack.shotCtrl.gameObject.SetActive(false);
        base.UpgradePerk();
        ubhShotCtrls[level - 1].gameObject.SetActive(false);
        BasePlayer._ins.playerAttack.shotCtrl = ubhShotCtrls[level];
        ubhShotCtrls[level].gameObject.SetActive(true);

    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UpgradePerk();
        }
    }
}
