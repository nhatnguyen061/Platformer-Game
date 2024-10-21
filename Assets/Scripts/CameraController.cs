using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Transform target;
    //di chuyển background theo main camera
    public Transform farBackground, middleBackground;

    public float minHeight, maxHeight;
    //tạo thêm biến xác nhận xem camera cần theo nữa không khi end level
    public bool stopFollow;

    //vị trí trước đó của khung hình để set đi theo player
    private Vector2 lastPos;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        lastPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopFollow)
        {
            //set cho background và main camera đi theo hướng của player
            //set vị trí main camera theo player, lấy trục y theo min-max height cho phép là clamp
            transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);

            Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);
            //để background sau chạy theo khung hình trước đó của player theo phía trục x,y với player    
            //tạo hiệu ứng đang chạy qua hàng cây
            farBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f);
            middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * .5f;
            lastPos = transform.position;
        }
        
        
    }
}
