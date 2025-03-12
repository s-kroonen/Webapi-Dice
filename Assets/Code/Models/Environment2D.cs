using System;
using UnityEngine;


/**
 * Bijzonderheden wegens beperkingen van JsonUtility:
 * - Models hebben variabelen met kleine letters omdat JsonUtility anders de velden uit de JSON niet correct overzet naar het C# object.
 * - De id is een string in plaats van een Guid omdat JsonUtility Guid niet ondersteunt. Gelukkig geeft dit geen probleem indien we gewoon een string gebruiken in Unity en een Guid in onze backend API.
*/
[Serializable]
public class Environment2D
{
    public string id;

    public string name;
    public Environment2D(string name)
    {
        this.name = name;
    }
}
