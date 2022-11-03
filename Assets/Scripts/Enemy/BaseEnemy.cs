using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IBaseEnemy
{
    public int poolIndex;
    public long maxHp;
    public int damage;
    public long currentHp;
    public float movementSpeed;
    public float delayAttack;
    public float distanceAttack;
    protected bool canAttack;
    protected bool isAttacking;

    [SerializeField] FxPool fxDie;
    void Start()
    {
        currentHp = maxHp;
        canAttack = true;
        isAttacking = false;
    }
   
    public virtual void Move()
    {
      
        if (transform.position.x > BasePlayer._ins.transform.position.x)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0f, transform.eulerAngles.z);
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180f, transform.eulerAngles.z);
        }

        Vector3 directionLine = transform.position - BasePlayer._ins.transform.position;
        Vector3 target = BasePlayer._ins.transform.position + directionLine.normalized * 0.3f;
        transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);

    }

    public void Update()
    {
        if(!isAttacking && !CanAttack())
        {
            Move();
        }
        if(!isAttacking && CanAttack())
        {
            Attack();
        }
    }
    public bool CanAttack()
    {
        float distance = Vector3.Distance(transform.position, BasePlayer._ins.transform.position);
        if (canAttack && distance <= distanceAttack) return true;
        return false;
    }

    public virtual void Attack() {
        isAttacking = true;
        canAttack = false;
        StartCoroutine(CoroutineAttack());
        StartCoroutine(CoroutineAttacking());
    }

    private IEnumerator CoroutineAttack()
    {
        yield return new WaitForSeconds(delayAttack);
        canAttack = true;
    }

    private IEnumerator CoroutineAttacking()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    public virtual void Die()
    {
        if (currentHp <= 0)
        {
            if (fxDie!=null)
            {
                FxPool fx= PoolFxManager._ins.SpawnFx(fxDie.gameObject);
                fx.transform.position = transform.position;
                fx.gameObject.SetActive(true);
            }    
            this.gameObject.SetActive(false);
            EnemyManager._ins.listAliveEnemy.Remove(this);
            EnemyManager._ins.AddToPoolEnemy(this);
        }
    }
     
    public virtual void ResetPool()
    {

        currentHp = maxHp;
        isAttacking = false;
        canAttack = true;
    }
}
