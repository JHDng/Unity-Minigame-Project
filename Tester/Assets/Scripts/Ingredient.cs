using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Ingredient")]
public class Ingredient : ScriptableObject
{
    public Sprite sprite;
    public Sprite miniSprite;
    public int ingredientIndex;
    public int ingredientState;
    //Not Processed = 0
    //Cut = 1
    //Induction = 2
    //Stove = 3
    //Smoker = 4
    public Sprite platedIngredient;
}
