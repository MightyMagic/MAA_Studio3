using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "Word", menuName = "Create Puzzle Word/Create New Puzzle Word")]
public class PuzzleWordSO : ScriptableObject
{
    [SerializeField] string word;
    public string Word {  get { return word; } }
}
