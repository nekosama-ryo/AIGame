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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Data.CharacterTagWeapon)) OnWeapon(false);
    }

    private void OnWeapon(bool on)
    {
        switch (caharacter)
        {
            case CharacterEnum.player:
                {
                    Debug.Log(on);
                    Data.PlayerOnDamage = on;
                }
                return;

            case CharacterEnum.ai:
                {
                    Data.AIOnDamage = on;
                }
                return;

            default:
                return;
        }
    }
}
