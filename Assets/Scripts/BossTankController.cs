using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{
    //tạo một enum lưu lại các trạng thái của boss như tupple k thay đổi chỉnh sửa được
    public enum bossStates { shooting, hurt, moving, ended};
    public bossStates currentState;

    public Transform theBoss;
    public Animator anim;

    [Header("Movement")]
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    private bool moveRight;
    public GameObject mine;
    public Transform minePoint;
    public float timeBetweenMines;
    private float mineCounter;

    //tạo đạn
    [Header("Shooting")]
    public GameObject bullet;
    //điểm bắn đạn
    public Transform firePoint;
    //thiet lap time bắn đạn
    public float timeBetweenShots;
    private float shotCounter;

    //thời gian chờ sau khi bị thương
    [Header("Hurt")]
    public float hurtTime;
    //thieset lập time đếm ngược khi bị thương
    private float hurtCounter;
    //taoj laij hitbox
    public GameObject hitBox;
    // Start is called before the first frame update

    [Header("Health")]
    public int health = 5;
    public GameObject explosion;
    public GameObject winPlatform;
    private bool isDefeated;
    [Header("SpeedShot")]
    public float shotSpeedUp;
    public float mineSpeedUp;
    void Start()
    {
        currentState = bossStates.shooting;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case bossStates.shooting:
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    shotCounter = timeBetweenShots;
                    var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    newBullet.transform.localScale = theBoss.localScale;
                }
                break;
            case bossStates.hurt:
                if (hurtCounter > 0)
                {
                    hurtCounter -= Time.deltaTime;
                    if (hurtCounter <= 0)
                    {
                        currentState = bossStates.moving;

                        mineCounter = 0;

                        if(isDefeated)
                        {
                            theBoss.gameObject.SetActive(false);
                            Instantiate(explosion, theBoss.position, theBoss.rotation);
                            winPlatform.gameObject.SetActive(true);

                            AudioManager.instance.StopBossMusic();

                            currentState = bossStates.ended;
                        }
                    }
                }
                break;
            case bossStates.moving:
                if (moveRight)
                {
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
                    if (theBoss.position.x > rightPoint.position.x)
                    {
                        theBoss.localScale = new Vector3(1f, 1f, 1f);
                        moveRight = false;
                        EndMovement();
                    }
                }
                else
                {
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
                    if (theBoss.position.x < leftPoint.position.x)
                    {
                        theBoss.localScale = new Vector3(-1f, 1f, 1f);
                        moveRight = true;
                        EndMovement();

                    }
                }

                mineCounter -= Time.deltaTime;

                if (mineCounter <= 0)
                {
                    mineCounter = timeBetweenMines;
                    Instantiate(mine, minePoint.position, minePoint.rotation);
                }
                break;
        }
        //dòng code sau #if sẽ chỉ chạy nếu trong Unity Editor
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeHit();
        }
#endif
    }

    public void TakeHit()
    {
        currentState = bossStates.hurt;
        hurtCounter = hurtTime;
        anim.SetTrigger("Hit");
        AudioManager.instance.PlaySFX(0);

        //phá mine khi tank chạy thêm một lần
        BossTankMine[] mines = FindObjectsOfType<BossTankMine>();
        if (mines.Length > 0)
        {
            foreach(BossTankMine foundMine in mines)
            {
                foundMine.Explode();
            }
        }
        health--;
        if(health <= 0)
        {
            isDefeated = true;
        }
        else
        {
            timeBetweenShots /= shotSpeedUp;

            timeBetweenMines /= mineSpeedUp;
        }
    }
    private void EndMovement()
    {
        currentState = bossStates.shooting;
        //shotCounter = timeBetweenShots;
        shotCounter = 0f;
        anim.SetTrigger("StopMoving");
        hitBox.SetActive(true);
    }
}
