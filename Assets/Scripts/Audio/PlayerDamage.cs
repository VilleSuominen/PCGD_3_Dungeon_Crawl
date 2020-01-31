using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{

    public AudioClip playerTookDamage;
    public AudioClip hitPlayerShield;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Controller")
        {
            source.PlayOneShot(playerTookDamage);
        } else
        {
            source.PlayOneShot(hitPlayerShield);
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}