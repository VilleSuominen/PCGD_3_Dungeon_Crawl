using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSound : MonoBehaviour
{
    private AudioSource source;
    public AudioClip wind;
    private float playEverySeconds = 2;
    private float timePassed = 0;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.clip = wind;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= playEverySeconds)
        {
            timePassed = 0;
            source.Play();
        }
    }
}
