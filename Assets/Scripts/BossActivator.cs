using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    public GameObject theBossBattle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //tạo ra một khoảng để nhận biết player đã đi vào vùng boss hay chưa
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            theBossBattle.gameObject.SetActive(true);
            gameObject.SetActive(false);
            AudioManager.instance.PlayBossMusic();
        }
    }
}
