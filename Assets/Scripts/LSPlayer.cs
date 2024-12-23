using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public MapPoint currentPoint;
    public float moveSpeed = 10f;
    private bool levelLoading;
    public LSManager theManager;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check vi tri hien tai cua player voi currentpoint va check xem currentpoint # levelLoading
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, currentPoint.transform.position) < .1f && ! levelLoading)
        {

    
        if(Input.GetAxisRaw("Horizontal") > .5f)
        {
            if(currentPoint.right != null)
            {
                currentPoint= currentPoint.right;

            }
        }
        if (Input.GetAxisRaw("Horizontal") < -.5f)
        {
            if (currentPoint.left != null)
            {
                currentPoint = currentPoint.left;

            }
        }
        if (Input.GetAxisRaw("Vertical") > .5f)
        {
            if (currentPoint.up != null)
            {
                currentPoint = currentPoint.up;

            }
        }
        if (Input.GetAxisRaw("Vertical") < -.5f)
        {
            if (currentPoint.down != null)
            {
                currentPoint = currentPoint.down;

            }
        }

        }
        if(currentPoint.isLevel && currentPoint.levelToLoad != "" && !currentPoint.isLocked )   
        {
            // show levelname in the current point
            LSUIController.instance.ShowInfo(currentPoint);
            if(Input.GetButtonDown("Jump"))
            {

                levelLoading = true;
                theManager.LoadLevel();
            }
        }
    }
    public void SetNextPoint(MapPoint nextPoint)
    {
        currentPoint = nextPoint;
        // move to next point hide past point
        LSUIController.instance.HideInfo();
        AudioManager.instance.PlaySFX(5);
        
    }
}
