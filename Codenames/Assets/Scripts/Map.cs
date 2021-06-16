using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Map
{

    int[] _characters = { 0,0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 3};
    System.Random _rand = new System.Random();

    public Map(int goesFirst)
    {
        _characters[0] = goesFirst;
        Shuffle();
    }
    public void Shuffle()
    {
        foreach (int num in _characters)
        {
            Debug.Log(num);
        }
        int n = _characters.Length;
        int value;
        int k;
        while (n > 1)
        {
            n--;
            k = _rand.Next(n + 1);
            value = _characters[k];
            _characters[k] = _characters[n];
            _characters[n] = value;
        }
        Debug.Log(_characters);
    }
    
    public Card.CardType getType(int index)
    {
        return (Card.CardType) _characters[index];
    }
}


