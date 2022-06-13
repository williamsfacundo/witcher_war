using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Following_Player : MonoBehaviour
{
    [SerializeField] private Vector3 offset;

    [SerializeField] private Vector3 initialCameraAngle;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {       
        player = GameObject.FindGameObjectWithTag("Player");

        gameObject.transform.rotation = Quaternion.Euler(initialCameraAngle);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position - offset;        
    }    
}
