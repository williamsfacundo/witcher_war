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

    public void InstanciatePotion(GameObject potionPrefab, Vector2 instantiatorIndex, WITCHER_DIRECTION direction)
    {
        if (Input.GetKeyDown(instanciatePotionKey) && amountPotions > 0) 
        {
            Vector2 targetIndex = GetIndexWherePlayerIsLooking(instantiatorIndex, direction);

            if (Tile_Map.IsTileEmpty(targetIndex)) 
            {
                GameObject potion = Object.Instantiate(potionPrefab);

                potion.GetComponent<StaticGameObject>().InitialPosIndex = targetIndex;
                potion.GetComponent<Potion_Explotion>().ExplosionIndex = targetIndex;

                amountPotions--;
            }            
        }

        PotionRegeneration();
    }

    private Vector2 GetIndexWherePlayerIsLooking(Vector2 index, WITCHER_DIRECTION playerDirection) 
    {
        switch (playerDirection) 
        {
            case WITCHER_DIRECTION.DOWN:

                return index + Vector2.up;
                
            case WITCHER_DIRECTION.UP:

                return index - Vector2.up;

            case WITCHER_DIRECTION.RIGHT:

                return index + Vector2.right;
            case WITCHER_DIRECTION.LEFT:

                return index - Vector2.right;
            default:

                return index + Vector2.up;
        }
    }

    private void PotionRegeneration() 
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
