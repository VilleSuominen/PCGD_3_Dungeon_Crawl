using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHit : MonoBehaviour
{

    public AudioClip swordHitSkelly;
    public AudioClip swordHitShield;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "skelly")
        {
            source.PlayOneShot(swordHitSkelly);
        } else
        {
            source.PlayOneShot(swordHitShield);
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
