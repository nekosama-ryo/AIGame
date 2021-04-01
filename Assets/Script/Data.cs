using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>データ管理</summary>
public static class Data
{
    //ゲームマネージャーの起動状況
    static public bool IsGameManager = false;

    //キー情報
    static public readonly KeyCode UP = KeyCode.W;
    static public readonly KeyCode Down = KeyCode.S;
    static public readonly KeyCode Right = KeyCode.D;
    static public readonly KeyCode Left = KeyCode.A;

    static public readonly KeyCode Attack = KeyCode.Space;
    static public readonly KeyCode Defense = KeyCode.LeftShift;

    static public readonly KeyCode CameraRight = KeyCode.Q;
    static public readonly KeyCode CameraLeft = KeyCode.E;
    static public readonly KeyCode CameraReset = KeyCode.R;
    static public readonly KeyCode CameraTarget = KeyCode.Tab;

    //キャラクターのパラメータ
    public const float CharacterSpeed = 250;//キャラクターの移動速度
    public const float CharacterAttackTime = 0.5f;//攻撃行動の受付時間
    public const float CharacterMaxHp = 1000;//最大体力量
    public const float CharacterAttackWaitTime = 0.3f;//可能になるまでの待機処理
    public const float CharacterDefenseAngle = 90;//防御可能な範囲

    //プレイヤーのパラメータ
    static public bool PlayerOnDamage = false;//ダメージを受けたかどうか
    static public int PlayerHash = 0;//現在の再生中のアニメーションのハッシュ値

    //AIのパラメータ
    static public bool AIOnDamage = false;//ダメージを受けたかどうか
    static public int AIHash = 0;//現在の再生中のアニメーションのハッシュ値

    //AIの思考パラメータ
    static public int AIReflexes = 0;//反射神経
    static public int AIConcentration = 0;//集中力
    static public int AIActive = 0;//活発
    static public int AIIntellectual = 0;//知的
    static public int AIcarefully = 0;//慎重



    //カメラのパラメータ
    public const float CameraHeight = 3f;//カメラの高さ
    public const float CameraTilt = 35f;//カメラの傾き
    public const float CameraSpeed = 15f;//カメラの回転速度
    public const float CameraFollowSpeed = 5f;//カメラの追従速度
    public const float CameraDistance = 2.5f;//カメラとプレイヤーとの距離

    static public bool IsTaeget = false;//カメラがターゲット状態かどうか
    public const float CameraTargetHeight = 2f;//ターゲット時のカメラの高さ
    public const float CameraTargetTilt = 15f;//ターゲット時のカメラの傾き

    //タグ名
    public const string CharacterTagWeapon = "Weapon";//武器のタグ名

    //アニメーションの名前
    public const string AnimationNameDamage = "WGS_Damaged_Front";
    public const string AnimationNameDefense = "WGS_Defend_Defend";

    //アニメーションのタグ名
    public const string AnimationTagMove = "Move";
    public const string AnimationTagAttack = "Attack";
    public const string AnimationTagDamage = "Damage";

    //アニメーションのフラグ名
    public const string AnimationRun = "Run";
    public const string AnimationDie = "Die";
    public const string AnimationDefend = "Defend";
    public const string AnimationDamaged = "Damaged";
    public const string AnimationWalk = "Walk";
    public const string AnimationAttack = "Attack";











    //シーンのパラメータ
    static public int SceneNumber { get; private set; } = 0;//現在のシーン情報

    public const int TitleSceneNumber = 99;//タイトルシーンの番号
    public const int GameSceneNumber = 0;//ゲームシーンの番号

    public const string TitleDataTagName = "TitleData"; //タイトルシーンタグ名
    public const string GameDataTagName = "GameData";//ゲームシーンタグ名

    //処理
    /// <summary>初期化処理。最初期に呼び出す </summary>
    static public void StartSceneSetting()
    {
        SceneNumber = SceneManager.GetActiveScene().buildIndex;
    }

    /// <summary>シーンを設定する。 </summary>
    static public void SetScene(int SceneNum)
    {
        //ロード中は一度ゲームマネージャーを停止刺せる
        IsGameManager = false;
        //シーン遷移
        SceneNumber = SceneNum;
        SceneManager.LoadSceneAsync(SceneNum);
    }
}
