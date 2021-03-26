using System.Collections;
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
        _hp = Data.CharacterMaxHp;
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
    //アニメーションのハッシュ値
    private int _hash = 0;

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
        if (_ani.GetCurrentAnimatorStateInfo(0).IsTag(Data.AnimationTagDamage)) 
        {
            _attackWaitTime = Data.CharacterAttackWaitTime;
            return;
        }

        //攻撃を行う。ダメージを受けた直後は攻撃不可
        if (flag&& _attackTime>_attackWaitTime)
        {
            _col.enabled = true;
            //受付時間のリセット
            _attackTime = 0;
            _attackWaitTime = 0;
            //アニメーションの再生
            _ani.SetBool(Data.AnimationAttack, true);
        }

        //受付時間の加算
        _attackTime += Time.deltaTime;

        //受付時間が過ぎたら、攻撃行動をリセットする。
        if (_attackTime > Data.CharacterAttackTime)
        {
            _ani.SetBool(Data.AnimationAttack, false);
        }
    }

    public int GetAnimationHash()
    {
        //前回と同じ攻撃だったら足さない
        return _ani.GetCurrentAnimatorStateInfo(0).shortNameHash;
    }

    public void Damage(ref bool OnCollider,int hash)
    {
        //攻撃時以外は剣の当たり判定を消す。
        if (!_ani.GetBool(Data.AnimationAttack)) _col.enabled = false;

        //ダメージを受けていない場合、前回のダメージ時とハッシュ値が同じ場合は以降の処理をしない
        if (!OnCollider || hash == _hash) return;
        

        //ガードを行っているかどうか
        if (_ani.GetBool(Data.AnimationDefend))
        {
            //ガードのアニメーションの再生
            _ani.Play(Data.AnimationNameDefense, 0, 0);
        }
        else
        {
            //体力を減らして、UIに体力量を反映させる。
            Vector3 size = _hpTrs.localScale;
            _hp = _hp - 1 < 0 ? 0 : _hp - 1;
            size.x = size.x == 0 ? 0 : _hp / Data.CharacterMaxHp;
            _hpTrs.localScale = size;

            //死亡しているかどうか
            if (_hp == 0)
            {
                //死亡処理
                _col.enabled = false;
                _ani.SetBool(Data.AnimationDie, true);
            }
            else
            {
                //ダメージのアニメーションの再生
                _ani.Play(Data.AnimationNameDamage, 0, 0);
            }
        }
        _hash = hash;
        OnCollider = false;
    }

    public void Defense(bool flag)
    {
        //ダメージを受けている際は処理を行わない
        if (_ani.GetCurrentAnimatorStateInfo(0).IsTag(Data.AnimationTagDamage)) return;

        //ガード処理
        _ani.SetBool(Data.AnimationDefend, flag);
    }
}