using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSerializeData : MonoBehaviour
{
    //外部参照用（Serializeが外れてしまうので、シングルトンにはしない）
    public static GameSerializeData GameData { get; private set; } = default;
    public void SetSerializeData(GameSerializeData data)
    {
        GameData = data;
    }

    [Header("プレイヤー")]
    public Transform _PlayerTransform = default;
    public Rigidbody _PlayerRigidbody = default;
    public Animator _PlayerAnimator = default;
    [Header("プレイヤーの武器Collider")]
    public Collider _PlayerCollider = default;
    [Header("プレイヤーのHPバー")]
    public Transform _PlayerHPBar = default;

    [Header("AI"), Space(30)]
    public Transform _AITransform = default;
    public Rigidbody _AIRigidbody = default;
    public Animator _AIAnimator = default;
    [Header("AIの武器Collider")]
    public Collider _AICollider = default;
    [Header("AIのHPバー")]
    public Transform _AIHPBar = default;

    [Header("カメラ")]
    public Transform _CameraTransform = default;

}
