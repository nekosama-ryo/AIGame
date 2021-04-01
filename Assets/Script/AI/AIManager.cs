using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public AIManager()
    {
        _trs = GameSerializeData.GameData._AITransform;

    }
    private Transform _trs=default;
    

    private enum AIAction
    {
        Move,
        Attack,
        Defence,
    }

    private int _previousHp = 0;





    private int percent(int i)
    {
        if(i>100)
        {
            return 100;
        }
        else if(i<0)
        {
            return 0;
        }
        return i;
    }
}

public enum Thinking
{
    






}
