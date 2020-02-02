using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoboIdoRevese : MonoBehaviour
{
    //上昇、下降のbool
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
    //プレイヤーの敵を入れる
    public GameObject enemy;
    //Damegeオブジェクトを入れる
    public GameObject DamegeEfect;
    public int count;
    //主人公のパーティクルを出した時のノックバックの数値
    private float X_down;

    public float X_Min;

    public GameObject Right;

    private Vector3 Pos;

    public AudioClip sorce1;
    public AudioClip sorce2;
    public AudioClip sorce3;
    private AudioSource audio;

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
        //adolストップ
        on = false;
        //アニメーションを再生
        shoot.SetBool("ShootTriger", true);
        //目の前に銃弾オブジェクトを作成
        Instantiate(tama, transform.position + new Vector3(2, 0, 0), Quaternion.identity);
        audio.PlayOneShot(sorce1);
        //3秒後にaを読み込む
        Invoke("a", 3);

    }
    void a()
    {
        //idol開始
        on = true;

        this.transform.position -= new Vector3(3 * Time.deltaTime, 0);
        Instantiate(Right, transform.position, Quaternion.identity);
        audio.PlayOneShot(sorce2);
        shoot.SetBool("ShootTriger", false);

        //１秒後にbを読み込む
        Invoke("b", 3);
    }
    void b()
    {
        this.transform.Translate(-2, 0, 0);
        //見ている敵の座標に当たったかのようなオブジェクトを作成
        Instantiate(DamegeEfect, enemy.transform.position, Quaternion.identity);
        audio.PlayOneShot(sorce3);
        Invoke("c", 6);
    }
    void c()
    {
        transform.position = Pos;
    }
}
