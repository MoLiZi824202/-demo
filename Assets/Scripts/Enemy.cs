using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed=10;
    Transform[] position;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        position = Waypoints.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()//移动方法
    {
        if (index > position.Length - 1)
        {
            return;
        }
        transform.Translate((position[index].position - transform.position).normalized * Time.deltaTime*speed);
        if (Vector3.Distance(position[index].position,transform.position) < 0.2f)
        {
            index++;
        }
        if (index > position.Length - 1)
        {
            ReachDestination();
        }
    }

    void ReachDestination()
    {
        GameObject.Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        EnemySpawner.countEnemyAlive--;
    }
}
