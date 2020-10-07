using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;

public class PlayerMenuInteract : MonoBehaviour
{
    private int _layerMask;
    void Start()
    {
        _layerMask = 1 << 8;
        _layerMask = ~_layerMask;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hitInfo;
        Ray ray = new Ray(transform.position, transform.forward);
        
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, _layerMask))
        {
            //Debug.DrawRay(transform.position, ray.direction, Color.white);
            if (hitInfo.collider.tag == "menu")
            {
                hitInfo.collider.GetComponent<Button>().Select();
                if (Input.anyKeyDown)
                {
                    hitInfo.collider.GetComponent<Button>().onClick.Invoke();
                }
            }
        }
    }
}
