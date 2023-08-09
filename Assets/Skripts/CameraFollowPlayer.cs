using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    GameObject player;
    bool followPlayer = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer) 
            CamFollowPlayer(); 
    }
    public void SetFollowPlayer(bool value)
    {
        followPlayer = value;
    }
    void CamFollowPlayer()
    {
        Vector3 newPosistion = new Vector3(player.transform.position.x, 
                                           player.transform.position.y, 
                                           player.transform.position.z);
        this.transform.position = newPosistion;
    }
}
