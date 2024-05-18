using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient Holder", menuName = "Ingredient Holder")]
public class IngredientHolder : ScriptableObject
{
    public Sprite sprite;
    public Ingredient[] ingredients;
    
    //Not Processed = 0
    //Cut = 1
    //Induction = 2
    //Stove = 3
    //Smoker = 4
}
