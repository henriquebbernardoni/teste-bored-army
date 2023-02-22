using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    Camera _camera;
    PlayerController playerController;

    [SerializeField] private EnemyBase[] enemies;
    [SerializeField] private Transform[] hpBars;
    [SerializeField] private NavMeshAgent[] enemyAgents;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private float spawnTime;
    private Transform chosenSpawnPoint;
    private EnemyBase chosenEnemy;

    private void Awake()
    {
        _camera = Camera.main;
        playerController = GetComponent<PlayerController>();
        SetUpSpawnPoints();
        SetUpEnemyAgents();
        SetUpHealthBars();
    }

    private void Start()
    {
        StartCoroutine(EnemySpawn(spawnTime));
    }

    private IEnumerator EnemySpawn(float spawnTime)
    {
        while (true)
        {
            SelectSpawnPoint();
            SelectEnemy();
            if (!chosenEnemy.gameObject.activeInHierarchy)
            {
                chosenEnemy.gameObject.SetActive(true);
            }
            //chosenEnemy.Agent.transform.position = chosenSpawnPoint.position;
            NavMesh.SamplePosition(chosenSpawnPoint.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas);
            chosenEnemy.Agent.transform.position = hit.position;
            chosenEnemy.transform.position = chosenSpawnPoint.position;
            chosenEnemy.transform.rotation = chosenSpawnPoint.rotation;
            chosenEnemy.RestoreShip();
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void SelectSpawnPoint()
    {
        List<Transform> tempSpawnList = new(spawnPoints);
        tempSpawnList.Remove(chosenSpawnPoint);
        int randomValue = Random.Range(0, tempSpawnList.Count);
        chosenSpawnPoint = tempSpawnList[randomValue];
    }

    private void SelectEnemy()
    {
        List<EnemyBase> tempEnemyList = new(enemies);
        for (int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i].IsShipActive)
            {
                tempEnemyList.Remove(enemies[i]);
            }
        }
        int randomValue = Random.Range(0, tempEnemyList.Count);
        chosenEnemy = tempEnemyList[randomValue];
    }

    private void SetUpSpawnPoints()
    {
        for (int i = 0; i < 4; i++)
        {
            spawnPoints[i].position = _camera.ViewportToWorldPoint(new Vector2(0f, (i + 1f) / (5f)));
            spawnPoints[i].position += 2f * Vector3.left;
            spawnPoints[i].position -= spawnPoints[i].position.z * Vector3.forward;

            spawnPoints[i + 4].position = _camera.ViewportToWorldPoint(new Vector2(1f, (i + 1f) / (5f)));
            spawnPoints[i + 4].position += 2f * Vector3.right;
            spawnPoints[i + 4].position -= spawnPoints[i + 4].position.z * Vector3.forward;

            spawnPoints[i + 8].position = _camera.ViewportToWorldPoint(new Vector2((i + 1f) / (5f), 0f));
            spawnPoints[i + 8].position += 2f * Vector3.down;
            spawnPoints[i + 8].position -= spawnPoints[i + 8].position.z * Vector3.forward;

            spawnPoints[i + 12].position = _camera.ViewportToWorldPoint(new Vector2((i + 1f) / (5f), 1f));
            spawnPoints[i + 12].position += 2f * Vector3.up;
            spawnPoints[i + 12].position -= spawnPoints[i + 12].position.z * Vector3.forward;
        }
    }

    private void SetUpEnemyAgents()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].SetUpAgent(enemyAgents[i]);
        }
    }

    private void SetUpHealthBars()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<HealthController>().SetHPBar(hpBars[i]);
            hpBars[i].gameObject.SetActive(false);
        }
    }
}