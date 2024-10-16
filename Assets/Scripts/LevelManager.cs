using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    //khi chết thì chờ thời gian để hồi sinh
    public float waitToRespawn;

    public int gemsCollected;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        //tao vong lap hoi sinh trong vong bao nhieu giay
        StartCoroutine(RespawnCo());

    }
    //tạo một vòng lặp cho việc hồi sinh nằm ngoài vòng lặp ụpdate
    private IEnumerator RespawnCo()
    {
        //player duoc gan script PlayerController
        PlayerController.instance.gameObject.SetActive(false);
        //khi thực hiện hàm trên thì chờ một thời gian với hàm yield
        yield return new WaitForSeconds(waitToRespawn);

        PlayerController.instance.gameObject.SetActive(true);
        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;

        //set lại máu lại ban đầu cho player
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        UIController.instance.UpdateHealthDisplay();

    }

}
