using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Match3
{
    public class Icicle : MonoBehaviour
    {
        private Animator anim;
        private Cell _myCell;
        public Cell MyCell { get { return _myCell; } set { _myCell = value; init(); } }
        public void Clear()
        {
            anim.enabled = true;
        }
        private void init()
        {
            anim = GetComponent<Animator>();
            anim.enabled = false;
        }
    }
}
