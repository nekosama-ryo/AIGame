using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    private CharacterControl _controlScr = default;

    public void OnStart()
    {
        _controlScr = new CharacterControl(GameSerializeData.GameData._PlayerTransform,GameSerializeData.GameData._PlayerRigidbody,GameSerializeData.GameData._PlayerAnimator);
        
    }

    public void OnUpdate()
    {
        KeyMove();
    }

    /// <summary>キー入力に応じて移動する </summary>
    private void KeyMove()
    {
        //キー入力から力量を設定する
        _controlScr.CharacterMoveZ(Input.GetKey(Data.UP),Input.GetKey(Data.Down));
        _controlScr.CharacterMoveX(Input.GetKey(Data.Right),Input.GetKey(Data.Left));

        //力量に応じた移動を行う
        _controlScr.CharacterMove();
    }


}
