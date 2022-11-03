using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] public UbhShotCtrl shotCtrl;
    public float fireRate = .3f;
    public long power = 10;
    public Transform avatar;

    public void Attack()
    {
        
        shotCtrl.StartShotRoutine();

        
    }

    public Vector3 posTarget;
    public void SetTargetEnemy()
    {
        /*float distance = 20f;
        Vector3 pos = transform.position+Vector3.down;
        if(EnemyManager._ins.listAliveEnemy!=null)
            for(int i = 0; i < EnemyManager._ins.listAliveEnemy.Count; i++)
            {
                float distanceCache = Vector2.Distance(EnemyManager._ins.listAliveEnemy[i].transform.position, transform.position);
                if (distanceCache < distance)
                {
                    distance = distanceCache;
                    pos = EnemyManager._ins.listAliveEnemy[i].transform.position;
                }
            }
        if (EnemyManager._ins.listAliveEnemy.Count<=0)
        {
            SpawnEnemy._ins.ForceNextWave();
        }
        float rotate = Vector2.SignedAngle(Vector3.up, pos - transform.position);
        posTarget = pos;*/

        //if (Input.touchCount == 1 && Input.GetTouch(0).phase == touchPhase)
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //    RaycastHit hit;
        //    Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        Debug.Log(hit.transform.name);
        //        if (hit.collider != null)
        //        {

        //            GameObject touchedObject = hit.transform.gameObject;

        //            Debug.Log("Touched " + touchedObject.transform.name);
        //        }
        //    }
        //}
    }

    Coroutine TargetEnemyCor;

    IEnumerator TargetEnemy()
    {
        while(true)
        {
            yield return new WaitForSeconds(fireRate);
            SetTargetEnemy();
            shotCtrl.StartShotRoutine();
            
        }
    }
    private void OnEnable()
    {
        TargetEnemyCor = StartCoroutine(TargetEnemy());
    }

    public void Update()
    {
        Vector3 gunRotate = shotCtrl.transform.rotation.eulerAngles;
        Vector3 rotateTarget = new Vector3(0,0, Vector2.SignedAngle(Vector3.up, posTarget - shotCtrl.transform.position)) ;
        shotCtrl.transform.rotation =Quaternion.Lerp(Quaternion.Euler(gunRotate), Quaternion.Euler(rotateTarget), Time.deltaTime * 6);
        if (posTarget.x>transform.position.x)
        {
            avatar.localScale = new Vector3(-1, avatar.localScale.y, 1);
        }
        if (posTarget.x < transform.position.x)
        {
            avatar.localScale = new Vector3(1, avatar.localScale.y, 1);
        }
    }

    public long GetDPS()
    {
        return (long)(power / fireRate);
    }
}
