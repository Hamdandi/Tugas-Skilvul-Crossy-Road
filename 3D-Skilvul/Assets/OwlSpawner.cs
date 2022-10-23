using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlSpawner : MonoBehaviour
{
    [SerializeField] GameObject eglePrefab;
    [SerializeField] int spawnZPos = 7;
    [SerializeField] Player player;
    [SerializeField] float timeOut = 5f;

    [SerializeField] float timer = 0f;
    int playerLastMaxTravel = 0;

    private void SpawnOwl()
    {
        player.enabled = false;
        var position = new Vector3
        (
            player.transform.position.x,
            1,
            player.CurrentTravel + spawnZPos
        );
        var rotation = Quaternion.Euler(0, 180, 0);
        var owlObject = Instantiate(eglePrefab, position, rotation);
        var owl = owlObject.GetComponent<Owl>();
        owl.SetUpTarget(player);

    }

    private void Update()
    {
        // jika player ada kemajuan
        if (player.MaxTravel != playerLastMaxTravel)
        {
            timer = 0; // reset timer
            playerLastMaxTravel = player.MaxTravel; // update playerLastMaxTravel
            return;
        }

        // jika player tidak ada kemajuan
        if (timer < timeOut)
        {
            timer += Time.deltaTime;
            return;
        }

        // jika player tidak ada kemajuan dan timer sudah mencapai timeOut
        if (player.IsJumping() == false && player.IsDead == false)
            SpawnOwl();
    }
}
