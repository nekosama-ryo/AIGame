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


    //キャラクターのパラメータ
    public const float CharacterSpeed = 100;//キャラクターの移動速度
    public const float CharacterMaxSpeed = 100;//キャラクターの最高速度



    //アニメーションのフラグ名
    static public string AnimationRun = "Run";
    static public string AnimationDie = "Die";
    static public string AnimationDefend = "Defend";
    static public string AnimationDamaged = "Damaged";
    static public string AnimationWalk = "Walk";
    










    //現在のシーン情報
    static public int SceneNumber { get; private set; } = 0;

    //それぞれのシーンの番号
    public const int TitleSceneNumber = 99;//タイトル
    public const int GameSceneNumber = 0;//ゲームシーン

    //それぞれのデータタグ名
    public const string TitleDataTagName = "TitleData";
    public const string GameDataTagName = "GameData";

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
