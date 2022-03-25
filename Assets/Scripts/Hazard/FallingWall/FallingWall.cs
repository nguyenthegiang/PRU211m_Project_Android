using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWall : MonoBehaviour
{
    private GameObject fallingWall;

    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        fallingWall = (GameObject)Resources.Load(@"Prefabs\FallingWall");


        startPosition = transform.position;
    }

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);

            Instantiate(fallingWall, startPosition, Quaternion.identity);
        }
        GameObject trigger = GameObject.Find("fallingWallTrigger");
        /*trigger.GetComponent<FallingWallTrigger>().spikes[0] = GameObject.Find("/FallingWall(Clone)/spike");
        trigger.GetComponent<FallingWallTrigger>().spikes[1] = GameObject.Find("/FallingWall(Clone)/spike (1)");
        trigger.GetComponent<FallingWallTrigger>().spikes[2] = GameObject.Find("/FallingWall(Clone)/spike (2)");
        trigger.GetComponent<FallingWallTrigger>().spikes[3] = GameObject.Find("/FallingWall(Clone)/spike (3)");
        trigger.GetComponent<FallingWallTrigger>().spikes[4] = GameObject.Find("/FallingWall(Clone)/spike (4)");
*/
        trigger.GetComponent<FallingWallTrigger>().isTriggered = false;
    }
}
