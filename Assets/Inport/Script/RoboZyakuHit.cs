using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboZyakuHit : MonoBehaviour
{//上昇、下降のbool
    bool m_xPlus = true;
    //上昇する距離
    public float Max;
    //上昇がマックスに行ったとき下に降りる距離
    public float Min;
    //idolかどうかのチェック
    bool on = true;
    //アニメーション
    public Animator shoot;
    //プレイヤーの前から出る球
    public GameObject tama;
    public AudioClip sorce1;
    public AudioClip sorce2;
    private AudioSource audio;
    Vector3 Pos;
    public GameObject DamegeEfect;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        Pos = transform.position;
        //相手を見る
        enemy = GameObject.FindGameObjectWithTag("Player2");
        //アニメーターを取得
        shoot = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
        
            if (m_xPlus)
            {
                transform.position += new Vector3(0, 0.15f * Time.deltaTime, 0f);
                if (transform.position.y >= Max)
                    m_xPlus = false;
            }
            else
            {
                transform.position -= new Vector3(0, 0.15f * Time.deltaTime, 0f);
                if (transform.position.y <= Min)
                    m_xPlus = true;
            }
        

    }
    public void OnClick()
    {
        //adolストップ
        
        //アニメーションを再生
        shoot.SetBool("ShootTriger", true);
        //目の前に銃弾オブジェクトを作成
        Instantiate(tama, transform.position + new Vector3(2, 0, 0), Quaternion.identity);
        audio.PlayOneShot(sorce1);
        Invoke("a", 3);
    }
   void a()
    {
        Instantiate(DamegeEfect, enemy.transform.position, Quaternion.identity);
        audio.PlayOneShot(sorce2);
        shoot.SetBool("ShootTriger", false);
    }
}
