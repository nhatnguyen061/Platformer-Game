﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    //khi chết thì chờ thời gian để hồi sinh
    public float waitToRespawn;

    public int gemsCollected;
    public string levelToLoad;
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
        AudioManager.instance.PlaySFX(8);

        //khi thực hiện hàm trên thì chờ một thời gian với hàm yield
        yield return new WaitForSeconds(waitToRespawn - (1f / UIController.instance.fadeSpeed));
        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed)+.2f);
        UIController.instance.FadeFromBlack();

        PlayerController.instance.gameObject.SetActive(true);
        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;

        //set lại máu lại ban đầu cho player
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        UIController.instance.UpdateHealthDisplay();

    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }
    public IEnumerator EndLevelCo()
    {
        //set stop input để dừng di chuyển và làm màn hình mờ dần
        PlayerController.instance.stopInput = true;
        CameraController.instance.stopFollow = true;

        UIController.instance.levelCompleteText.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        UIController.instance.FadeToBlack();
        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + .25f);

        SceneManager.LoadScene(levelToLoad);
    }

}
