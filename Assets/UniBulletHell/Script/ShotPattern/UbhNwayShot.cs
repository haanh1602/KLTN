using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Ubh nWay shot.
/// </summary>
[AddComponentMenu("UniBulletHell/Shot Pattern/nWay Shot")]
public class UbhNwayShot : UbhBaseShot
{
    [Header("===== NwayShot Settings =====")]
    // "Set a number of shot way."
    [FormerlySerializedAs("_WayNum")]
    public int m_wayNum = 5;
    // "Set a center angle of shot. (0 to 360)"
    [Range(0f, 360f), FormerlySerializedAs("_CenterAngle")]
    public float m_centerAngle = 180f;
    // "Set a angle between bullet and next bullet. (0 to 360)"
    [Range(0f, 360f), FormerlySerializedAs("_BetweenAngle")]
    public float m_betweenAngle = 10f;
    // "Set a delay time between shot and next line shot. (sec)"
    [FormerlySerializedAs("_NextLineDelay")]
    public float m_nextLineDelay = 0.1f;

    public override void Shot()
    {
        StartCoroutine(ShotCoroutine());
    }

    //private IEnumerator ShotCoroutine()
    //{
    //    if (m_bulletNum <= 0 || m_bulletSpeed <= 0f || m_wayNum <= 0)
    //    {
    //        Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed or WayNum is not set.");
    //        yield break;
    //    }
    //    if (m_shooting)
    //    {
    //        yield break;
    //    }
    //    m_shooting = true;

    //    int wayIndex = 0;

    //    for (int i = 0; i < m_bulletNum; i++)
    //    {
    //        if (m_wayNum <= wayIndex)
    //        {
    //            wayIndex = 0;
    //            if (0f < m_nextLineDelay)
    //            {
    //                FiredShot();
    //                yield return UbhUtil.WaitForSeconds(m_nextLineDelay);
    //            }
    //        }

    //        var bullet = GetBullet(transform.position);
    //        if (bullet == null)
    //        {
    //            break;
    //        }

    //        float baseAngle = m_wayNum % 2 == 0 ? m_centerAngle - (m_betweenAngle / 2f) : m_centerAngle;
    //        float angle = UbhUtil.GetShiftedAngle(wayIndex, baseAngle, m_betweenAngle);
    //        ShotBullet(bullet, m_bulletSpeed, angle);
    //        wayIndex++;
    //    }

    //    FiredShot();
    //    FinishedShot();

    //    yield break;
    //}
    
    private IEnumerator ShotCoroutine()
    {
        if (m_bulletNum <= 0 || m_bulletSpeed <= 0f || m_wayNum <= 0)
        {
            Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed or WayNum is not set.");
            yield break;
        }
        if (m_shooting)
        {
            yield break;
        }
        m_shooting = true;

        int wayIndex = 0;
        FiredShot();
        for (int i = 0; i < m_bulletNum; i++)
        {
            var bullet = GetBullet(transform.position);
            if (bullet == null)
                break;
            float baseAngle = m_wayNum % 2 == 0 ? m_centerAngle - (m_betweenAngle / 2f) : m_centerAngle;
            float angle = UbhUtil.GetShiftedAngle(wayIndex, baseAngle, m_betweenAngle);
            ShotBullet(bullet, m_bulletSpeed, angle);
            wayIndex++;
            if(stParentNway)
            StartCoroutine(SetChild(bullet));
        }
        yield return UbhUtil.WaitForSeconds(m_nextLineDelay);
        
        FinishedShot();
        yield break;

    }
    public bool stParentNway = false;
    public float timeUnParent = 0.5f;
    public bool setTrail = false;
    public Transform parentBullet;
    IEnumerator SetChild(UbhBullet bullet)
    {
        Transform parent;
        parent = bullet.transform.parent.transform;
        if (parentBullet == null)
        { 
        }
           // bullet.transform.SetParent(PlaneIngameManager.current.CurrentTransformPlayer);
        else
            bullet.transform.SetParent(parentBullet);
        yield return new WaitForSeconds(timeUnParent);
        bullet.transform.SetParent(parent);
        if (setTrail)
            bullet.GetComponent<TrailRenderer>().enabled = true;
    }
}