using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGeneration : MonoBehaviour
{
    public static LevelGeneration instance;

    #region LevelSpawnVariables
    [SerializeField] GameObject tile;
    public GameObject endpoint;
    public GameObject startpoint;
    float z_Spawn = 0;
    float tile_Length = 9.5f;
    public List<GameObject> active_Tiles = new List<GameObject>();
    #endregion

    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        

    }

    #region Levlel_Spawning_Functions

    public void Instantiate_Tiles(int no_Of_Tiles)
    {
        for (int i = 0; i < no_Of_Tiles; i++)
        {
            GameObject spawned_Tile = Instantiate(tile, transform.forward * z_Spawn, transform.rotation);

            if (i > 0)
            {
                spawned_Tile.GetComponent<CrowdSpawning>().Spwan_Crowd();
            }

            active_Tiles.Add(spawned_Tile);

            z_Spawn += tile_Length;
        }
        startpoint = active_Tiles[0].transform.GetChild(0).gameObject;

        endpoint = active_Tiles[no_Of_Tiles - 1].transform.GetChild(1).gameObject;

        startpoint.SetActive(true);
        endpoint.SetActive(true);

        
    }
    void Destroy_Tiles()
    {
        for (int i = 0; i < active_Tiles.Count; i++)
        {
            Destroy(active_Tiles[i]);
        }
    }

    #endregion

}
