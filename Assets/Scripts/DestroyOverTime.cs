using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    //thời gian loại bỏ đối tượng pickup
    public float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //đợi cho đến khoảng thời gian lifeTime thì sẽ hủy
        Destroy(gameObject, lifeTime);
    }
}
