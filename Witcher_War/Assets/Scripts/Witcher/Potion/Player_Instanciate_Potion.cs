using UnityEngine;

public class Player_Instanciate_Potion : ICanUsePotion
{
    private const KeyCode instanciatePotionKey = KeyCode.Space;    

    float maxPotions;

    float amountPotions;

    float newPotionTime;

    float newPotionTimer;

    public Player_Instanciate_Potion(short maxBombs, float newPotionTime) 
    {       
        this.maxPotions = maxBombs;
        amountPotions = maxBombs;
        this.newPotionTime = newPotionTime;
        newPotionTimer = 0f;
    }

    public void InstanciatePotion(GameObject potionPrefab, Vector3 position)
    {
        if (Input.GetKeyDown(instanciatePotionKey) && amountPotions > 0) 
        {
            GameObject potion = Object.Instantiate(potionPrefab, position, Quaternion.identity);

            potion.transform.position = Tile_Map.GetGameObjectRightYPosition(potionPrefab);

            amountPotions--;
        }

        PotionRegeneration();
    }

    void PotionRegeneration() 
    {
        if (amountPotions < maxPotions) 
        {
            newPotionTimer += Time.deltaTime;
        }

        if (newPotionTimer >= newPotionTime) 
        {
            newPotionTimer = 0f;
            amountPotions++;
        }
    }
}
