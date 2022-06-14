using UnityEngine;

public class Player_Instanciate_Potion : ICanUsePotion
{
    private const KeyCode instanciatePotionKey = KeyCode.Space;    

    float maxBombs;

    float currentBombs;

    public Player_Instanciate_Potion(short maxBombs) 
    {       
        this.maxBombs = maxBombs;
        currentBombs = maxBombs;
    }

    public void InstanciatePotion(GameObject potionPrefab, Vector3 position)
    {
        if (Input.GetKeyDown(instanciatePotionKey) && currentBombs > 0) 
        {
            Object.Instantiate(potionPrefab, position, Quaternion.identity);
            
            currentBombs--;
        }        
    }
}
