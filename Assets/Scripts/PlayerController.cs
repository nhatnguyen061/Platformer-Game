using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //tạo singleton để khi dẫm bẫy bị đẩy lùi truy cập đúng 1 player
    public static PlayerController instance;

    public float moveSpeed;
    public Rigidbody2D theRB;
    public float jumpForce;


    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    private bool canDoubleJump;
    private SpriteRenderer theSR;

    //cập nhật thêm hiệu ứng đẩy lùi khi dẫm bẫy                                
    public float knockBackLength, knockBackForce; //thời gian đẩy lùi như thời gian dẫm bẫy vô địch
    //đếm thời gian nếu count >0 thì đang dẫm bẫy còn <0 thì chạy bình thường
    private float knockBackCounter;

    private Animator anim;

    public float bounceForce;
    //ngăn di chuyển khi chạm vào lá cờ end level
    public bool stopInput;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();   
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.instance.isPause && !stopInput)
        {
            //đầu tiên k dẫm bẫy sẽ là 0, khi dẫm bẫy sẽ set lại thời gian length là 1 là bị đẩy lùi, còn dưới 0 thì đi bình thường
            if (knockBackCounter <= 0)
            {
                theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);
                //biến kiểm tra xem player đã chạm tới ground hay chưa
                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

                if (Input.GetButtonDown("Jump"))
                {
                    if (isGrounded)
                    {
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                        AudioManager.instance.PlaySFX(11);

                        canDoubleJump = true;

                    }
                    else
                    {
                        if (canDoubleJump)
                        {
                            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                            AudioManager.instance.PlaySFX(11);
                            canDoubleJump = false;
                        }
                    }

                }
                //set condition để chuyển trạng thái của player
                if (theRB.velocity.x < 0)
                {
                    theSR.flipX = true;
                }
                if (theRB.velocity.x > 0)
                {
                    theSR.flipX = false;
                }
            }
            else
            {
                knockBackCounter -= Time.deltaTime;
                //nếu đang đi về phái trước thì bị đầy lùi, còn đi về phía sau thì bị đẩy tới. Và bị đẩy lùi trong thời gian set là 0.25 giây
                if (!theSR.flipX)
                {
                    theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
                }
                else
                {
                    theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
                }
            }

            //set chạy khi vận tốc >0
            anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
            // set  nhảy khi chạm đất = false
            anim.SetBool("isGrounded", isGrounded);
        }
        
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        //khi vừa chạm vào bẫy thì sẽ dừng di chuyển, nhảy lên với lục knockForce
        theRB.velocity = new Vector2(0f, knockBackForce);


    }

    public void Bounce()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceForce);
        AudioManager.instance.PlaySFX(11);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Platform")
        {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }
}
