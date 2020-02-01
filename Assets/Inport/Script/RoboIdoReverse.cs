using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoboIdoReverse : MonoBehaviour
{
    bool m_xPlus = true;
    public float Max;
    public float Min;
    bool on = true;
    public Animator shoot;
    public GameObject tama;
    public GameObject enemy;
    public GameObject DamegeEfect;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Player");
        shoot = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //待機モーションの上下
        //もし打つアニメーションをしてないとき
        if (on == true)
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

    }
    //もしボタンをクリックしたとき
    public void OnClick()
    {
        //boolをオンからオフにする
        on = false;
        //アニメーションの打ってるときを再生
        shoot.SetBool("ShootTriger", true);
        //アニメーションがついた玉オブジェクトを前にだす
        Instantiate(tama, transform.position + new Vector3(-2, 0, 0), Quaternion.identity);
        //3秒後にaを読み込む
        Invoke("a", 3);
    }
    void a()
    {
        //boolをオンにする
        on = true;
        //アニメーションをidolに戻す
        shoot.SetBool("ShootTriger", false);
        //3秒後にbを読み込む
        Invoke("b", 3);
    }
    void b()
    {
        //敵の位置に弾が当たったかのようなオブジェクトを生み出す
        Instantiate(DamegeEfect, enemy.transform.position, Quaternion.identity);
    }
}

