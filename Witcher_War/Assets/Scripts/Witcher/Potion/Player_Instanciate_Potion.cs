using UnityEngine;

public class Player_Instanciate_Potion : ICanUsePotion
{
    private const KeyCode instanciatePotionKey = KeyCode.Space;
    
    public void InstanciatePotion(GameObject potionPrefab, Vector3 position)
    {
        if (Input.GetKeyDown(instanciatePotionKey)) 
        {
            Object.Instantiate(potionPrefab, position, Quaternion.identity);
        }        
    }
}
