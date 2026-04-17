using System;
using System.Collections;
using UnityEngine;

public class InstanciarBombas : MonoBehaviour
{
    [SerializeField] private GameObject bombaPrefab;
    [SerializeField] private Transform player;

    [SerializeField] private float spawnInterval = 2f;
    private float distancia = 5f;
    private float altura = 30f;

    public PlayerMovement playerMovement;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(InstanciarBomb());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator InstanciarBomb()
    {
        while (true)
        {
            if (playerMovement.muerto) break;

            Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
            Vector3 spawnPosition = player.position + new Vector3(randomDirection.x, altura, randomDirection.y) * UnityEngine.Random.Range(0, distancia);

            Instantiate(bombaPrefab, spawnPosition, Quaternion.EulerAngles(180f, 0, 0));

            yield return new WaitForSeconds(spawnInterval);

        }
    }
}
