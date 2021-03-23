using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    private Vector3 _camPos = default;
    private Quaternion _camRot = default;

    public void OnStart()
    {
        //カメラの初期情報を設定
        _camPos = GameSerializeData.GameData._CameraTransform.position;
        _camRot = GameSerializeData.GameData._CameraTransform.rotation;
    }

    public void OnUpdate()
    {
        //カメラのプレイヤー追従処理
        CameraMove(GameSerializeData.GameData._PlayerTransform, GameSerializeData.GameData._CameraTransform);
    }

    private void CameraMove(Transform player,Transform camera)
    {
        //プレイヤーの後ろの位置を設定
        _camPos.x = player.position.x+ (-player.forward.x*Data.CameraDistance);
        _camPos.z = player.position.z + (-player.forward.z*Data.CameraDistance);

        //カメラの角度をプレイヤーの向いている方向に設定
        _camRot =Quaternion.AngleAxis(player.eulerAngles.y,Vector3.up)*Quaternion.AngleAxis(Data.CameraTilt,Vector3.right);

        //カメラの情報を設定
        camera.position = Vector3.Lerp(camera.position, _camPos, Time.deltaTime * Data.CameraSpeed);
        camera.rotation = Quaternion.Lerp(camera.rotation, _camRot, Time.deltaTime * Data.CameraSpeed);
    }
}
