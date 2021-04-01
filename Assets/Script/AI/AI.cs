using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    CharacterControl _charaScr = default;
    public void OnStart()
    {
        _charaScr = new CharacterControl(GameSerializeData.GameData._AITransform, GameSerializeData.GameData._PlayerTransform,
            GameSerializeData.GameData._AIRigidbody, GameSerializeData.GameData._AICollider, GameSerializeData.GameData._AIAnimator, GameSerializeData.GameData._AIHPBar);
    }
    public void OnUpdate()
    {

    }

    





}
