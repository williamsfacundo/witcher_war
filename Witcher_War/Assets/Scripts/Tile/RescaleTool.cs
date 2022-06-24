using UnityEngine;

namespace WizardWar 
{
    namespace Tile 
    {
        public static class RescaleTool
        {
            public static void RescaleGameObject(GameObject gameObject, Vector3 size)
            {
                Vector3 gameObjectSize = gameObject.GetComponent<Renderer>().bounds.size;

                float xScale = gameObject.transform.localScale.x * (size.x / gameObjectSize.x);
                float yScale = gameObject.transform.localScale.y * (size.y / gameObjectSize.y);
                float zScale = gameObject.transform.localScale.z * (size.z / gameObjectSize.z);

                gameObject.transform.localScale = new Vector3(xScale, yScale, zScale);
            }

            public static void RescaleGameObjectBasedOnPercentageSize(GameObject gameObject, float percentage, float tileSizeAxis)
            {
                Vector3 size = Vector3.one * tileSizeAxis * percentage;

                if (gameObject.GetComponent<Renderer>() == null)
                {
                    GameObject child;

                    for (short i = 0; i < gameObject.transform.childCount; i++)
                    {
                        child = gameObject.transform.GetChild(i).gameObject;

                        if (child.GetComponent<Renderer>() != null)
                        {
                            RescaleGameObject(child, size);
                        }
                    }
                }
                else
                {
                    RescaleGameObject(gameObject, size);
                }
            }
        }
    }
}