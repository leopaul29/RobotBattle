using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboKyouHit : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
   private AudioSource audioSource;
    public AudioClip sound1;
    // Start is called before the first frame update
    void Start()
    {
        particle.Stop();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void Onclic()
    {
        particle.Play();
        audioSource.PlayOneShot(sound1);
        Invoke("a", 1);
    }
    void a()
    {
        particle.Stop();
    }
}
