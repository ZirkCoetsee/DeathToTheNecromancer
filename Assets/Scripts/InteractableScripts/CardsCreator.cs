using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsCreator : MonoBehaviour
{
    [SerializeField] int numberOfCards = 50;

    public enum Stat { maxHealth, maxSpeed, maxDamage, exampleStat };
    public class Card
    {
        public float positiveSkillValue;
        public float negativeSkillValue;
        public Stat positiveStat;
        public Stat negativeStat;

    }
    public Card[] cards;
    Card card;

    // Start is called before the first frame update
    void Start()
    {
        cards = new Card[numberOfCards];
        GenerateCards();
        //PrintCards();
    }

    void GenerateCards()
    {
        for (int i = 0; i < numberOfCards; i++)
        {
            card = new Card();
            card.positiveSkillValue = GeneratePositiveCardValue();
            card.negativeSkillValue = GenerateNegativeCardValue();
            // Debug.Log($"count: {i}");
            card.positiveStat = GenerateCardStat();
            card.negativeStat = GenerateCardStat();
            while (card.positiveStat == card.negativeStat)
            {
                card.positiveStat = GenerateCardStat();
            }

            cards[i] = card;
        }
    }

    float GeneratePositiveCardValue()
    {
        return Mathf.Round(Random.Range(1.0f, 10.0f) * 10.0f) * 0.1f;
    }

    float GenerateNegativeCardValue()
    {
        return Mathf.Round(Random.Range(1.0f, 10.0f) * 10.0f) * 0.1f;
    }

    Stat GenerateCardStat()
    {
        int value = Random.Range(1, 4);
        Stat stat = Stat.maxHealth;
        Debug.Log($"Value generated{value}");
        switch (value)
        {
            case 1:
                // code block
                stat = Stat.maxHealth;
                break;
            case 2:
                // code block
                stat = Stat.maxDamage;
                break;
            case 3:
                // code block
                stat = Stat.maxSpeed;
                break;
            default:
                // code block
                stat = Stat.maxHealth;
                break;
        }

        return stat;
    }

    public void PrintCards()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            Debug.Log($" Card number {i} Positive Stat: {cards[i].positiveStat} = Value:{cards[i].positiveSkillValue}");
            Debug.Log($" Card number {i} Negative Stat: {cards[i].negativeStat} = Value:{cards[i].negativeSkillValue}");

        }
    }

}
