using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    //kiểm tra nhặt vật phẩm
    public bool isGem, isHeal;

    private bool isCollected;
    //khi nhặt một gem thì sẽ gọi hiệu ứng nhặt lên
    public GameObject pickupEffect;
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
        if (other.CompareTag("Player") && !isCollected)
        {
            if (isGem)
            {
                LevelManager.instance.gemsCollected++;

                isCollected = true;
                Destroy(gameObject);
                //tạo một bản sao hiệu ứng nhặt
                Instantiate(pickupEffect, transform.position, transform.rotation);

                UIController.instance.UpdateGemCount();
                AudioManager.instance.PlaySFX(7);

            }

            if (isHeal)
            {
                if(PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)
                {
                    PlayerHealthController.instance.HealPlayer();

                    isCollected = true;
                    Destroy(gameObject);
                    //tạo một bản sao hiệu ứng nhặt máu
                    Instantiate(pickupEffect, transform.position, transform.rotation);

                    AudioManager.instance.PlaySFX(8);

                }
            }
        }
    }
}
