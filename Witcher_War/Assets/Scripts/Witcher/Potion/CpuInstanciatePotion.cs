using UnityEngine;

public class CpuInstanciatePotion : ICanUsePotion
{
    const float minTimeToSpawnPotion = 3.5f;
    const float maxTimeToSpawnPotion = 6.5f; 

    float timer = 0f;

    Cpu_Movement cpuMovement;

    public CpuInstanciatePotion(Cpu_Movement cpuMovement) 
    {
        this.cpuMovement = cpuMovement;

        RandomTime();        
    }

    public void InstanciatePotion(GameObject potionPrefab, Vector2 instantiatorIndex, WITCHER_DIRECTION direction) 
    {
        if (timer > 0f) 
        {
            timer -= Time.deltaTime;
        }
        
        if (!cpuMovement.IsObjectMoving() && timer <= 0f) 
        {
            Vector2 targetIndex = PlayerInstanciatePotion.GetIndexWhereWitcherIsLooking(instantiatorIndex, direction);
            Vector2 moveIndex = GetOpositeTileIndex(instantiatorIndex, direction);

            if (TileMap.IsTileEmpty(targetIndex) && TileMap.IsTileEmpty(moveIndex))
            {
                GameObject potion = Object.Instantiate(potionPrefab);

                potion.GetComponent<StaticGameObject>().InitialPosIndex = targetIndex;
                potion.GetComponent<PotionExplotion>().ExplosionIndex = targetIndex;                

                direction = GetOpositeTileDirection(direction);
                cpuMovement.PotionInstanciated(moveIndex, direction);
            }

            RandomTime();
        }        
    }

    public static Vector2 GetOpositeTileIndex(Vector2 witcherIndex, WITCHER_DIRECTION witcherDirection) 
    {
        switch (witcherDirection)
        {
            case WITCHER_DIRECTION.DOWN:

                return witcherIndex - Vector2.up;
            case WITCHER_DIRECTION.UP:

                return witcherIndex + Vector2.up;
            case WITCHER_DIRECTION.RIGHT:

                return witcherIndex - Vector2.right;
            case WITCHER_DIRECTION.LEFT:

                return witcherIndex + Vector2.right;
            default:

                return witcherIndex - Vector2.up;
        }
    }

    public static WITCHER_DIRECTION GetOpositeTileDirection(WITCHER_DIRECTION witcherDirection)
    {
        switch (witcherDirection)
        {
            case WITCHER_DIRECTION.DOWN:

                return WITCHER_DIRECTION.UP;
            case WITCHER_DIRECTION.UP:

                return WITCHER_DIRECTION.DOWN;
            case WITCHER_DIRECTION.RIGHT:

                return WITCHER_DIRECTION.LEFT;
            case WITCHER_DIRECTION.LEFT:

                return WITCHER_DIRECTION.RIGHT;
            default:

                return WITCHER_DIRECTION.UP;
        }
    }

    private void RandomTime() 
    {
        timer = Random.Range(minTimeToSpawnPotion, maxTimeToSpawnPotion);
    }
}
