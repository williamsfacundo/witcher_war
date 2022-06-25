namespace WizardWar
{
    namespace Tile 
    {
        public class Index2
        {
            private short _x;
            private short _y;            

            public short X
            {
                set
                {
                    _x = value;
                }

                get
                {
                    return _x;
                }
            }

            public short Y
            {
                set
                {
                    _y = value;
                }
                get
                {
                    return _y;
                }
            }           
            
            public static Index2 IndexNull 
            {
                get 
                {
                    return new Index2(-1, -1);
                }
            }

            public static Index2 Right 
            {
                get 
                {
                    return new Index2(1, 0);
                }
            }

            public static Index2 Up 
            {
                get 
                {
                    return new Index2(0, 1);
                }
            }

            public Index2() 
            {
                _x = 0;
                _y = 0;
            }

            public Index2(short x, short y) 
            {
                _x = x;
                _y = y;
            }           

            public static Index2 operator +(Index2 index1, Index2 index2)
            {
                return new Index2((short)(index1.X + index2.X), (short)(index1.Y + index2.Y));
            }

            public static Index2 operator -(Index2 index1, Index2 index2)
            {
                return new Index2((short)(index1.X - index2.X), (short)(index1.Y - index2.Y));
            }

            public override bool Equals(object obj)
            {
                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public static bool operator == (Index2 index1, Index2 index2)
            {                
                return index1.X == index2.X && index1.Y == index2.Y;
            }

            public static bool operator != (Index2 index1, Index2 index2) 
            {
                return index1.X != index2.X && index1.Y != index2.Y;
            }
        }        
    }
}