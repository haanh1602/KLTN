using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkBumerang : BasePerk
{
    [SerializeField] List<GameObject> listObjSkill;
    public override void Attack()
    {
        if (level <= 0)
        {
            return;
        }
        
        for(int i = 0; i < listObjSkill.Count; i++)
        {
            listObjSkill[i].SetActive(false);
        }

        listObjSkill[level - 1].SetActive(true);
    }
}
