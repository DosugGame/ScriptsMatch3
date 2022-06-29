using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using System.Reflection;
using System;


namespace Match3
{
    public enum DeathOption
    {
        Normal,
        InATrice
    }
    public interface IDestroyer
    {
       public void Destroy<T>(List<T> colections) where T : IDestroyable;
        public void Destroy<T>(T tile) where T : IDestroyable;
        public void Destroy<T>(List<T> colections, IPool<T> pool);
        public void Destroy<T>(T obj, IPool<T> pool, DeathOption variant = DeathOption.Normal);
    }
    public class Destroyer: IDestroyer
    {
        public void Destroy<T>(List<T> colections) where T : IDestroyable
        {
            foreach (T obj in colections)
            {
                if (!HaveBonusCheck(obj)) obj.Destroy();
            }
        }
       
        public void Destroy<T>(List<T> colections, IPool<T> pool)
        {

            if (pool != null)
            {
                foreach (T obj in colections)
                {
                    if ( !HaveBonusCheck(obj)) pool.PutIntoThePool(obj);
                }
            } 
            foreach (IDestroyable obj in colections)
            {
               
                IDestroyable dest = obj as IDestroyable;
                dest.Destroy();
            }
        }
        public void Destroy<T>(T tile) where T : IDestroyable
        {
            tile.Destroy();
        }

        public void Destroy<T>(T obj, IPool<T> pool, DeathOption variant = DeathOption.Normal)
        {

            if (pool != null && !HaveBonusCheck(obj))
            {
                pool.PutIntoThePool(obj);
            }
            if (variant == DeathOption.InATrice && obj is IDestroyableInATrice)
            {
                IDestroyableInATrice dest = obj as IDestroyableInATrice;
                dest.DestroyInATrice();
            }
            else
            { 
                IDestroyable dest = obj as IDestroyable;
                dest.Destroy();
            }
          
            
            
        }
        private bool HaveBonusCheck<T>(T forcheck)
        {
            bool havebonus = false;
            if (forcheck is IBonusItem) havebonus = true;
            return havebonus;
        }
    }
}