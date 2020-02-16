using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SA
{
    public class EnWeaponManager : MonoBehaviour
    {
        public EnemyWeapon enemyWeapon;


        public void Init()
        {
            enemyWeapon.ewDo.DisableDamageEnColliders();
            enemyWeapon.ewDo.DisableCollisionCollider();
            //enemyWeapon.ewDo.DisableShieldCollider();
        }

        [System.Serializable]
        public class EnemyWeapon
        {
            public List<Action> actions;
            public GameObject weaponModel;
            public EnWeaponDo ewDo;
        }
    }
}