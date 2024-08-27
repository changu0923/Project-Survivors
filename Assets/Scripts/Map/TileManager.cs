using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public Vector2 tileSize = new(3, 3); 
    public Transform tileParent;
    public int generateCellSize = 3; 
    public int borderDistance = 2;                               
    public int destroyDistance = 5; 
    public Transform playerTransform;
    public List<Vector2Int> tiledCell;
    public List<GameObject> tiledObjects; 
    Vector2Int[] directionCell;

    private void Start()
    {
        tileParent = new GameObject("Tiles").transform;
        tileParent.parent = transform;

        directionCell = new Vector2Int[] {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right};

        playerTransform = GameManager.Instance.Player.transform;
        TileGenerate(Vector2.zero);
    }

    public void TileGenerate(Vector2 center)
    {
        TileGenerate(Vector2Int.FloorToInt(center / tileSize));
    }

    public void TileGenerate(Vector2Int center)
    {
        for (int x = center.x - generateCellSize; x <= center.x + generateCellSize; x++)
        {
            for (int y = center.y - generateCellSize; y <= center.y + generateCellSize; y++)
            { 
                Vector2Int targetCell = new Vector2Int(x, y);

                if (tiledCell.Contains(targetCell))
                {
                    continue;
                }
                Vector2 targetPosition = new Vector2(x, y) * tileSize;
                int tileNum = Random.Range(0, tilePrefabs.Length);
                GameObject targetPrefab = tilePrefabs[tileNum];
                GameObject tileObject = Instantiate(targetPrefab, targetPosition, Quaternion.identity);
                tiledCell.Add(targetCell);
                tiledObjects.Add(tileObject);

                tileObject.transform.parent = tileParent;
            }
        }
    }

    private void TileDestroy(Vector2Int centerCell)
    {
        List<GameObject> destoryList = new List<GameObject>();
        foreach (GameObject tile in tiledObjects)
        {
            Vector2Int targetCell = PosToCell(tile.transform.position);
            Vector2Int sqr = centerCell - targetCell;
            if (Mathf.Abs(sqr.x) >= destroyDistance || Mathf.Abs(sqr.y) >= destroyDistance)
            {
                destoryList.Add(tile);
                tiledCell.Remove(targetCell);
            }
        }

        foreach (GameObject tile in destoryList)
        {
            tiledObjects.Remove(tile);
            Destroy(tile);
        }
    }

    Vector2Int PosToCell(Vector2 pos)
    {
        return Vector2Int.RoundToInt(pos / tileSize);
    }

    private void Update()
    {
        if (playerTransform == null) return;
        Vector2Int playerCell = PosToCell(playerTransform.position);
        bool isBorder = false;
        Vector2Int borderCell = playerCell * borderDistance;
        foreach (Vector2Int dir in directionCell)
        {
            if (false == tiledCell.Contains(playerCell + (dir * borderDistance)))
            {
                isBorder = true;
                break;
            }
        }

        if (isBorder)
        {
            TileGenerate(playerCell);
            TileDestroy(playerCell);
        }

    }

}
