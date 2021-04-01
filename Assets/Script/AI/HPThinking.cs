using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPThinking : MonoBehaviour
{
    /// <summary>残りHPの量から危機を感じる</summary>
    /// <param name="hp">現在のHP</param>
    /// <param name="remaining">危機を感じ始めるHP量</param>
    /// <returns>危機を数値化して返す（１００に近いほど危機を感じている）</returns>
    public int Remaining(int hp, int remaining)
    {
        int difference = 0;
        //HP量が危機領域に入っている
        if (hp < remaining)
        {
            //危機感を感じる度合を数値化（数字が１００に近い程危機を感じる）
            difference = 100 - (hp / remaining);
        }

        //危機度合いを返す
        return difference;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="opponentHp"></param>
    /// <returns></returns>
    //private int Difference(int hp, int opponentHp)
    //{

    //}
}
