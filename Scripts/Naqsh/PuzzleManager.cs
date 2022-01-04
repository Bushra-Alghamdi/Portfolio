using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private Slot slotPrefab;
    [SerializeField] private  Piece piecePrefab;
    [SerializeField] private Transform slotParent, pieceParent;


    void start()
    {
        Spawn();
    }
    void Spawn()
    {
        var pieceSlot = slotPrefab;
        var SpawnedSlot = Instantiate(pieceSlot,slotParent.position,Quaternion.identity);
        var SpawnedPiece = Instantiate(piecePrefab, pieceParent.position, Quaternion.identity);

       


    }
}
