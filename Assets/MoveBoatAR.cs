using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoatAR : MonoBehaviour
{
    Vector3 orignalPos;
    // Start is called before the first frame update
    void Start()
    {
        orignalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(new Vector3(0.2f,0,0)* Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("boatwall"))
        {
            transform.localPosition = orignalPos;
        }
    }
}
