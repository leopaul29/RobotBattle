using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoboDefens : MonoBehaviour
{
    [SerializeField] public ParticleSystem particle;
    public AudioClip source1;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        particle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        StartCoroutine("difens");
        
    }
    IEnumerator difens()
    {
        particle.Play();
        audio.PlayOneShot(source1);
        yield return new WaitForSeconds(3);
        particle.Stop();
    }
}
