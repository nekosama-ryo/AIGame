using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAI
{
    CharacterControl _charaScr = default;
    float time = 0;
    int ran = 0;
    bool x1 = false;
    bool x2 = false;
    bool z1 = false;
    bool z2 = false;

    bool attack = false;
    float attackTime = 0;
    float attackWaitTime = 5;
    public void OnStart()
    {
        _charaScr = new CharacterControl(GameSerializeData.GameData._AITransform, GameSerializeData.GameData._PlayerTransform,
            GameSerializeData.GameData._AIRigidbody, GameSerializeData.GameData._AICollider, GameSerializeData.GameData._AIAnimator,GameSerializeData.GameData._AIHPBar);
    }

    public void OnUpdate()
    {
        _charaScr.Defense(true);
        //_charaScr.Atttack(true);
        //RandomAttack();
        _charaScr.DamageMove(ref Data.AIOnDamage);
        _charaScr.Look();
    }

    private void RandomRun()
    {
        x1 = x2 = z1 = z2 = false;
        if (time > 2)
        {
            switch (ran)
            {
                case 0:
                    x1 = true;
                    break;
                case 1:
                    x2 = true;
                    break;
                case 2:
                    z1 = true;
                    break;
                case 3:
                    z2 = true;
                    break;
            }
            time += Time.deltaTime;
        }
        else
        {
            time += Time.deltaTime;
        }

        _charaScr.MoveX(x1, x2);
        _charaScr.MoveZ(z1, z2);
        _charaScr.SetMove();

        if (Physics.Raycast(GameSerializeData.GameData._AITransform.position, GameSerializeData.GameData._AITransform.forward, 3) && time > 2.5f)
        {
            ran = Random.Range(0, 4);
            time = 0;
        }
    }

    private void RandomAttack()
    {
        attackTime += Time.deltaTime;
        if(!attack&&attackTime+4<attackWaitTime)
        {
            attack = true;
        }
        else
        {
            attack = false;
        }

        if (attackTime>attackWaitTime)
        {
            attackTime = 0;
            attackWaitTime = Random.Range(0,10);
            attack = false;
        }

        _charaScr.Atttack(attack);
    }
}
