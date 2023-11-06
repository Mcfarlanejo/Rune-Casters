using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float range;
    public float spawnDelay = 15;
    public float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTime >= spawnDelay)
        {
            if (Random.Range(1,3) == 1)
            {
                GameObject go = Instantiate(enemyPrefab);
                Transform t = go.GetComponent<Transform>();
                t.position = gameObject.transform.position + new Vector3(Random.Range(-range, range), Random.Range(-range, range));

                EnemyLoot el = go.GetComponent<EnemyLoot>();
                el.rarity = (Rarity)Random.Range(1,5);

                go.GetComponent<SpriteRenderer>().color = UIManager.instance.rarityColours[(int)el.rarity];
            }
            spawnTime = 0;
        }

        spawnTime += Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
