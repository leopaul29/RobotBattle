using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybakuha : MonoBehaviour
{
    public GameObject Fire;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("a", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void a()
    {
        Instantiate(Fire,transform.position,Quaternion.identity);
        Destroy(this.gameObject);
    }
}
