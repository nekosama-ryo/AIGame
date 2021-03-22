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
    static public readonly KeyCode UP = KeyCode.UpArrow;
    static public readonly KeyCode Down = KeyCode.DownArrow;
    static public readonly KeyCode Right = KeyCode.RightArrow;
    static public readonly KeyCode Left = KeyCode.LeftArrow;

    static public readonly KeyCode Attack = KeyCode.Space;
    static public readonly KeyCode Defense = KeyCode.LeftShift;

    //現在のシーン情報
    static public int SceneNumber { get; private set; } = 0;

    //それぞれのシーンの番号
    public const int TitleSceneNumber = 0;//タイトル
    public const int GameSceneNumber = 1;//ゲームシーン

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
