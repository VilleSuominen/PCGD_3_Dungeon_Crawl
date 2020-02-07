using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SA
{
    public class WeaponManager : MonoBehaviour
    {
        public Weapon currentWeapon;
        

        public void Init()
        {
            
            currentWeapon.wDo.DisableDamageColliders();
            currentWeapon.wDo.DisableParryCollider();
            
        }
    }

    [System.Serializable]
    public class Weapon
    {
        public List<Action> actions;
        public List<Action> twoHandedActions;
        
        public GameObject weaponModel;
        public WeaponDo wDo;
    }
}