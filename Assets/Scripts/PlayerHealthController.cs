using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    // hàm static kh hiển thị ra phía game scenes
    public static PlayerHealthController instance;

    public int currentHealth, maxHealth;

    //thêm khả năng vô địch khi bẫy dài gồm nhiều cái gộp lại thì chỉ bị một lần sát thương
    public float invincibleLength;
    private float invincibleCounter;

    private SpriteRenderer thSR;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        thSR = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // nếu set Length vô địch trong một giây thì nó sẽ trừ đi số lần qua mỗi khung hình trong vòng 1 giây voiws deltatime
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
            //khi hết thời gian vô địch thì sẽ trở về hình dạng bth và sẽ bị trừ máu lại
            if (invincibleCounter <= 0)
            {
                thSR.color = new Color(thSR.color.r, thSR.color.g, thSR.color.b, 1f);

            }
        }
    }

    public void DealDamage()
    {
        //nếu thời gian đếm vô địch dưới 0 thì sẽ trừ máu
        if (invincibleCounter <= 0)
        {


            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //gameObject.SetActive(false);

                //gọi lệnh chờ thời gian phản hồi để hồi sinh
                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                //nếu dẫm bẫy thì set vô địch với length đc set là 1 giây, set bị đẩy lùi khi dẫm bẫy
                invincibleCounter = invincibleLength;
                thSR.color = new Color(thSR.color.r, thSR.color.g, thSR.color.b, 0.5f);

                PlayerController.instance.KnockBack();

            }

            UIController.instance.UpdateHealthDisplay();
        }
    }
}
