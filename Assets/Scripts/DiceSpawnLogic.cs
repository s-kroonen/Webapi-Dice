using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiceSpawnLogic : MonoBehaviour
{
    public GameObject dice1;
    public GameObject dice2;
    public GameObject dice3;
    public GameObject dice4;
    public GameObject dice5;
    public GameObject dice6;
    public GameObject[] dices;
    private List<GameObject> diceList = new List<GameObject>();
    public DiceApp diceApp;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (var d in dices)
            {
                Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (d.GetComponent<Collider2D>().OverlapPoint(point))
                {
                    string dice = d.name;
                    if (!IsDiceAtPosition(point))
                    {
                        CreateDice(dice, point);
                    }
                    else
                    {
                        Debug.Log("A dice already exists at this position. No new dice spawned.");
                    }
                }
            }
        }
    }
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    bool IsDiceAtPosition(Vector3 position)
    {
        foreach (var dice in diceList)
        {
            if (Vector2.Distance(dice.transform.position, position) < 0.5f) // Adjust distance as needed
            {
                return true;
            }
        }
        return false;
    }
    void CreateDice(string dice, Vector3 position)
    {
        GameObject dicePrefab = Resources.Load<GameObject>("Prefabs/"+dice);

        if (dicePrefab == null)
        {
            Debug.LogError($"Prefab Prefabs/'{dice}' not found in Resources!");
            return;
        }

        GameObject newDice = Instantiate(dicePrefab, position, Quaternion.identity);

        if (newDice != null)
        {
            diceList.Add(newDice);

            Draggable draggable = newDice.GetComponent<Draggable>();
            if (draggable != null)
            {
                draggable.StartDragging();
            }

            // Convert position to integer values
            int posX = Mathf.RoundToInt(position.x);
            int posY = Mathf.RoundToInt(position.y);

            // Call method to save the dice in 2D format
            diceApp.CreateObject2D(dice, posX, posY);
        }
    }

}
