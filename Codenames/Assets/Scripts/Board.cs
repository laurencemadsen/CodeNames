using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour
{
    public static int numWords = 187;
    public GameObject cellPrefab;
    public static int SIZE = 25;
    string[] _wordList = new string[numWords];
    Card[] _board = new Card[SIZE];
    List<string> _usedWords = new List<string>();
    System.Random _rand = new System.Random();
    Map _map;
    public int redCount = 0;
    public int blueCount = 0;
    public bool gameEnded = false;


    private void loadWordList()
    {
        _wordList = System.IO.File.ReadAllLines("Assets/wordlist.txt");
    }
    public void newBoard()
    {
        if (_board[0] != null)
        {
            addUsedWords();
        }
        newCards();
    }
    private void addUsedWords()
    {
        for (int i = 0; i < SIZE; i++)
        {
            _usedWords.Add(_board[i].getWord());
        }
    }
    private string[] getNewWords()
    {
        string[] words = new string[SIZE];
        for (int i = 0; i < SIZE; i++)
        {
            string newWord = _wordList[_rand.Next(numWords)];
            while (_usedWords.Contains(newWord))
            {
                newWord = _wordList[_rand.Next(numWords)];
            }
            words[i] = newWord;
        }
        return words;
    }

    private void newCards()
    {
        string[] toAdd = getNewWords();
        for (int i = 0; i < SIZE; i++)
        {
            _board[i].setWord(toAdd[i]);
            _board[i].setCardType(_map.getType(i));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("I am alive!");
        Build();
        newCards();
        disableCards();     //Spymaster must first give a hint to their team
        
    }

    public void Build()
    {
        //string[] toAdd = getNewWords();
        //int counter = 0;
        _map = new Map(1);
        loadWordList();
        for (int i = 0; i < SIZE; i++)
        {
            GameObject newCard = Instantiate(cellPrefab, transform);
            _board[i] = newCard.GetComponent<Card>();
            //Debug.Log(_board[i]);   
        }
    }

    public void OnRevealMapClicked()
    {
        for (int i = 0; i < SIZE; i++)
        {
            _board[i].setOutlineEnabled();
        }
        Debug.Log("reveal map button pressed");
        
    }

    public void disableCards()
    {
        // Make it so the cards on the board cannot be clicked
        for (int i = 0; i < _board.Length; i++)
        {
            _board[i].disable();
        }
    }

    public void enableCards()
    {
        // Make it so the cards on the board can be clicked
        for (int i = 0; i < _board.Length; i++)
        {
            _board[i].enable();
        }
    }
    public void goToTitle(){
        SceneManager.LoadScene("TitleScreen");
    }
}
