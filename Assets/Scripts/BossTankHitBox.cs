using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankHitBox : MonoBehaviour
{
    //chiếu đến object boss
    public BossTankController bossCont;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Player" && PlayerController.instance.transform.position.y > transform.position.y)
        {
            bossCont.TakeHit();
            PlayerController.instance.Bounce();
            //khi boss chạy qua lại phía bên kia mới cho hitbox trở lại để gây dame
            gameObject.SetActive(false);
        }
    }
}
