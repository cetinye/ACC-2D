using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject ballPrefab;
    public Camera cameraRef;
    public GameObject startingPos;
    public GameObject blockPrefab;

    bool available = true;
    public int ballCount = 15;
    private int destroyedBalls = 0;
    private int blockPositionCount = 19;
    private float blockSpawnProbability = 0.6f;
    //private float stretchAmount = 1;

    // Start is called before the first frame update
    void Start()
    {
        cameraRef = Camera.main;
        if (cameraRef == null)
        {
            Debug.Log("Camera not found");
        }

        SpawnBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 projectedVector = 
            cameraRef.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        Vector3 direction = (projectedVector - startingPos.transform.position);

        if (Input.GetMouseButton(0) && available)
        {
            StartCoroutine("SpawnBalls", direction);
            available = false;
        }

        /*Debug.DrawLine(startingPos.transform.position, 
            startingPos.transform.position + (direction * stretchAmount), Color.red);*/
    }

    private void SpawnBlocks()
    {
        for (int blockIndex = 0; blockIndex < blockPositionCount; blockIndex++)
        {
            if (Random.Range(0f, 1f) < blockSpawnProbability)
            {
                Instantiate(blockPrefab, new Vector3(blockIndex - 9.25f, 4.5f, 0f), Quaternion.identity);
            }
        }
    }

    IEnumerator SpawnBalls(Vector3 direction)
    {
        for (int ballIndex = 0; ballIndex < ballCount; ballIndex++)
        {
            GameObject instBall = Instantiate(ballPrefab, startingPos.transform.position, Quaternion.identity);
            instBall.GetComponent<BallMovement>().SetVelocity(direction.normalized);
            yield return new WaitForSeconds(0.1f);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ball"))
        {
            Destroy(collision.gameObject);
            destroyedBalls++;
        }

        if (destroyedBalls >= ballCount)
        {
            MoveBlocks();
            destroyedBalls = 0;
        }
    }

    private void MoveBlocks()
    {
        foreach (GameObject block in GameObject.FindGameObjectsWithTag("Blocks"))
        {
            block.transform.position -= new Vector3(0, 1, 0);
        }
        SpawnBlocks();
        available = true;
    }
}
