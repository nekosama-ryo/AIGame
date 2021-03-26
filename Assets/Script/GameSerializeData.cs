﻿using System.Collections;
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
    public Collider _PlayerCollider = default;
    public Transform _PlayerHPBar = default;

    [Header("AI")]
    public Transform _AITransform = default;
    public Rigidbody _AIRigidbody = default;
    public Animator _AIAnimator = default;
    public Collider _AICollider = default;
    public Transform _AIHPBar = default;

    [Header("カメラ")]
    public Transform _CameraTransform = default;

}
