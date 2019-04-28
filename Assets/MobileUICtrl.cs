using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileUICtrl : MonoBehaviour
{
    public GameObject player;
    PlayerCtrl playerCtrl;

    // Start is called before the first frame update
    void Start()
    {
        playerCtrl = player.GetComponent<PlayerCtrl>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MobileMoveLeft()
    {
        Debug.Log("calledthis");
        playerCtrl.MoveHorizontal(5);
    }
    public void MobileMoveRight()
    {
        playerCtrl.MoveHorizontal(-5);
    }

    public void MobileStopMoving()
    {
        playerCtrl.StopMoving();
    }

    public void MobileJump()
    {
        playerCtrl.Jump();
    }
    public void Fire()
    {
        playerCtrl.Fire();
    }

}

