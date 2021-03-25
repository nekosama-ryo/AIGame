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
        if (other.CompareTag(Data.CharacterTagWeapon)) OnWeapon(true);
    }

    private void OnWeapon(bool on)
    {
        switch (caharacter)
        {
            case CharacterEnum.player:
                {
                    Data.PlayerOnDamage = on;
                }
                break;

            case CharacterEnum.ai:
                {
                    Data.AIOnDamage = on;
                }
                break;

            default:
                return;
        }
    }
}
