using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
	Rigidbody P_rigid;
	Rigidbody rigid;
	public GameObject player;
	public bool vert = false;
	public bool horiz = true;
	public bool frozen = false;
	public float max = 62f;
	public float min = 32f;
	public float speed = 3f;
	public float xoffset, yoffset;
    private float interpolation;
    // Start is called before the first frame update
    void Start()
    {

        P_rigid = player.GetComponent<Rigidbody>();
        xoffset = player.transform.position.x - this.transform.position.x;
        yoffset = player.transform.position.y - this.transform.position.y;
    }

    void Update()
    {
    	Vector3 pos = this.transform.position;
    	if(!frozen){
    		interpolation = speed * Time.deltaTime;
        	if(horiz){

        		if(player.transform.position.x - this.transform.position.x < (-2 + xoffset)){
        			if(player.transform.position.x + xoffset >= min - 1){
        			pos.x = Mathf.Lerp(this.transform.position.x, (player.transform.position.x - xoffset - 2), interpolation);
        			}
        			else{
        				pos.x = Mathf.Lerp(this.transform.position.x, min, interpolation);
        			}
        		}

        		else if(player.transform.position.x - this.transform.position.x > (2 + xoffset)){
        			if(player.transform.position.x + xoffset <= max + 1){
        			pos.x = Mathf.Lerp(this.transform.position.x, (player.transform.position.x - xoffset + 2), interpolation);
        			}
        			else{
        				pos.x = pos.x = Mathf.Lerp(this.transform.position.x, max, interpolation);
        			}
        		}
        		

        		this.transform.position = pos;

        	}
        	else if(vert){
                if(player.transform.position.y - this.transform.position.y < (-2 + yoffset)){
                    if(player.transform.position.y + yoffset >= min - 1){
                    pos.y = Mathf.Lerp(this.transform.position.y, (player.transform.position.y - yoffset - 3), interpolation);
                    }
                    else{
                        pos.y = Mathf.Lerp(this.transform.position.y, min, interpolation);
                    }
                }

                else if(player.transform.position.y - this.transform.position.y > (2 + xoffset)){
                    if(player.transform.position.y + yoffset <= max + 1){
                    pos.y = Mathf.Lerp(this.transform.position.y, (player.transform.position.y - yoffset + 2), interpolation);
                    }
                    else{
                        pos.y = Mathf.Lerp(this.transform.position.y, max, interpolation);
                    }
                }
                

                this.transform.position = pos;
        	}
        }	
    }
}
