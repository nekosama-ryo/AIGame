using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOnCollider : MonoBehaviour
{
    [SerializeField]
    private CharacterEnum caharacter = default;

    private enum CharacterEnum
    {
        player,
        ai
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Data.CharacterTagWeapon)) OnWeapon(true, other.gameObject.name);
    }

    private void OnWeapon(bool on, string s)
    {
        switch (caharacter)
        {
            case CharacterEnum.player:
                {
                    Data.PlayerOnDamage = on;
                    //Debug.Log("ーーーーPlayerは" + s + "でダメージを受けた-ーーー");
                    //Debug.Log("Hash：" + Data.PlayerHash + "　　前回のHash：" + hash);
                }
                break;

            case CharacterEnum.ai:
                {
                    Data.AIOnDamage = on;
                    //Debug.Log("ーーーーAIは" + s + "でダメージを受けたーーーー");
                    //Debug.Log("Hash：" + Data.AIHash + "　　前回のHash：" + hash);
                }
                break;

            default:
                return;
        }
    }
}
