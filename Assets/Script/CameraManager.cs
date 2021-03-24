using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    //カメラの情報
    private Vector3 _camPos = default;
    private Quaternion _camRot = default;

    private bool _isTarget = false;

    public void OnStart()
    {
        //カメラの初期情報を設定
        _camPos = GameSerializeData.GameData._CameraTransform.position;
        _camRot = GameSerializeData.GameData._CameraTransform.rotation;
    }

    public void OnUpdate()
    {
        Target();
        Reset();
        Gradually();

        //カメラの挙動
        CameraMove();
    }

    private void CameraMove()
    {
        //プレイヤーの後ろの位置を設定
        _camPos.x = GameSerializeData.GameData._PlayerTransform.position.x + (-GameSerializeData.GameData._CameraTransform.forward.x * Data.CameraDistance);
        _camPos.z = GameSerializeData.GameData._PlayerTransform.position.z + (-GameSerializeData.GameData._CameraTransform.forward.z * Data.CameraDistance);

        //カメラの情報を設定
        GameSerializeData.GameData._CameraTransform.position = Vector3.Lerp(GameSerializeData.GameData._CameraTransform.position, _camPos, Time.deltaTime * Data.CameraFollowSpeed);
        GameSerializeData.GameData._CameraTransform.rotation = Quaternion.Lerp(GameSerializeData.GameData._CameraTransform.rotation, _camRot, Time.deltaTime * Data.CameraFollowSpeed);
    }

    private void Reset()
    {
        if (Input.GetKeyDown(Data.CameraReset))
        {
            //カメラの角度をプレイヤーの向いている方向に設定
            _camRot = Quaternion.AngleAxis(GameSerializeData.GameData._PlayerTransform.eulerAngles.y, Vector3.up) * Quaternion.AngleAxis(Data.CameraTilt, Vector3.right);
            _isTarget = false;
        }
    }

    private void Gradually()
    {
        if (Input.GetKey(Data.CameraRight))
        {
            _camRot = Quaternion.AngleAxis(GameSerializeData.GameData._CameraTransform.eulerAngles.y - Data.CameraSpeed, Vector3.up) * Quaternion.AngleAxis(Data.CameraTilt, Vector3.right);
            _isTarget = false;
        }
        if (Input.GetKey(Data.CameraLeft))
        {
            _camRot = Quaternion.AngleAxis(GameSerializeData.GameData._CameraTransform.eulerAngles.y + Data.CameraSpeed, Vector3.up) * Quaternion.AngleAxis(Data.CameraTilt, Vector3.right);
            _isTarget = false;
        }
    }

    private void Target()
    {
        if (Input.GetKeyDown(Data.CameraTarget))
        {
            _isTarget = !_isTarget;
        }

        if (!_isTarget) return;
        _camRot = Quaternion.LookRotation(GameSerializeData.GameData._AITransform.position, Vector3.up) * Quaternion.AngleAxis(Data.CameraTilt, Vector3.right);
    }
}
