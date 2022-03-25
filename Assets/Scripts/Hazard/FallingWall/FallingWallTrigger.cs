using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWallTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject[] spikes;

    Timer timer;
    public bool isTriggered = false;

    private void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player" && isTriggered == false)
        {

            StartCoroutine(moveSpike());

            GameObject fallingWall = GameObject.Find("FallingWall");
            GameObject wallPivot = GameObject.Find("FallingWall/wallPivot");
            Rigidbody2D rb = fallingWall.GetComponent<Rigidbody2D>();
            fallingWall.transform.RotateAround(wallPivot.transform.position, new Vector3(0,0,1), -20);

            isTriggered = true;
        }
        
    }

    IEnumerator moveSpike()
    {
        foreach(GameObject spike in spikes)
        {
            float spikewidth = spike.GetComponent<Renderer>().bounds.size.x;

            Vector3 targetPosition = new Vector3(spike.transform.position.x + spikewidth, spike.transform.position.y, spike.transform.position.z);
            spike.transform.position = Vector3.MoveTowards(spike.transform.position, targetPosition, 1f);
        }
        yield return new WaitForSeconds(1);
    }
}
