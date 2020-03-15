using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToParent : MonoBehaviour
{
    GameObject obj;
    SpriteRenderer sr;
    public Vector3 offset = new Vector3(.25f, 0f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        obj = transform.parent.gameObject;
        sr = obj.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if(!sr.flipX) {
            transform.position = obj.transform.position + offset;
        }
        else {
            transform.position = obj.transform.position - offset;
        }
    }
}
