using UnityEngine;
using WizardWar.Tile;
using WizardWar.Enums;

namespace WizardWar 
{
    namespace Witcher 
    {
        namespace RotationFuncs 
        {
            public static class Rotation
            {
                public static void RotateWitcher(GameObject witcher, Index2 rotationAxis, ref WitcherLookingDirection witcherDirection)
                {
                    if (rotationAxis != Index2.IndexZero) 
                    {
                        if (rotationAxis.X > 0)
                        {
                            witcherDirection = WitcherLookingDirection.Right;
                        }
                        else if (rotationAxis.X < 0)
                        {
                            witcherDirection = WitcherLookingDirection.Left;
                        }
                        else
                        {
                            if (rotationAxis.Y > 0)
                            {
                                witcherDirection = WitcherLookingDirection.Down;
                            }
                            else
                            {
                                witcherDirection = WitcherLookingDirection.Up;
                            }
                        }

                        switch (witcherDirection)
                        {
                            case WitcherLookingDirection.Down:

                                witcher.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                break;
                            case WitcherLookingDirection.Up:

                                witcher.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                                break;

                            case WitcherLookingDirection.Right:

                                witcher.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                                break;

                            case WitcherLookingDirection.Left:

                                witcher.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                                break;
                            default:

                                witcher.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                                break;
                        }
                    }                    
                } 
                
                public static void RandomRotation(GameObject witcher, ref WitcherLookingDirection witcherDirection) 
                {
                    int aux = Random.Range(1, 5);

                    switch (aux)
                    {
                        case 1:

                            RotateWitcher(witcher, Index2.Up, ref witcherDirection);
                            break;
                        case 2:

                            RotateWitcher(witcher, -Index2.Up, ref witcherDirection);
                            break;
                        case 3:

                            RotateWitcher(witcher, Index2.Right, ref witcherDirection);
                            break;
                        case 4:

                            RotateWitcher(witcher, -Index2.Right, ref witcherDirection);
                            break;
                        default:

                            RotateWitcher(witcher, Index2.Up, ref witcherDirection);
                            break;
                    }
                }
            }
        }
    }
}