using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public GameObject fireBullet;
    
    void Update()
    {
        ShootBullet();
    }

    void ShootBullet(){
        if (Input.GetKeyDown(KeyCode.J)){
            
        }
    }
}
