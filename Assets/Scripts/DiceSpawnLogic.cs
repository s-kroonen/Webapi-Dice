using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiceSpawnLogic : MonoBehaviour
{
    public GameObject[] dices; // List of dice prefabs
    private List<GameObject> diceList = new List<GameObject>(); // Spawned dice list
    private Dictionary<GameObject, Object2D> spawnedDice = new Dictionary<GameObject, Object2D>(); // Tracking spawned objects

    public DiceApp diceApp; // Reference to DiceApp for API calls

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            foreach (var d in dices)
            {
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

    private bool IsDiceAtPosition(Vector3 position)
    {
        foreach (var dice in diceList)
        {
            if (Vector2.Distance(dice.transform.position, position) < 0.5f)
            {
                return true;
            }
        }
        return false;
    }

    public void CreateDice(string dice, Vector3 position)
    {
        GameObject dicePrefab = Resources.Load<GameObject>("Prefabs/" + dice);

        if (dicePrefab == null)
        {
            Debug.LogError($"Prefab 'Prefabs/{dice}' not found in Resources!");
            return;
        }

        GameObject newDice = Instantiate(dicePrefab, position, Quaternion.identity);

        if (newDice != null)
        {
            diceList.Add(newDice);

            Draggable draggable = newDice.GetComponent<Draggable>();
            if (draggable != null)
            {
                draggable.OnDragEnd += () => OnDiceMoved(newDice); // Listen for drag-end
                draggable.StartDragging();
            }
            float posX = position.x;
            float posY = position.y;

            // Create Object2D and store in dictionary
            Object2D object2D = new Object2D(diceApp.currentEnvId, dice, posX, posY);
            spawnedDice[newDice] = object2D;

            // Save to database
            diceApp.CreateObject2D(object2D);

        }
    }

    private void OnDiceMoved(GameObject diceObject)
    {
        if (spawnedDice.TryGetValue(diceObject, out Object2D object2D))
        {
            // Update Object2D position
            object2D.positionX = diceObject.transform.position.x;
            object2D.positionY = diceObject.transform.position.y;

            // Send update request
            diceApp.UpdateObject2D(object2D);
        }
        else
        {
            Debug.LogError("No Object2D found for this dice!");
        }
    }

    public void loadDice(string dice, Vector2 position, Object2D object2D)
    {
        GameObject dicePrefab = Resources.Load<GameObject>("Prefabs/" + dice);

        if (dicePrefab == null)
        {
            Debug.LogError($"Prefab 'Prefabs/{dice}' not found in Resources!");
            return;
        }

        GameObject newDice = Instantiate(dicePrefab, position, Quaternion.identity);

        if (newDice != null)
        {
            diceList.Add(newDice);

            Draggable draggable = newDice.GetComponent<Draggable>();
            if (draggable != null)
            {
                draggable.OnDragEnd += () => OnDiceMoved(newDice); // Listen for drag-end
            }
            float posX = position.x;
            float posY = position.y;

            // Create Object2D and store in dictionary
            spawnedDice[newDice] = object2D;


        }
    }
}
