using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingRotation : MonoBehaviour
{
    [SerializeField] float speed=1000;
    [SerializeField] float angle=180;
    Vector3 rot;
    private void Start()
    {
         rot = transform.eulerAngles;
    }
    void Update()
    {
      
        transform.eulerAngles = new Vector3(0, 0, Mathf.Sin(Time.realtimeSinceStartup)* angle) + rot;
    }
}
