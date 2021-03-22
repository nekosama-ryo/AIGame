﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>全てのゲーム内の動作を管理する </summary>
public class GameManager : MonoBehaviour
{
    //シングルトン
    private static GameManager _gameManager = default;

    private void Awake()
    {
        //シングルトン処理
        SingletonSetting();
        //データの初期処理。現在のシーン番号を保存する。
        Data.StartSceneSetting();
        //シーン読み込み時に呼び出す処理を追加することで、疑似Startメソッドを作成
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {

    }

    private void Update()
    {

        //各シーンで行う処理
        switch (Data.SceneNumber)
        {
            //タイトルシーン
            case Data.TitleSceneNumber:
                break;

            //ゲームシーン
            case Data.GameSceneNumber:
                break;
        }
    }

    /// <summary>Startと同じ動作。シーンが切り替わる際に呼び出される。</summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //多重起動の防止
        if (Data.IsGameManager) return;

        //疑似スタート処理。それぞれのシーンで最初期に行う処理
        switch (Data.SceneNumber)
        {
            //タイトルシーン
            case Data.TitleSceneNumber:
                break;

            //ゲームシーン
            case Data.GameSceneNumber:
                break;
        }

        //起動状況を変更
        Data.IsGameManager = true;
    }

    /// <summary>シングルトン処理</summary>
    private void SingletonSetting()
    {
        if (_gameManager == null)
        {
            _gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}