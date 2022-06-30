using UnityEngine;
using WizardWar.Enums;
using WizardWar.GameplayObjects;
using WizardWar.Tile;
using WizardWar.Witcher.Interfaces;
using WizardWar.Witcher.RotationFuncs;

namespace WizardWar
{
    namespace Witcher
    {
        namespace Movement
        {
            public class PlayerMovement : BasedMovement, IMovable
            {
                private short _freezePos;               

                private Index2 _movementAxis;                

                public PlayerMovement() : base()
                {
                    _freezePos = 0;                                   
                }

                public void MoveInput()
                {
                    _movementAxis.X = (short)Input.GetAxisRaw("Horizontal");

                    if (_movementAxis.X == 0) 
                    {
                        _movementAxis.Y = (short)Input.GetAxisRaw("Vertical");

                        _movementAxis.Y *= -1;
                    }                   

                    _freezePos = (short)Input.GetAxisRaw("Freeze");
                }                                 

                public void Move(GameObject witcher, ref WitcherLookingDirection direction)
                {
                    if (_freezePos != 0) 
                    {
                        Rotation.RotateWitcher(witcher, _movementAxis, ref direction);

                        _movementAxis = Index2.IndexZero;
                    }                    

                    MoveFromCurrentTileToNewTile(witcher, _movementAxis, ref direction);
                }   

                public void Timer() 
                {
                    UpdateMovementTimer();                       
                }                
            }
        }
    }
}