using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip damage;
    public AudioClip attack;
    public AudioClip repair;

    AudioSource audioData;

    #region Singleton
    public static SoundManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAttackSound()
    {
        StartCoroutine("PlayAttackCoroutine");
    }

    IEnumerator PlayAttackCoroutine()
    {
        audioData.clip = attack;
        audioData.Play();
        yield return new WaitForSeconds(audioData.clip.length); 
        audioData.clip = damage;
        audioData.Play();
    }


    public void PlayRepairSound()
    {
        audioData.clip = repair;
        audioData.Play();
    }

}
