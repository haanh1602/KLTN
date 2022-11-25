using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Direction _direction;
    [SerializeField] Animator anim;
    [SerializeField] private Direction defaultDirection;
    private void Awake()
    {
        _direction = defaultDirection;
    }

    private void Start()
    {
        SetAnim(defaultDirection);
    }

    void Update()
    {
        Vector2 v2Direction = UIGameManager._ins.joystick.Direction;
        Direction currentDirection = GetDirection.FromVector2(v2Direction);
        anim.SetFloat("Speed", v2Direction.magnitude);
        if (_direction != currentDirection)
        {
            SetAnim(currentDirection);
        }
    }

    private void SetAnim(Direction direction)
    {
        Quaternion rotation = transform.rotation;

        switch (direction)
        {
            case Direction.Left:
                rotation.y = 0f;
                transform.rotation = rotation;
                break;
            case Direction.Right:
                rotation.y = 180f;
                transform.rotation = rotation;
                break;
        }

        _direction = direction;
    }
}

public enum Direction
{
    None,
    Left,
    Right
}

public class GetDirection
{
    public static Direction FromVector2(Vector2 v2Direction)
    {
        if (v2Direction == Vector2.zero)
        {
            return Direction.None;
        }

        if (Mathf.Abs(v2Direction.x) >= Mathf.Abs(v2Direction.y))
        {
            return v2Direction.x >= 0f ? Direction.Right : Direction.Left;
        }

        return Direction.None;
    }
}

