using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource source;
    public AudioClip swordHitSkelly;
    public AudioClip swordHitShield;
    public AudioClip hitPlayer;
    public AudioClip enemyDeath;
    public AudioClip parrySound;

    public AudioClip goblinHit;
    public AudioClip goblinBlock;
    public AudioClip goblinDeath;
    public AudioClip playerDeath;


    void Start()
    {
        source = GetComponent<AudioSource>();

    }

    public void SwordHitSkellySound()
    {
        source.PlayOneShot(swordHitSkelly, 0.1f);
    }

    // Tätä voi käyttää kummankin shieldiin? no mikä ettei
    public void SwordHitShieldSound()
    {
        source.PlayOneShot(swordHitShield, 0.1f);
    }

    public void PlayerDamagedSound()
    {
        source.PlayOneShot(hitPlayer, 0.1f);
    }

    public void EnemyDeathSound()
    {
        source.PlayOneShot(enemyDeath, 0.1f);
    }

    public void Parry()
    {
        source.PlayOneShot(parrySound, 0.1f);
    }

    public void GoblinHit()
    {
        source.PlayOneShot(goblinHit, 0.1f);
    }

    public void GoblinBlock()
    {
        source.PlayOneShot(goblinBlock, 0.1f);
    }

    public void GoblinDeath()
    {
        source.PlayOneShot(goblinDeath, 0.1f);
    }

    public void PlayerDeath()
    {
        source.PlayOneShot(playerDeath, 0.1f);
    }

}