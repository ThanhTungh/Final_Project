using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FollowCamare : MonoBehaviour
{
    [SerializeField] Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate() {
        transform.position = player.position + new Vector3(0, 0, -10);
    }
}
