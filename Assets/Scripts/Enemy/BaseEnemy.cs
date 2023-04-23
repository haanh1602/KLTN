using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class BaseEnemy : MonoBehaviour, IBaseEnemy
{
    public int poolIndex;
    private float speed = 1f;
    private const float _distanceMoveToPlayer = 5f;
    private bool _isRandomMoveMode = false;
    private float _timeMoveRandomLeftRight = 1f;
    private Coroutine _coroutineMoveRandomLeftRight;
    public QuestionData question;

    [SerializeField] private LevelCircle levelCircle;

    [SerializeField] FxPool fxDie;

    void Start()
    {
        RandomQuestion();
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
        if (EnemyManager._ins.IsPause)
        {
            StopCoroutine(_coroutineMoveRandomLeftRight);
            return;
        } 
        
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

    public IEnumerator DieFx()
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        for(float i = 0f; i <= 1f; i += Time.deltaTime)
        {
            renderer.material.SetFloat("_DissolveAmount", i);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        this.gameObject.SetActive(false);
        renderer.material.SetFloat("_DissolveAmount", 0f);
        EnemyManager._ins.listAliveEnemy.Remove(this);
        EnemyManager._ins.AddToPoolEnemy(this);
    }
    public virtual void Die()
    {
        StartCoroutine(DieFx());
    }
    public void RandomQuestion()
    {
        int id = GameManager.Instance.ID;
        Debug.LogError("ID - " + id);
        question = GameData.Instance.questionsData[id];
        GameManager.Instance.ID++;
        question.answer = question.answer.OrderBy(x => Random.value).ToList();
        if (levelCircle) levelCircle.Init(GetQuestionLevel.FromDataString(question.qLevel));
    }
    public virtual void ResetPool()
    {
        gameObject.SetActive(true);
        RandomQuestion();
    }

    public void StopMove()
    {
        StopAllCoroutines();
        _isRandomMoveMode = false;
    }
}
