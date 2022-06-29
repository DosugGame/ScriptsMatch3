using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Match3
{
    

    public class Level
    {
        public int number { get; protected set; }
        public int NumberBackgroung = 1;
        public int CountTiles = 5;
        public int Width = 7;
        public int Height = 5;
        public GameMode GameMode;
        public int[] Target;
        public int AvailableNumberOfSteps = 100;
        public bool HaveIcicle = false;
        public List<Coordinate> Icicles = null;
        public List<Coordinate> DirtyCells = null;
        public bool MessageForWallVK = false;
        public Level(int number)
        {
            this.number = number;
        }
        
       
        public void AddIcicle(List<Coordinate> position)
        {
            Icicles = new List<Coordinate>();
            Icicles.AddRange(position);
            HaveIcicle = true;
        }
    }
    public class Level_1: Level
    {
        public Level_1() : base(1)
        {
            NumberBackgroung = 0;
            number = 1;
            Width = 12;
            Height = 5;
            CountTiles = 4;
            GameMode = GameMode.CollectTiles;
            Target = new int[1] {20};
            AvailableNumberOfSteps = 10;
            MessageForWallVK = true;

        }
    }
    public class Level_2 : Level
    {
        public Level_2() : base(1)
        {
            NumberBackgroung = 0;
            number = 2;
            Width = 12;
            Height = 5;
            CountTiles = 4;
            GameMode = GameMode.ClearCells;
            AvailableNumberOfSteps = 10;

            DirtyCells = new List<Coordinate>();
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    DirtyCells.Add(new Coordinate(i, j));
                }
            }
            Target = new int[1] { DirtyCells.Count }; 
        }
    }


    public class Level_3 : Level
    {
        public Level_3() : base(1)
        {
            NumberBackgroung = 0;
            number = 3;
            Width = 12;
            Height = 5;
            CountTiles = 4;
            GameMode = GameMode.DeliverTheItem;
            Target = new int[1] { 2 };
            AvailableNumberOfSteps = 15;


        }
    }
    public class Level_4 : Level
    {
        public Level_4() : base(1)
        {
            NumberBackgroung = 1;
            number = 4;
            Width = 12;
            Height = 8;
            GameMode = GameMode.CollectTiles;
            Target = new int[2] { 20,20 };
            AvailableNumberOfSteps = 7;


        }
    }
    public class Level_5 : Level
    {
        public Level_5() : base(1)
        {
            NumberBackgroung = 1;
            number = 5;
            Width = 12;
            Height = 8;
            MessageForWallVK = true;
            GameMode = GameMode.ClearCells;
            AvailableNumberOfSteps = 19;

            DirtyCells = new List<Coordinate>();
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    DirtyCells.Add(new Coordinate(i, j));
                }
            }
            Target = new int[1] { DirtyCells.Count };
        }
    }
    public class Level_6 : Level
    {
        public Level_6() : base(1)
        {
            NumberBackgroung = 1;
            number = 6;
            Width = 8;
            Height = 8;
            GameMode = GameMode.CollectTiles;
            Target = new int[2] { 20, 20 };
            AvailableNumberOfSteps = 19;
            List<Coordinate> list = new List<Coordinate>();
            for (int i = 0; i < Width; i++)
            {
                for (int j = 3; j < 5; j++)
                {
                    list.Add(new Coordinate(i, j));
                }
                    
                
            }
            AddIcicle(list);

        }
    }
    public class Level_7 : Level
    {
        public Level_7() : base(1)
        {
            NumberBackgroung = 2;
            number = 7;
            Width = 8;
            Height = 8;
            GameMode = GameMode.CollectTiles;
            Target = new int[2] { 20, 20 };
            AvailableNumberOfSteps = 16;
            List<Coordinate> list = new List<Coordinate>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 2; j < 6; j++)
                {
                    list.Add(new Coordinate(i, j));
                }


            }
            for (int i = 5; i < Width; i++)
            {
                for (int j = 2; j < 6; j++)
                {
                    list.Add(new Coordinate(i, j));
                }


            }
            AddIcicle(list);
        }
    }
    public class Level_8 : Level
    {
        public Level_8() : base(1)
        {
            NumberBackgroung = 2;
            number = 8;
            Width = 8;
            Height = 8;
            GameMode = GameMode.CollectTiles;
            Target = new int[2] { 30, 35 };
            AvailableNumberOfSteps = 21;
            List<Coordinate> list = new List<Coordinate>();
            for (int i = 0; i < Height - 2; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    list.Add(new Coordinate(j, i));
                }


            }
            
            AddIcicle(list);
        }
    }
    public class Level_9 : Level
    {
        public Level_9() : base(1)
        {
            NumberBackgroung = 2;
            number = 9;
            Width = 8;
            Height = 8;
            GameMode = GameMode.DeliverTheItem;
            Target = new int[1] {2};
            AvailableNumberOfSteps = 15;
            List<Coordinate> list = new List<Coordinate>();
            for (int i = 1; i < 4; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    list.Add(new Coordinate(j, i));
                }


            }

            AddIcicle(list);
        }
    }
    public class Level_10 : Level
    {
        public Level_10() : base(1)
        {
            NumberBackgroung = 3;
            number = 10;
                Width = 12;
                Height = 8;
                CountTiles = 6;
                GameMode = GameMode.CollectTiles;
                Target = new int[3] { 20, 20, 20 };
                AvailableNumberOfSteps = 20;
            MessageForWallVK = true;


        }
    }
    public class Level_11 : Level
    {
        public Level_11() : base(1)
        {
            NumberBackgroung = 3;
            number = 11;
            Width = 14;
            Height = 8;
            GameMode = GameMode.ClearCells;
            AvailableNumberOfSteps = 30;

            DirtyCells = new List<Coordinate>();
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    DirtyCells.Add(new Coordinate(i, j));
                }
            }
            Target = new int[1] { DirtyCells.Count };

        }
    }
    public class Level_12 : Level
    {
        public Level_12() : base(1)
        {
            NumberBackgroung = 3;
            number = 12;
            Width = 14;
            Height = 8;
            GameMode = GameMode.ClearCells;
            AvailableNumberOfSteps = 27;

            DirtyCells = new List<Coordinate>();
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    DirtyCells.Add(new Coordinate(i, j));
                }
            }
            Target = new int[1] { DirtyCells.Count };
            List<Coordinate> list = new List<Coordinate>();

            for (int j = 0; j < Width; j++)
            {
                list.Add(new Coordinate(j, 5));
            }




            AddIcicle(list);
        }
    }
    public class Level_13 : Level
    {
        public Level_13() : base(1)
        {
            NumberBackgroung = 4;
            number = 13;
            Width = 5;
            Height = 8;
            GameMode = GameMode.DeliverTheItem;
            AvailableNumberOfSteps = 21;
            CountTiles = 6;
           
            Target = new int[1] { 3 };
            List<Coordinate> list = new List<Coordinate>();
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    list.Add(new Coordinate(j, i));
                }
            }
            




            AddIcicle(list);
        }
    }
    public class Level_14 : Level
    {
        public Level_14() : base(1)
        {
            NumberBackgroung = 4;
            number = 14;
            Width = 12;
            Height = 8;
            GameMode = GameMode.CollectTiles;
            AvailableNumberOfSteps = 17;
            CountTiles = 4;

            Target = new int[1] { 150 };
           

        }
    }
    public class Level_15 : Level
    {
        public Level_15() : base(1)
        {
            NumberBackgroung = 4;
            number = 15;
            Width = 12;
            Height = 8;
            GameMode = GameMode.DeliverTheItem;
            AvailableNumberOfSteps = 32;
            CountTiles = 4;
            MessageForWallVK = true;
            Target = new int[1] { 10 };

        }
    }
    public class Level_16 : Level
    {
        public Level_16() : base(1)
        {
            NumberBackgroung = 5;
            number = 16;
            Width = 12;
            Height = 8;
            GameMode = GameMode.ClearCells;
            AvailableNumberOfSteps = 15;
            CountTiles = 4;

            DirtyCells = new List<Coordinate>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    DirtyCells.Add(new Coordinate(i, j));
                }
            }
            for (int i = 8; i < 12; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    DirtyCells.Add(new Coordinate(i, j));
                }
            }
            Target = new int[1] { DirtyCells.Count };
            List<Coordinate> list = new List<Coordinate>();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    list.Add(new Coordinate(i, j));
                }
            }
            for (int i = 8; i < 12; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    list.Add(new Coordinate(i, j));
                }
            }
            AddIcicle(list);
        }
    }
    public class Level_17 : Level
    {
        public Level_17() : base(1)
        {
            NumberBackgroung = 5;
            number = 17;
            Width = 12;
            Height = 8;
            GameMode = GameMode.CollectTiles;
            AvailableNumberOfSteps = 27;
            CountTiles = 4;
                       
            Target = new int[2] { 10,100 };
            List<Coordinate> list = new List<Coordinate>();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    list.Add(new Coordinate(i, j));
                }
            }
            for (int i = 8; i < 12; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    list.Add(new Coordinate(i, j));
                }
            }
            AddIcicle(list);
        
    }
    }
    public class Level_18 : Level
    {
        public Level_18() : base(1)
        {
            NumberBackgroung = 5;
            number = 18;
            Width = 12;
            Height = 8;
            GameMode = GameMode.DeliverTheItem;
            AvailableNumberOfSteps = 30;
            CountTiles = 6;

            Target = new int[1] { 1 };
            List<Coordinate> list = new List<Coordinate>();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    list.Add(new Coordinate(i, j));
                }
            }
            for (int i = 8; i < 12; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    list.Add(new Coordinate(i, j));
                }

            }
            for (int i = 4; i < 8; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    list.Add(new Coordinate(i, j));
                }
            }
            AddIcicle(list);
        }
    }
    public class Level_19 : Level
    {
        public Level_19() : base(1)
        {
            NumberBackgroung = 6;
            number = 19;
            Width = 9;
            Height = 9;
            GameMode = GameMode.ClearCells;
            AvailableNumberOfSteps = 25;
            CountTiles = 6;

            DirtyCells = new List<Coordinate>();
            int center = 4;
            int width = 0;
            for (int i = 0; i < 5; i++)
            {

                for (int j = center - width; j <= center + width; j++)
                {
                    DirtyCells.Add(new Coordinate(j, i));
                    
                }
                width++;

            }
            width-=2;
            for (int i = 5; i < Height; i++)
            {

                for (int j = center - width; j <= center + width; j++)
                {
                    DirtyCells.Add(new Coordinate(j, i));

                }
                width--;

            }
            Target = new int[1] { DirtyCells.Count };
            
        }
    }
    public class Level_20 : Level
    {
        public Level_20() : base(1)
        {
            NumberBackgroung = 6;
            number = 20;
            Width = 12;
            Height = 8;
            GameMode = GameMode.CollectTiles;
            AvailableNumberOfSteps = 38;
            CountTiles = 5;
            MessageForWallVK = true;
            Target = new int[3] { 50, 50, 50 };
            List<Coordinate> list = new List<Coordinate>();


            for (int i = 0; i < Height; i++)
            {
                list.Add(new Coordinate(0, i));
                list.Add(new Coordinate(Width-1, i));
            }
            for (int i = 1; i < Width-1; i++)
            {
                if(i != 4 )
                {
                    
                    list.Add(new Coordinate(i, Height - 1));
                }
                list.Add(new Coordinate(i, 0));

            }
            AddIcicle(list);
        }
    }
    public class Level_21 : Level
    {
        public Level_21() : base(1)
        {
            NumberBackgroung = 6;
            number = 21;
            Width = 13;
            Height = 9;
            GameMode = GameMode.DeliverTheItem;
            AvailableNumberOfSteps = 45;
            CountTiles = 6;
            Target = new int[1] { 1 };
            List<Coordinate> list = new List<Coordinate>();
            
            int width = 0;
            for (int i = 0; i < Height; i++)
            {

                for (int j = 0 + width; j < Width - width; j++)
                {
                    list.Add(new Coordinate(j, i));

                }
                width++;
                if (width == 8) break;

            }
            AddIcicle(list);
           
        }
    }
    public class Level_22 : Level
    {
        public Level_22() : base(1)
        {
            NumberBackgroung = 7;
            number = 22;
            Width = 14;
            Height = 5;
            GameMode = GameMode.CollectTiles;
            AvailableNumberOfSteps = 19;
            CountTiles = 5;

            Target = new int[3] { 30, 50, 30 };
           


        }
    }
    public class Level_23 : Level
    {
        public Level_23() : base(1)
        {
            NumberBackgroung = 7;
            number = 23;
            Width = 13;
            Height = 8;
            GameMode = GameMode.DeliverTheItem;
            AvailableNumberOfSteps = 43;
            CountTiles = 5;

            Target = new int[1] { 5 };
            List<Coordinate> list = new List<Coordinate>();


            for (int i = 0; i < Height; i++)
            {
                list.Add(new Coordinate(0, i));
                list.Add(new Coordinate(Width - 1, i));
            }
            for (int i = 1; i < Width - 1; i++)
            {
                
                list.Add(new Coordinate(i, 0));
                if (i > 4 && i < 8) continue;
                list.Add(new Coordinate(i, Height - 1));
            }
            
            
            AddIcicle(list);
        }
    }
    public class Level_24 : Level
    {
        public Level_24() : base(1)
        {
            NumberBackgroung = 7;
            number = 24;
            Width = 13;
            Height = 8;
            GameMode = GameMode.ClearCells;
            AvailableNumberOfSteps = 34;
            CountTiles = 4;

            Target = new int[1] { 5 };
            List<Coordinate> list = new List<Coordinate>();
            DirtyCells = new List<Coordinate>();

            int icicle = 1;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (icicle == 3) { list.Add(new Coordinate(i, j)); icicle = 1; }
                    else { DirtyCells.Add(new Coordinate(i, j)); icicle++; }
                    
                    
                    
                }
            }
            Target = new int[1] { DirtyCells.Count };


            AddIcicle(list);
        }
    }
    public class Level_25 : Level
    {
        public Level_25() : base(1)
        {
            NumberBackgroung = 8;
            number = 25;
            Width = 13;
            Height = 8;
            GameMode = GameMode.ClearCells;
            AvailableNumberOfSteps = 21;
            CountTiles = 4;
            MessageForWallVK = true;

            List<Coordinate> list = new List<Coordinate>();
            DirtyCells = new List<Coordinate>();

            int icicle = 1;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height - 1; j++)
                {
                    if (icicle == 2) { list.Add(new Coordinate(i, j)); icicle = 1; }
                    else { DirtyCells.Add(new Coordinate(i, j)); icicle++; }



                }
            }
            Target = new int[1] { DirtyCells.Count };


            AddIcicle(list);
        }
    }
    public class Level_26 : Level
    {
        public Level_26() : base(1)
        {
            NumberBackgroung = 8;
            number = 26;
            Width = 13;
            Height = 8;
            GameMode = GameMode.CollectTiles;
            AvailableNumberOfSteps = 15;
            CountTiles = 5;

            Target = new int[3] { 15, 40, 15 };
            List<Coordinate> list = new List<Coordinate>();
           

            
            for (int i= 3; i < Width-3; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                     list.Add(new Coordinate(i, j)); 
                    



                }
            }
            


            AddIcicle(list);
        }
    }
    public class Level_27 : Level
    {
        public Level_27() : base(1)
        {
            NumberBackgroung = 8;
            number = 27;
            Width = 13;
            Height = 8;
            GameMode = GameMode.DeliverTheItem;
            AvailableNumberOfSteps = 30;
            CountTiles = 4;


            List<Coordinate> list = new List<Coordinate>();
            DirtyCells = new List<Coordinate>();
            Target = new int[1] { 5 };
            int icicle = 1;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height - 1; j++)
                {
                    if (icicle == 2) { list.Add(new Coordinate(i, j)); icicle = 1; }
                    else {  icicle++; }



                }
            }
            


            AddIcicle(list);
        }
    }
    public class Level_28 : Level
    {
        public Level_28() : base(1)
        {
            NumberBackgroung = 9;
            number = 28;
            Width = 13;
            Height = 8;
            GameMode = GameMode.ClearCells;
            AvailableNumberOfSteps = 25;
            CountTiles = 5;


            List<Coordinate> list = new List<Coordinate>();
            DirtyCells = new List<Coordinate>();

            
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height ; j++)
                {
                    if(i == 0 || i == Width-1) DirtyCells.Add(new Coordinate(i, j));
                    else if(i< 5 || i > 7) list.Add(new Coordinate(i, j));
                    



                }
            }
            Target = new int[1] { DirtyCells.Count };


            AddIcicle(list);
        }
    }
    public class Level_29 : Level
    {
        public Level_29() : base(1)
        {
            NumberBackgroung = 9;
            number = 29;
            Width = 12;
            Height = 7;
            GameMode = GameMode.CollectTiles;
            AvailableNumberOfSteps = 29;
            CountTiles = 4;
            Target = new int[1] { 200 };

        }
    }
    public class Level_30 : Level
    {
        public Level_30() : base(1)
        {
            NumberBackgroung = 9;
            number = 30;
            Width = 10;
            Height = 7;
            GameMode = GameMode.CollectTiles;
            AvailableNumberOfSteps = 10;
            CountTiles = 3;
            Target = new int[3] { 200,200,200 };
            MessageForWallVK = true;
        }
    }
    public class Level_31 : Level
    {
        public Level_31() : base(1)
        {
            NumberBackgroung = 10;
            number = 31;
            Width = 13;
            Height = 8;
            GameMode = GameMode.ClearCells;
            AvailableNumberOfSteps = 26;
            CountTiles = 5;


            List<Coordinate> list = new List<Coordinate>();
            DirtyCells = new List<Coordinate>();


            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height-2; j++)
                {
                    if (j == 0) DirtyCells.Add(new Coordinate(i, j));
                    list.Add(new Coordinate(i, j));




                }
            }
            Target = new int[1] { DirtyCells.Count };


            AddIcicle(list);
        }
    }
    public class Level_32 : Level
    {
        public Level_32() : base(1)
        {
            NumberBackgroung = 10;
            number = 32;
            Width = 13;
            Height = 8;
            GameMode = GameMode.ClearCells;
            AvailableNumberOfSteps = 20;
            CountTiles = 6;


            List<Coordinate> list = new List<Coordinate>();
            DirtyCells = new List<Coordinate>();


            for (int i = 2; i < Width-2; i++)
            {
                for (int j = 1; j < Height - 2; j++)
                {
                    
                    list.Add(new Coordinate(i, j));




                }
            }
            DirtyCells.Add(new Coordinate(6, 3));
            Target = new int[1] { DirtyCells.Count };

            
            AddIcicle(list);
        }
    }
    public class Level_33 : Level
    {
        public Level_33() : base(1)
        {

        }
    }
    public class Level_34 : Level
    {
        public Level_34() : base(1)
        {

        }
    }
    public class Level_35 : Level
    {
        public Level_35() : base(1)
        {

        }
    }
    public class Level_36 : Level
    {
        public Level_36() : base(1)
        {

        }
    }
    public class Level_37 : Level
    {
        public Level_37() : base(1)
        {

        }
    }
    public class Level_38 : Level
    {
        public Level_38() : base(1)
        {

        }
    }
    public class Level_39 : Level
    {
        public Level_39() : base(1)
        {

        }
    }
    public class Level_40 : Level
    {
        public Level_40() : base(1)
        {

        }
    }
    public class Level_41 : Level
    {
        public Level_41() : base(1)
        {

        }
    }
    public class Level_42 : Level
    {
        public Level_42() : base(1)
        {

        }
    }
    public class Level_43 : Level
    {
        public Level_43() : base(1)
        {

        }
    }
    public class Level_44 : Level
    {
        public Level_44() : base(1)
        {

        }
    }
    public class Level_45 : Level
    {
        public Level_45() : base(1)
        {

        }
    }
    public class Level_46 : Level
    {
        public Level_46() : base(1)
        {

        }
    }
    public class Level_47 : Level
    {
        public Level_47() : base(1)
        {

        }
    }
    public class Level_48 : Level
    {
        public Level_48() : base(1)
        {

        }
    }
    public class Level_49 : Level
    {
        public Level_49() : base(1)
        {

        }
    }
    public class Level_50 : Level
    {
        public Level_50() : base(1)
        {

        }
    }
}
