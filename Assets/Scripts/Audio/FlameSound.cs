using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSound : MonoBehaviour
{
    private AudioSource source;
    public AudioClip flame;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.clip = flame;
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