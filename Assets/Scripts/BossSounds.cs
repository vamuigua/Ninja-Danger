using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSounds : MonoBehaviour
{
    private AudioSource source;
    public AudioClip[] bossSounds;

    private float nextBossSound;
    public float timeBetweenBossSounds;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.time >= nextBossSound)
        {
            int randNum = Random.Range(0, bossSounds.Length);
            source.clip = bossSounds[randNum];
            source.Play();
            nextBossSound = Time.time + timeBetweenBossSounds;
        }
    }
}
