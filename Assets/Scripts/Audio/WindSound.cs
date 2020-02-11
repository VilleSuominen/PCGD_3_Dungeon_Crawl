using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSound : MonoBehaviour
{
    private AudioSource source;
    public AudioClip wind;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.clip = wind;
        source.Play();
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
