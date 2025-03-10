using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EndlessMapGenerator : MonoBehaviour
{
    public Tilemap tilemap;             // Referenz zur Tilemap
    public TileBase groundTile;         // Das Bodentile
    public int chunkWidth = 10;         // Breite der generierten Chunks
    public int maxJumpWidth = 3;        // Maximale Breite einer L�cke
    public int maxJumpHeight = 2;       // Maximale H�he, die der Spieler springen muss
    public int minGroundLength = 2;     // Minimale Plattforml�nge
    public int maxGroundLength = 6;     // Maximale Plattforml�nge
    public Transform player;            // Referenz zum Spieler

    private Vector3Int lastGeneratedPos; // Letzte Position, an der Tiles generiert wurden

    private void Start()
    {
        lastGeneratedPos = new Vector3Int(0, -1, 0); // Anfangsposition der Map
        GenerateChunk();  // Ersten Chunk generieren
    }

    private void Update()
    {
        // Generiere einen neuen Chunk, wenn der Spieler sich dem Ende n�hert
        if (player.position.x > lastGeneratedPos.x - (chunkWidth / 2))
        {
            GenerateChunk();
        }
    }

    void GenerateChunk()
    {
        int startX = lastGeneratedPos.x + 1; // Startposition f�r den neuen Chunk
        int currentY = lastGeneratedPos.y;   // H�he des letzten Chunks

        while (startX < lastGeneratedPos.x + chunkWidth)
        {
            int groundLength = Random.Range(minGroundLength, maxGroundLength + 1); // Plattformgr��e zuf�llig bestimmen

            // Bestimme die H�he der n�chsten Plattform (sanftere Anpassung)
            int heightChange = Random.Range(-1, maxJumpHeight + 1);
            currentY += heightChange;
            currentY = Mathf.Clamp(currentY, -3, 3); // H�he begrenzen, damit Spr�nge fair bleiben

            // Erzeuge die Plattform
            for (int i = 0; i < groundLength; i++)
            {
                tilemap.SetTile(new Vector3Int(startX + i, currentY, 0), groundTile);
            }

            startX += groundLength;

            // F�ge mit einer Wahrscheinlichkeit von 50% eine Sprungl�cke hinzu
            if (Random.Range(0, 2) == 0)
            {
                startX += Random.Range(2, maxJumpWidth + 1); // �berspringe eine L�cke
            }
        }

        lastGeneratedPos = new Vector3Int(startX, currentY, 0); // Update letzte Position
    }
}
