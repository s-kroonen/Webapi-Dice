using System.Collections.Generic;
using UnityEngine;

public class GameScreenLogic : MonoBehaviour
{

    [Header("Object2D")]
    public DiceSpawnLogic spawnLogic;
    public List<Object2D> object2Ds;
    [Header("Dependencies")]
    public DiceApp diceApp;

    public void loadObjects()
    {
        object2Ds.ForEach(object2D =>
        {
            Vector2 position = new Vector2(object2D.positionX, object2D.positionY);
            spawnLogic.loadDice(object2D.prefabId, position, object2D);
        });
    }
    public void OnBackButtonPressed()
    {
        spawnLogic.destroyAllDice();
        diceApp.screenLogic.envActive();
    }
}
