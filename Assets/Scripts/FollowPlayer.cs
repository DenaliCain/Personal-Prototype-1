using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private SpawnManager spawnManagerScript;
    private GameObject player;

    [SerializeField] float zFollow;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        spawnManagerScript = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (spawnManagerScript.isGameActive)
        {
            FollowPlayerPosition(zFollow);
        }
    }
    void FollowPlayerPosition(float FollowPlayerOffset)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + FollowPlayerOffset);
    }
}
