using UnityEngine;
using WizardWar.Tile;
using System.IO;

namespace WizardWar 
{
    namespace TileObjects 
    {
        public static class MapReader
        {
            private const char lineBreakCharOne = (char)13;
            private const char lineBreakCharTwo = (char)10;

            public static char[] GetMapArrayChar(short level, short tileMapMaxRows)
            {
                FileStream fs = File.OpenRead(GetMapPath(level));               

                StreamReader sr = new StreamReader(fs);

                char[] map = sr.ReadToEnd().ToCharArray();               

                sr.Close();
                fs.Close();

                return ReturnArrayWithOutLineBreaks(map, tileMapMaxRows);
            }

            public static Index2 ConvertArrayIndexIntoArray2DIndex(int arrayIndex, short tileMapMaxRows)
            {
                Index2 index = new Index2(0, 0);

                for (short i = 0; i < arrayIndex; i++)
                {
                    index.X++;

                    if (index.X == tileMapMaxRows - 1 && i + 1 < arrayIndex)
                    {
                        index.X = -1;
                        index.Y++;
                    }
                }

                return index;
            }

            private static char[] ReturnArrayWithOutLineBreaks(char[] map, short tileMapMaxRows) 
            {
                char[] mapWithOutLineBreaks = new char[map.Length - ((tileMapMaxRows - 1) * 2)];

                int auxIndex = 0;

                for (short i = 0; i < map.Length; i++)
                {
                    if (map[i] != lineBreakCharOne && map[i] != lineBreakCharTwo)
                    {
                        mapWithOutLineBreaks[auxIndex] = map[i];

                        auxIndex++;
                    }
                }

                return mapWithOutLineBreaks;
            }

            private static string GetMapPath(short level)
            {
                return Application.dataPath + "/Maps_Files/map_level_" + level.ToString() + ".txt";
            }
        }
    }
}