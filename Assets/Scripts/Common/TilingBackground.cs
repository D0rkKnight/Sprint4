using System.Diagnostics;
using System.Collections.Generic;
using System;
using UnityEngine;
public class TilingBackground : MonoBehaviour
{
    public GameObject tilePrefab;
    public bool tileX = true;
    public bool tileY = true;

    private Transform backgroundContainer;

    private void Start()
    {
        backgroundContainer = transform;
        GenerateTiles();
    }

    private void Update()
    {
        GenerateTiles();
    }

    private void GenerateTiles()
    {
        if (!tileX && !tileY)
            return;

        // Get camera's extents
        Vector3 camBL = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 camTR = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Vector2 tilePeriod = tilePrefab.GetComponent<SpriteRenderer>().bounds.size;

        int rows = 1;
        int cols = 1;

        Vector2 ptr = backgroundContainer.transform.position;

        if (tileX)
        {
            while (ptr.x < camBL.x)
                ptr.x += tilePeriod.x;
            while (ptr.x > camBL.x)
                ptr.x -= tilePeriod.x;

            cols = Mathf.CeilToInt((camTR.x - ptr.x) / tilePeriod.x) + 1;
        }

        if (tileY)
        {
            while (ptr.y < camBL.y)
                ptr.y += tilePeriod.y;
            while (ptr.y > camBL.y)
                ptr.y -= tilePeriod.y;

            rows = Mathf.CeilToInt((camTR.y - ptr.y) / tilePeriod.y) + 1;
        }


        for (int i = 0; i < rows; i++)
        {
            Vector2 rowPtr = ptr;

            for (int j = 0; j < cols; j++)
            {
                if (!TileExistsAtPosition(rowPtr))
                {
                    InstantiateTile(backgroundContainer, rowPtr);
                }

                rowPtr.x += tilePeriod.x;
            }

            ptr.y += tilePeriod.y;
        }
    }

    private bool TileExistsAtPosition(Vector3 position)
    {
        foreach (Transform child in backgroundContainer)
        {
            if (Vector3.Distance(position, child.position) < 0.0001f && child.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    private void InstantiateTile(Transform container, Vector3 npos)
    {
        GameObject tile = Instantiate(tilePrefab, container);
        tile.transform.position = npos;
        tile.SetActive(true);
    }

    private void DestroyTiles()
    {
        List<GameObject> tilesToDestroy = new List<GameObject>();

        foreach (Transform child in backgroundContainer)
        {
            if (child.gameObject.activeSelf)
            {
                tilesToDestroy.Add(child.gameObject);
            }
        }

        for (int i = 0; i < tilesToDestroy.Count; i++)
        {
            DestroyImmediate(tilesToDestroy[i]);
        }
    }

    public void Redraw()
    {
        DestroyTiles();
        GenerateTiles();
    }
}
