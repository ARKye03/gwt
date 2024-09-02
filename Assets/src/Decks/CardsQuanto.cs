using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsQuanto : MonoBehaviour
{
    // needs to be a singleton <= idk wtf is this
    // Edit:
    /*
     * Singleton is a creational design pattern that lets you ensure that a class has only one instance, while providing a global access point to this instance.
     */
    private readonly List<Card> CardsOfIdanai = new()
    {

    };
    private readonly List<Card> CardsOfCelai = new()
    {
    };
    private readonly List<Card> CardsOfYudivain = new()
    {
    };
}
