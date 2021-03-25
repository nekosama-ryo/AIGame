using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    //カメラの情報
    private Vector3 _camPos = default;
    private Quaternion _camRot = default;

    //ターゲットカメラの状態

    public void OnStart()
    {
        //カメラの初期情報を設定
        _camPos = GameSerializeData.GameData._CameraTransform.position;
        _camRot = GameSerializeData.GameData._CameraTransform.rotation;
    }

    public void OnUpdate()
    {
        Target(Input.GetKeyDown(Data.CameraTarget));
        Reset(Input.GetKeyDown(Data.CameraReset));
        Gradually(Input.GetKey(Data.CameraRight), Input.GetKey(Data.CameraLeft));

        //カメラの挙動
        CameraMove();
    }

    /// <summary>カメラの挙動 </summary>
    private void CameraMove()
    {
        //プレイヤーの後ろの位置を設定
        _camPos.x = GameSerializeData.GameData._PlayerTransform.position.x + (-GameSerializeData.GameData._CameraTransform.forward.x * Data.CameraDistance);
        _camPos.z = GameSerializeData.GameData._PlayerTransform.position.z + (-GameSerializeData.GameData._CameraTransform.forward.z * Data.CameraDistance);

        //カメラの情報を設定
        GameSerializeData.GameData._CameraTransform.position = Vector3.Lerp(GameSerializeData.GameData._CameraTransform.position, _camPos, Time.deltaTime * Data.CameraFollowSpeed);
        GameSerializeData.GameData._CameraTransform.rotation = Quaternion.Lerp(GameSerializeData.GameData._CameraTransform.rotation, _camRot, Time.deltaTime * Data.CameraFollowSpeed);
    }

    /// <summary>カメラの角度をプレイヤーの向いている方向に変更 </summary>
    private void Reset(bool key)
    {
        if (key)
        {
            //カメラの角度をプレイヤーの向いている方向に設定
            _camRot = Quaternion.AngleAxis(GameSerializeData.GameData._PlayerTransform.eulerAngles.y, Vector3.up) * Quaternion.AngleAxis(Data.CameraTilt, Vector3.right);
            TargetReset();
        }
    }

    /// <summary>カメラを左右に動かす</summary>
    private void Gradually(bool keyR,bool keyL)
    {
        //右回転
        if (keyR)
        {
            _camRot = Quaternion.AngleAxis(GameSerializeData.GameData._CameraTransform.eulerAngles.y - Data.CameraSpeed, Vector3.up) * Quaternion.AngleAxis(Data.CameraTilt, Vector3.right);
            TargetReset();
        }
        //左回転
        if (keyL)
        {
            _camRot = Quaternion.AngleAxis(GameSerializeData.GameData._CameraTransform.eulerAngles.y + Data.CameraSpeed, Vector3.up) * Quaternion.AngleAxis(Data.CameraTilt, Vector3.right);
            TargetReset();
        }
    }

    /// <summary>カメラの方向をターゲットの方向に合わせる</summary>
    private void Target(bool key)
    {
        if (key)
        {
            //オンオフの切り替え
            Data.IsTaeget = !Data.IsTaeget;
            //高さをターゲットカメラかどうかで変更する
            _camPos.y = Data.IsTaeget ? Data.CameraTargetHeight : Data.CameraHeight;

            //ターゲット解除時に角度を元に戻す
            if(!Data.IsTaeget)
            {
                _camRot = GameSerializeData.GameData._CameraTransform.rotation * Quaternion.AngleAxis(Data.CameraTargetTilt, Vector3.right);
            }
        }

        //ターゲットの方向を向く
        if (Data.IsTaeget)
        {
            _camRot = Quaternion.LookRotation(GameSerializeData.GameData._AITransform.position) * Quaternion.AngleAxis(Data.CameraTargetTilt, Vector3.right);
        }
    }
    /// <summary>ターゲット状態を解除する</summary>
    private void TargetReset()
    {
        //高さを変更する
        _camPos.y =  Data.CameraHeight;
        //ターゲットをオフにする
        Data.IsTaeget = false;
    }
}
