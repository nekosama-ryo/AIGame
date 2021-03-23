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

    [Header("カメラ")]
    public Transform _CameraTransform = default;

}
