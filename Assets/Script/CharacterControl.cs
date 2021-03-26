﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>キャラクターの行動動作 </summary>
public class CharacterControl
{
    //コンストラクタ
    public CharacterControl(Transform CharaTrs, Rigidbody CharaRigid, Collider CharaCol, Animator CharaAnim, Transform HpTrs)
    {
        //コンポーネントの設定
        _playerTrs = CharaTrs;
        _rb = CharaRigid;
        _col = CharaCol;
        _ani = CharaAnim;
        _hpTrs = HpTrs;

        //初期位置の取得
        _latestPos = _playerTrs.position;
        //初期HPを設定
        _hp = Data.CharacterMaxHp;
        //ダメージアニメーションのハッシュ値を取得
        _damageHash = Animator.StringToHash(Data.AnimationNameDamage);
    }

    //コンポーネントの情報
    private Transform _playerTrs = default;
    private Rigidbody _rb = default;
    private Collider _col = default;
    private Animator _ani = default;
    private Transform _hpTrs = default;
    //現在の移動力量
    private Vector3 _force = Vector3.zero;
    //前回の位置情報
    private Vector3 _latestPos = Vector3.zero;
    //攻撃受付時間の管理
    private float _attackTime = 0;
    private float _attackWaitTime = 0;
    //現在のHP
    private float _hp = default;
    //前回のダメージを受けた際のアニメーションハッシュ値
    private int _hash = 0;
    //ダメージのアニメーションハッシュ値
    private int _damageHash = default;


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
        Vector3 diff = _playerTrs.position - _latestPos;
        _latestPos = _playerTrs.position;

        //差が大きければキャラクターの向きを調整する
        if (diff.magnitude > 0.07f)
        {
            _playerTrs.rotation = Quaternion.LookRotation(diff);
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
        //行動アニメーション再生中の場合移動処理を行わない
        if (!_ani.GetCurrentAnimatorStateInfo(0).IsTag(Data.AnimationTagMove)) return;

        //アニメーションの再生
        _ani.SetBool(Data.AnimationRun, force != Vector3.zero ? true : false);

        //移動処理
        _rb.AddForce(force);
        //回転処理
        Rotation();
    }

    /// <summary>攻撃を行う </summary>
    public void Atttack(bool flag)
    {
        //ダメージを受けている際は処理を行わない
        if (_ani.GetCurrentAnimatorStateInfo(0).IsTag(Data.AnimationTagDamage)) return;

        //攻撃を行う。
        if (flag)
        {
            //受付時間のリセット
            _attackTime = 0;

            //アニメーションの再生
            _ani.SetBool(Data.AnimationAttack, true);
        }

        //攻撃行動中かどうか
        if(_ani.GetCurrentAnimatorStateInfo(0).IsTag(Data.AnimationTagAttack))
        {
            //武器の当たり判定をオンにする。
            _col.enabled = true;
        }
        else
        {
            //武器の当たり判定をオフにする。
            _col.enabled = false;
            return;
        }

        //受付時間の加算
        _attackTime += Time.deltaTime;

        //受付時間が過ぎたら、攻撃行動をリセットする。
        if (_attackTime > Data.CharacterAttackTime) _ani.SetBool(Data.AnimationAttack, false);
    }

    /// <summary>現在行っているアニメーションのハッシュ値を返す。 </summary>
    public int GetAnimationHash()
    {
        //前回と同じ攻撃だったら足さない
        return _ani.GetCurrentAnimatorStateInfo(0).shortNameHash;
    }

    /// <summary>ダメージを受けた際の挙動 </summary>
    public void DamageMove(ref bool OnCollider,int hash)
    {
        //ダメージを受けていない場合は以降の処理をしない
        if (!OnCollider || hash == _hash) return;
        //前回のダメージ時とハッシュ値が同じか、ダメージハッシュ値の場合は以降の処理をしない
        //連続ダメージが起きなくなってしまう
        if (hash==_hash||hash==_damageHash) return;

        //ガードを行っているかどうか
        if (_ani.GetBool(Data.AnimationDefend))
        {
            //ガードのアニメーションの再生
            _ani.Play(Data.AnimationNameDefense, 0, 0);
        }
        else
        {
            //ダメージ処理
            Damage();
        }

        //ハッシュ値を保持しておく。
        _hash = hash;
        //当たり判定をリセット
        OnCollider = false;
    }

    /// <summary>ダメージの処理 </summary>
    private void Damage()
    {
        //体力バーの長さを取得
        Vector3 size = _hpTrs.localScale;
        //体力を減らす。０以下にはならない。
        _hp = _hp - 1 < 0 ? 0 : _hp - 1;
        //体力バーの長さを調整
        size.x = size.x == 0 ? 0 : _hp / Data.CharacterMaxHp;
        //体力バーの長さを画面に反映
        _hpTrs.localScale = size;

        //死亡しているかどうか
        if (_hp == 0)
        {
            //死亡処理
            _col.enabled = false;
            //死亡のアニメーションの再生
            _ani.SetBool(Data.AnimationDie, true);
        }
        else
        {
            //ダメージのアニメーションの再生
            _ani.Play(Data.AnimationNameDamage, 0, 0);
        }
    }

    /// <summary>防御を行う </summary>
    public void Defense(bool flag)
    {
        //ダメージを受けている際は処理を行わない
        if (_ani.GetCurrentAnimatorStateInfo(0).IsTag(Data.AnimationTagDamage)) return;

        //ガード処理
        _ani.SetBool(Data.AnimationDefend, flag);
    }
}