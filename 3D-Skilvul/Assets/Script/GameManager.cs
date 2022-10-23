using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject grass;
    [SerializeField] GameObject road;
    [SerializeField] int extent = 7;
    // jarak dari player ke batas

    [SerializeField] int frontDistance = 10;
    // jarak dari player ke depan

    [SerializeField] int backDistance = -5;
    // jarak dari player ke belakang

    [SerializeField] int maxSameTerrainRepeat = 3;
    // jarak dari player ke belakang

    Dictionary<int, TerrainBlock> map = new Dictionary<int, TerrainBlock>(50);
    TMP_Text gameOverText;

    private void Start()
    {
        // setup GameOverPanel
        GameOverPanel.SetActive(false);
        gameOverText = GameOverPanel.GetComponentInChildren<TMP_Text>();

        // bagian belakang
        for (int z = backDistance; z <= 0; z++)
        {
            CreateTerrain(grass, z);

        }
        // bagiannya depan
        for (int z = 1; z <= frontDistance; z++)
        {
            // penentuan jenis block dengan probabilitas 50%
            var prefab = GetNextRandomTerrainPrefab(z);

            // instantiate block
            CreateTerrain(prefab, z);
        }

        player.SetUp(backDistance, extent);

    }

    private int playerLastMaxTravel;
    private void Update()
    {
        // cek player sudah mati atau belum
        if (player.IsDead && GameOverPanel.activeInHierarchy == false)
            StartCoroutine(ShowGameOverPanel());

        // infinite terrain system
        // if (player.MaxTravel > playerLastMaxTravel)
        if (player.MaxTravel == playerLastMaxTravel)
            return;

        playerLastMaxTravel = player.MaxTravel;

        // bikin block baru di depan
        var randTbPrefab = GetNextRandomTerrainPrefab(player.MaxTravel + frontDistance);
        CreateTerrain(randTbPrefab, player.MaxTravel + frontDistance);

        // hapus block yang sudah lewat
        var lastTB = map[player.MaxTravel - 1 + backDistance];
        map.Remove(player.MaxTravel - 1 + backDistance);
        Destroy(lastTB.gameObject);

        player.SetUp(player.MaxTravel + backDistance, extent);
    }

    IEnumerator ShowGameOverPanel()
    {
        yield return new WaitForSeconds(3); // delay 3 detik

        gameOverText.text = "Game Over : " + player.MaxTravel;
        GameOverPanel.SetActive(true);
    }

    private void CreateTerrain(GameObject prefab, int zPos)
    {
        var go = Instantiate(prefab, new Vector3(0, 0, zPos), Quaternion.identity);
        var tb = go.GetComponent<TerrainBlock>();
        tb.Build(extent);

        map.Add(zPos, tb);
        // Debug.Log(map[zPos] is Road);
    }

    private GameObject GetNextRandomTerrainPrefab(int nextPos)
    {
        bool isUniform = true;
        var tbRef = map[nextPos - 1];
        for (int distance = 2; distance <= maxSameTerrainRepeat; distance++)
        {
            if (map[nextPos - distance].GetType() != tbRef.GetType())
            {
                isUniform = false;
                break;
            }
        }

        if (isUniform)
        {
            if (tbRef is Grass)
                return road;
            else
                return grass;
        }

        //penentuan terrain block dengan probabilitas 50%
        return Random.value > 0.5f ? road : grass;
    }
}


