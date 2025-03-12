using System;
using UnityEngine;

/**
 * Bijzonderheden wegens beperkingen van JsonUtility:
 * - Models hebben variabelen met kleine letters omdat JsonUtility anders de velden uit de JSON niet correct overzet naar het C# object.
 * - De id is een string in plaats van een Guid omdat JsonUtility Guid niet ondersteunt. Gelukkig geeft dit geen probleem indien we gewoon een string gebruiken in Unity en een Guid in onze backend API.
*/
[Serializable]
public class Object2D
{
    public string id;

    public string environmentId;

    public string prefabId;

    public float positionX;

    public float positionY;

    public float scaleX;

    public float scaleY;

    public Object2D(string environmentId, string prefabId, float positionX, float positionY)
    {
        this.environmentId = environmentId;
        this.prefabId = prefabId;
        this.positionX = positionX;
        this.positionY = positionY;
        this.scaleX = 1;
        this.scaleY = 1;
    }
}