using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Ingredient")]
public class Ingredient : ScriptableObject
{
    public Sprite sprite;
    public int ingredientIndex;
    public int ingredientState;
    public int ingredientFinalState;
}
