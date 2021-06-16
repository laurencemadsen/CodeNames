using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static addPlayers;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;

    void Awake ()   {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy (gameObject);
        }
    }

    public const int MAX_PLAYERS = 8;   // max players allowed in a game
    public createPlayer[] players = new createPlayer[MAX_PLAYERS];    // create array of 6 players
    public string winner; 
}
