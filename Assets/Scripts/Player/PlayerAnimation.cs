using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Direction _direction;
    private Vector2 _v2Direction;
    [SerializeField] public Animator anim;
    [SerializeField] private Direction defaultDirection;

    [SerializeField] private Transform spritesContainer;
    
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
        _v2Direction = UIGameManager._ins.joystick.Direction;
        Direction currentDirection = GetDirection.FromVector2(_v2Direction);
        anim.SetFloat("Speed", _v2Direction.magnitude);
        if (_direction != currentDirection)
        {
            SetAnim(currentDirection);
        }
    }

    private void SetAnim(Direction direction)
    {
        Quaternion rotation = spritesContainer.rotation;

        switch (direction)
        {
            case Direction.Left:
                rotation.y = 0f;
                spritesContainer.rotation = rotation;
                break;
            case Direction.Right:
                rotation.y = 180f;
                spritesContainer.rotation = rotation;
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
        
        return v2Direction.x >= 0f ? Direction.Right : Direction.Left;
    }
}

