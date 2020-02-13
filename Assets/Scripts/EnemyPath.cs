using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    WaveConfig waveConfig;
    float moveSpeed;

    private List<Transform> waypoints;
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWayPoints();
        transform.position = GetNextWayPointPos();
    }

    // Update is called once per frame
    void Update()
    {
        MoveFollowPath();
    }

    public void SetWaveConfig(WaveConfig waveConfig) {
        this.waveConfig = waveConfig;
        moveSpeed = waveConfig.GetMoveSpeed();
        
    }

    private void MoveFollowPath() {
        if (waypointIndex < waypoints.Count) {
            float step = moveSpeed * Time.deltaTime;
            Vector2 currPos = transform.position;
            Vector2 targetPos = GetNextWayPointPos();
            transform.position = Vector2.MoveTowards(transform.position, targetPos, step);

            if (currPos == targetPos) {
                waypointIndex++;
            }
        } else {
            Destroy(gameObject);
        }
    }
    private Vector2 GetNextWayPointPos() {
        return waypoints[waypointIndex].position;
    }
}
