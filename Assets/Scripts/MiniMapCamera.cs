using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    //almacena posicion del personaje
    public Transform player;



    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.y);

        transform.rotation = Quaternion.Euler(90, player.eulerAngles.y, 0);
    }
}
