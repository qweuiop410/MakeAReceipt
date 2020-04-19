using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgIcons : MonoBehaviour
{
    public float speed = 0;
    
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5.6f)
        {
            transform.position = new Vector3(3, 5.6f, 0);
        }
        else {
            Vector3 pos = transform.position;
            transform.position = new Vector3(pos.x - Time.deltaTime * (speed/2), pos.y - Time.deltaTime * speed, pos.z);
        }
    }
}
