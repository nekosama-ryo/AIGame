using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinkingManager
{
    byte now = 0b_1000_0000;
    //思考プロセスの流れを管理
    public void MainThinking(byte thinking)
    {
        //行動中かどうかをビット演算で求める。
        int i = thinking & now;
        //先頭１ビットから行動中か動かを調べる。行動中の場合以降の処理を行わない。
        if (0b_1000_0000 == i) return;





    }



    void Start()
    {

    }

    void Update()
    {

    }
}
