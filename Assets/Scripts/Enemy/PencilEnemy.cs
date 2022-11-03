using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilEnemy : BaseEnemy
{
    [SerializeField] private GameObject archery;
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
        archery.SetActive(true);
        anim.SetBool("isAttack", true);
        StartCoroutine(CoroutineShoot());

    }

    private IEnumerator CoroutineShoot()
    {
        yield return new WaitForSeconds(0.5f);
        shotCtrl.StartShotRoutine();
        archery.SetActive(false);
        anim.SetBool("isAttack", false);
    }
}
