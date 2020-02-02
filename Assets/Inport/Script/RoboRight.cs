using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoboRight : MonoBehaviour
{
    //上昇、下降のbool
    bool m_xPlus = true;
    //上昇する距離
    public float Max;
    //上昇がマックスに行ったとき下に降りる距離
    public float Min;
    public GameObject reza;
    public GameObject bakuha;
    public int count;
    public float Timer;
    [SerializeField] private ParticleSystem particle;
    Vector3 pos;

    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pos = transform.position;
        particle.Stop();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (count == 1)
        {
            if (m_xPlus)
            {
                transform.position += new Vector3(1f * Time.deltaTime, 0, 0f);
                if (transform.position.x >= Max)
                    m_xPlus = false;
            }
            else
            {
                transform.position -= new Vector3(1f * Time.deltaTime, 0, 0f);
                if (transform.position.x <= Min)
                    m_xPlus = true;
            }
            Timer += Time.deltaTime;
            if (Timer >= 4)
            {
                count = 0;
                particle.Stop();
                transform.position = pos;
            }
        }
    }
     public void OnClickc()
    {


        Instantiate(reza, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(sound1);
        Invoke("a", 4);
   
        
    }
    void a()
    {
        Instantiate(bakuha, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(sound2);
        Invoke("b", 3.5f);
    }
    void b()
    {        
        particle.Play();
        audioSource.PlayOneShot(sound3);
        count++;
    }
}
