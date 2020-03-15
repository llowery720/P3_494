using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToCharacters : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    //public GameObject playerManager;
    //private PlayerManager pm;

    // Start is called before the first frame update
    void Start()
    {
        //pm = playerManager.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player1 != null && player2 != null)
        {
            Vector3 pos = gameObject.transform.position;
            pos.x = (player1.transform.position.x + player2.transform.position.x) / 2;
            pos.y = (player1.transform.position.y + player2.transform.position.y) / 2 + 1.25f;
            gameObject.transform.position = pos;
        }
        
    }
}
