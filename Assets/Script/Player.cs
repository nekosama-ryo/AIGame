using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    //キャラクターを動かす。
    private CharacterControl _controlScr = default;

    //移動量
    private Vector3 _force = Vector3.zero;
    //前後の移動量
    private Vector3 _forward = Vector3.zero;
    //前後の移動スピード
    private Vector3 _forwardSpeed = Vector3.zero;
    //カメラの傾き角度情報
    private Quaternion _camRot = Quaternion.identity;
    //左右の移動量
    private Vector3 _right = Vector3.zero;

    private float DamageTime = 0;
    public void OnStart()
    {
        _controlScr = new CharacterControl(GameSerializeData.GameData._PlayerTransform,GameSerializeData.GameData._PlayerRigidbody,
            GameSerializeData.GameData._PlayerCollider,GameSerializeData.GameData._PlayerAnimator);
        //前後のスピードを設定
        _forwardSpeed = new Vector3(0, 0, Data.CharacterSpeed);
    }

    public void OnFixedUpdate()
    {
        KeyMove();
    }

    public void OnUpdate()
    {
        KeyAttack();
        _controlScr.Defense(Input.GetKey(Data.Defense));


        _controlScr.Damage(ref Data.PlayerOnDamage);
    }

    /// <summary>キー入力に応じて移動・回転する </summary>
    private void KeyMove()
    {
        //前後の動き
        if(Input.GetKey(Data.UP)|| Input.GetKey(Data.Down))
        {
            //前移動か後ろ移動かを判定
            int i = Input.GetKey(Data.UP) ? 1 : -1;

            //カメラの回転情報を取得。
            _camRot=GameSerializeData.GameData._CameraTransform.rotation;
            //カメラの傾き具合を設定
            float tilt = Data.IsTaeget ? Data.CameraTargetTilt : Data.CameraTilt;
            //傾き具合をQuaternionに変換
            Quaternion tiltRot = Quaternion.AngleAxis(-tilt, Vector3.right);
            //カメラの回転情報と、傾きの回転情報を合成
            _camRot=_camRot*tiltRot;

            //前後の移動を設定
            _forward = _camRot* _forwardSpeed*i;
        }
        else
        {
            _forward = Vector3.zero;
        }

        //左右の動き
        if(Input.GetKey(Data.Right)||Input.GetKey(Data.Left))
        {
            //前左移動か右移動かを判定
            int i = Input.GetKey(Data.Right) ? 1 : -1;
            //左右の移動を設定
            _right = (Data.CharacterSpeed * i) * GameSerializeData.GameData._CameraTransform.right;
        }
        else
        {
            _right = Vector3.zero;
        }

        //前後左右の移動を合わせる。
        _force = _forward + _right;

        //力量に応じた移動・回転を行う
        _controlScr.SetMove(_force);
    }

    private void KeyAttack()
    {
        _controlScr.Atttack(Input.GetKeyDown(Data.Attack));
    }


}
