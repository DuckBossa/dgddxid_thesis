﻿using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
    public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject enemy;                // The enemy prefab to be spawned.
    public GameObject parents_enemy;
    public StomachLevel_Global slg;
    public float spawnTime = 3f;            // How long between each spawn.
    public float maxEnemies;                // Max number of enemies that can spawn in the map at a time.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
 
    public float currEnemies;


    void Start() {
      
    }

    void OnEnable() {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }
    void OnDisable() {
        CancelInvoke();
    }

    void Spawn() {
        /*
        if (playerHealth.currHealth <= 0f) {
            return;
        }
        */
        if (currEnemies > maxEnemies) return;

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        var temp = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation) as GameObject;
        var isWalking = temp.GetComponent<Enemy>();
        if (isWalking != null) {
            isWalking.SetupPlayer(playerHealth.gameObject);
        }
        temp.GetComponent<EnemyHealth>().SetManager(this);
        temp.GetComponent<EnemyHealth>().SetStomachLevelGlobal(slg);
        currEnemies++;
    }
}
