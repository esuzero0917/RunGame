using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float moveSpeed; //プレイヤーの移動速度
    [SerializeField] Vector2 rangeMovableMin; //移動可能範囲の最小値
    [SerializeField] Vector2 rangeMovableXax; //移動可能範囲の最大値

    Vector3 moveDirection; //移動方向
    PlayerInput input; //InputSystem;

    void Awake()
    {
        TryGetComponent(out input);
    }

    void OnEnable()
    {
        input.actions["Move"].performed += OnMove;
        input.actions["Move"].canceled += OnMoveStop;
    }

    void OnDisable()
    {
        input.actions["Move"].performed -= OnMove;
        input.actions["Move"].canceled -= OnMoveStop;
    }

    void Update()
    {
        Move(moveDirection);
        ClampPosition();
    }

    //移動キーを押したときの処理
    void OnMove(InputAction.CallbackContext context)
    {
        Vector3 value = context.ReadValue<Vector2>();
        moveDirection = value;
    }

    //移動キーを離したときの処理
    void OnMoveStop(InputAction.CallbackContext context)
    {
        moveDirection = Vector3.zero;
    }

    //移動処理
    void Move(Vector3 direction)
    {
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    //移動範囲の制限
    void ClampPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, rangeMovableMin.x, rangeMovableXax.x);
        pos.y = Mathf.Clamp(pos.y, rangeMovableMin.y, rangeMovableXax.y);
        transform.position = pos;
    }
}
