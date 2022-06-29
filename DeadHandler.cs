using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class DeadHandler 
    {
        public event ICommandHandler.MessageDelegate Dead;
       
        public event Handler CurrentTarget;
        public event Dead DeadTile;

        public DeadHandler(ICommandHandler handler)
        {
            handler.Dead += GetInfoAboutDead;
            

        }
        public void GetInfoAboutDead(Tile tile)
        {
            if(Dead != null) Dead(tile);
            if (CurrentTarget != null) CurrentTarget(1);
            if (DeadTile != null) DeadTile(tile);
        }
        
    }
}