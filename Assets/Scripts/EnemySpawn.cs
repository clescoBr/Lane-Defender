using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
public class EnemySpawn : MonoBehaviour
{
    public int enemiesRemaining;
    [SerializeField] private GameObject spawn1;
    [SerializeField] private GameObject spawn2;
    [SerializeField] private GameObject spawn3;
    [SerializeField] private GameObject spawn4;
    [SerializeField] private GameObject spawn5;


    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject enemy3;

    private int randomHelper; // a variable that will temporarely keep a random number 
    private GameObject cloneStorage;// a variable that will temporarely keep a clone as a reference 
    private GameObject enemyStorage; // which one of the enemy prefabs will be spawned
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        randomHelper = Random.Range(1, 4); // number that will decide what enemy spawns
        switch (randomHelper)
        {
            case 1:
                enemyStorage = enemy1;
                enemyStorage.GetComponent<EnemyMenager>().speed = Random.Range(-2.5f, -1.5f);
                break;

            case 2:
                enemyStorage = enemy2;
                enemyStorage.GetComponent<EnemyMenager>().speed = Random.Range(-3.0f, -3.5f);
                enemyStorage.GetComponent<EnemyMenager>().snake = true;

                break;

            case 3:
                enemyStorage = enemy3;
                enemyStorage.GetComponent<EnemyMenager>().speed = Random.Range(-1.5f, -0.5f);

                break;

        }




        int spawnNumb = Random.Range(1, 6); // number that will decide where enemy spawns
        switch (spawnNumb)
        {
            case 1:
             
                cloneStorage = Instantiate(enemyStorage, spawn1.transform);
                break;

            case 2:

                cloneStorage = Instantiate(enemyStorage, spawn2.transform);
                break;

            case 3:

                cloneStorage = Instantiate(enemyStorage, spawn3.transform);
                break;

            case 4:

                cloneStorage = Instantiate(enemyStorage, spawn4.transform);
                break;

            case 5:

                cloneStorage = Instantiate(enemyStorage, spawn5.transform);
                break;


        }
        if (enemiesRemaining > 0)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 2.0f)); 

            enemiesRemaining--;
            repeatLoop();
        }
        else
        {
            yield return null;

        }

    }

    private void repeatLoop()
    {
        StartCoroutine(SpawnEnemy());

    }
}
