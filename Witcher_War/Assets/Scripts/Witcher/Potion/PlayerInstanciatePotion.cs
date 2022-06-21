using UnityEngine;

public class PlayerInstanciatePotion : ICanUsePotion
{
    private const KeyCode instanciatePotionKey = KeyCode.Space;    

    const float maxPotions = 2;

    const float newPotionTime = 1.5f;

    float amountPotions;    

    float newPotionTimer;    

    public PlayerInstanciatePotion() 
    {       
        amountPotions = maxPotions;
        
        newPotionTimer = 0f;        
    }

    public void InstanciatePotion(GameObject potionPrefab, Vector2 instantiatorIndex, WITCHER_DIRECTION direction)
    {
        if (Input.GetKeyDown(instanciatePotionKey) && amountPotions > 0) 
        {
            Vector2 targetIndex = GetIndexWhereWitcherIsLooking(instantiatorIndex, direction);

            if (TileMap.IsTileEmpty(targetIndex)) 
            {
                GameObject potion = Object.Instantiate(potionPrefab);

                potion.GetComponent<StaticGameObject>().InitialPosIndex = targetIndex;
                potion.GetComponent<PotionExplotion>().ExplosionIndex = targetIndex;

                amountPotions--;
            }            
        }

        PotionRegeneration();
    }

    public static Vector2 GetIndexWhereWitcherIsLooking(Vector2 index, WITCHER_DIRECTION witcherDirection) 
    {
        switch (witcherDirection) 
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
