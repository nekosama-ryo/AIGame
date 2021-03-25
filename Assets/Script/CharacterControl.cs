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
    //攻撃受付時間の管理
    private float _attackTime = 0;

    //移動処理

    /// <summary>フラグに応じてキャラクターのX軸に力を加える </summary>
    public void MoveX(bool flag, bool minusFlag)
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
    /// <summary>フラグに応じてキャラクターのZ軸に力を加える </summary>
    public void MoveZ(bool flag, bool minusFlag)
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
    /// <summary>キャラクターの向きを調整する </summary>
    public void Rotation()
    {
        //前回の向きとの差を求める。
        Vector3 diff = _tr.position - _latestPos;
        _latestPos = _tr.position;

        //差が大きければキャラクターの向きを調整する
        if(diff.magnitude>0.07f)
        {
            _tr.rotation = Quaternion.LookRotation(diff);
        }
    }
    /// <summary>力量に応じたキャラクターの移動・回転を行う </summary>
    public void SetMove()
    {
        SetMove(_force);
    }
    /// <summary>力量を指定してキャラクターの移動・回転を行う </summary>
    public void SetMove(Vector3 force)
    {
        //攻撃アニメーション再生中の場合移動処理を行わない
        if (_ani.GetCurrentAnimatorStateInfo(0).IsTag(Data.AnimationTagAttack)) return;

        //アニメーションの再生
        bool aniFlag = force != Vector3.zero ? true : false;
        _ani.SetBool(Data.AnimationRun, aniFlag);

        //移動処理
        _rb.AddForce(force);
        //回転処理
        Rotation();
    }

    //攻撃処理
    public void Atttack(bool flag)
    {
        //攻撃を行う
        if (flag)
        {
            //受付時間のリセット
            _attackTime = 0;
            //アニメーションの再生
            _ani.SetBool(Data.AnimationAttack, true);
        }

        //攻撃行動中以外は以降の処理は行わない
        if (!_ani.GetBool(Data.AnimationAttack)) return;

        //受付時間の加算
        _attackTime += Time.deltaTime;

        //受付時間が過ぎたら、攻撃行動をリセットする。
        if(_attackTime>Data.AttackTime)
        {
            _ani.SetBool(Data.AnimationAttack, false);
        }
    }
}
