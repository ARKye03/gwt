using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name { get; set; }
    public List<Card> hand = new();
}
