using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBossEnemy : BaseEnemy
{
    public Animator anim;
    public UbhShotCtrl shotCtrl;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        base.Attack();
        anim.SetBool("isAttacking", true);
        StartCoroutine(CoroutineShoot());

    }

    private IEnumerator CoroutineShoot()
    {
        yield return new WaitForSeconds(0.5f);
        shotCtrl.StartShotRoutine();
        anim.SetBool("isAttacking", false);
    }
}
