using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>キャラクターの行動動作 </summary>
public class CharacterControl
{
    //コンストラクタ
    public CharacterControl(Transform Charatrs, Rigidbody CharaRigid, Animator CharaAnim)
    {
        //コンポーネントの設定
        _tr = Charatrs;
        _rb = CharaRigid;
        _ani = CharaAnim;

        //初期位置の取得
        _latestPos = _tr.position;
    }

    //コンポーネントの情報
    private Transform _tr = default;
    private Rigidbody _rb = default;
    private Animator _ani = default;
    //現在の移動力量
    private Vector3 _force = Vector3.zero;
    //前回の位置情報
    private Vector3 _latestPos = Vector3.zero;


    //移動処理

    /// <summary>フラグに応じてキャラクターのX軸に力を加える </summary>
    public void CharacterMoveX(bool flag, bool minusFlag)
    {
        //どちらも押されていないか、どちらも押されている、力が最大値を超えた場合
        if (!flag && !minusFlag || flag && minusFlag)
        {
            //力のリセット
            _force.x = 0;
            return;
        }

        //力の加える方向を調べる
        int i = flag ? 1 : -1;
        //加える力を設定
        _force.x = Data.CharacterSpeed * i;
    }
    /// <summary>フラグに応じてキャラクターのY軸に力を加える </summary>
    public void CharacterMoveY(bool flag, bool minusFlag)
    {
        //どちらも押されていないか、どちらも押されている、力が最大値を超えた場合
        if (!flag && !minusFlag || flag && minusFlag)
        {
            //力のリセット
            _force.y = 0;
            return;
        }

        //力の加える方向を調べる
        int i = flag && !minusFlag ? 1 : -1;
        //加える力を設定
        _force.y = Data.CharacterSpeed * i;
    }
    /// <summary>フラグに応じてキャラクターのZ軸に力を加える </summary>
    public void CharacterMoveZ(bool flag, bool minusFlag)
    {
        //どちらも押されていないか、どちらも押されている、力が最大値を超えた場合
        if (!flag && !minusFlag || flag && minusFlag)
        {
            //力のリセット
            _force.z = 0;
            return;
        }

        //力の加える方向を調べる
        int i = flag && !minusFlag ? 1 : -1;
        //加える力を設定
        _force.z = Data.CharacterSpeed * i;
    }
    /// <summary>フラグに応じてキャラクターのX軸に力を加える </summary>
    public void CharacterRotation()
    {
        Vector3 diff = _tr.position - _latestPos;
        _latestPos = _tr.position;

        if(diff.magnitude>0.07f)
        {
            _tr.rotation = Quaternion.LookRotation(diff);
        }
    }
    /// <summary>力量に応じたキャラクターの移動・回転を行う </summary>
    public void CharacterMove()
    {
        //アニメーションの再生
        bool aniFlag = _force != Vector3.zero ? true : false;
        _ani.SetBool(Data.AnimationRun, aniFlag);

        //移動処理
        _rb.AddForce(_force);
        //回転処理
        CharacterRotation();
    }

    //攻撃処理

}
