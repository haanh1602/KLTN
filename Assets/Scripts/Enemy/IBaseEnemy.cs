using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseEnemy 
{
    void Move();
    void Die(bool isRightAnswer);
    void ResetPool();
}
