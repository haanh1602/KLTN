using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IBaseEnemy
{
    public int poolIndex;
    private float speed = 1f;
    private const float _distanceMoveToPlayer = 5f;
    private bool _isRandomMoveMode = false;
    private float _timeMoveRandomLeftRight = 1f;
    private Coroutine _coroutineMoveRandomLeftRight;
    private QuestionData question;

    [SerializeField] FxPool fxDie;

    void Start()
    {

    }
   
    public virtual void Move()
    {
        //Face
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
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    public void RandomMove()
    {
        if (_coroutineMoveRandomLeftRight != null) StopCoroutine(_coroutineMoveRandomLeftRight);
        _coroutineMoveRandomLeftRight = StartCoroutine(MoveRandomLeftRight());
    }

    private IEnumerator MoveRandomLeftRight()
    {
        float timer = 0;
        int randomAction = UnityEngine.Random.Range(0, 2);
        Vector3 nextPos;

        while (gameObject.activeInHierarchy)
        {
            if (timer >= _timeMoveRandomLeftRight)
            {
                randomAction = UnityEngine.Random.Range(0, 2);
                timer = 0;
            }
            else
            {
                switch (randomAction)
                {
                    case 0:
                        nextPos = Vector3.left * speed * Time.deltaTime;
                        transform.position += nextPos;
                        break;
                    case 1:
                        nextPos = Vector3.right * speed * Time.deltaTime;
                        transform.position += nextPos;
                        break;
                }
            }

            yield return null;
            timer += Time.deltaTime;
        }
    }
    public void Update()
    {
        if (FollowToPlayer())
        {
            _isRandomMoveMode = false;
            if (_coroutineMoveRandomLeftRight != null) StopCoroutine(_coroutineMoveRandomLeftRight);
            Move();
        }
        else
        {
            if(_isRandomMoveMode == false)
            {
                _isRandomMoveMode = true;
                RandomMove();
            }
        }
    }

    public bool FollowToPlayer()
    {
        float distance = Vector3.Distance(transform.position, BasePlayer._ins.transform.position);
        return distance < _distanceMoveToPlayer;
    }

    public virtual void Die()
    {
        if (fxDie != null)
        {
            FxPool fx = PoolFxManager._ins.SpawnFx(fxDie.gameObject);
            fx.transform.position = transform.position;
            fx.gameObject.SetActive(true);
        }
        this.gameObject.SetActive(false);
        EnemyManager._ins.listAliveEnemy.Remove(this);
        EnemyManager._ins.AddToPoolEnemy(this);
    }
     
    public virtual void ResetPool()
    {

    }
}
