using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Match3
{
    public abstract class State
    {
        public bool INeedMove = false;
        protected Cell Target;
        protected StateContext _context;
        protected IListener _listener;
        protected MonoBehaviour mono;
        public abstract void Start();
        public abstract void Stop();
        protected State(StateContext context , IListener listener) { _context = context; _listener = listener; INeedMove = false; }
    }

    public class Idle: State
    {
        private WaitForSeconds waitforsecond;
        public Idle(StateContext context, IListener listener, bool ineedmove = false) : base(context, listener) { INeedMove = ineedmove; }
        public override void Start()
        {
            
            _context.Anim.SetInteger("StateInt", 0);
            _context.Anim.Play("BeforeIdle", 0);
            
            _context.Mono.StartCoroutine(Wait());
        }
        public override void Stop()
        {
            _context.Mono.StopAllCoroutines();
        }
        IEnumerator Wait()
        {
            float x = Random.Range(5f, 250f);
            waitforsecond = new WaitForSeconds(x);
            yield return waitforsecond;
            _context.Anim.SetInteger("StateInt", 1);
        }


    }
    public class Fail : State
    {
        private WaitForSeconds waitforsecond;
        public Fail(StateContext context, IListener listener) : base(context, listener) { }
        public override void Start()
        {
            _context.Anim.Play("Fail", 0);
        }
        public override void Stop()
        {

        }
        IEnumerator Wait()
        {
            waitforsecond = new WaitForSeconds(_context.Anim.GetCurrentAnimatorStateInfo(0).length);
            yield return waitforsecond;
            _listener.IAmIsOver(this);
        }
    }
    public class Destruction: State
    {
        private WaitForSeconds waitforsecond;
        public Destruction(StateContext context, IListener listener) : base(context, listener) {  }
        public override void Start()
        {
            _context.Mono.StopAllCoroutines();
            _context.Anim.StopPlayback();
            _context.Anim.Play("Dead", 0);
            
            
            _context.Mono.StartCoroutine(Wait());
        }
        public override void Stop()
        {
            _context.Mono.StopAllCoroutines();
        }
        IEnumerator Wait()
        {
            waitforsecond = new WaitForSeconds(0.7f);
            yield return waitforsecond;
            foreach (SpriteRenderer sprite in _context.Sprites)
            {
                sprite.enabled = false;
            }
            _context.Dead.Play();
            _context.Sound.Play();
            _listener.IAmIsOver(this);
            waitforsecond = new WaitForSeconds(0.5f);
            yield return waitforsecond;
            _context.Mono.gameObject.SetActive(false);
            foreach (SpriteRenderer sprite in _context.Sprites)
            {
                sprite.enabled = true;
            }
            

        }

    }
    public class DestructionInATrice : State
    {
        private WaitForSeconds waitforsecond;
        public DestructionInATrice(StateContext context, IListener listener) : base(context, listener) { }
        public override void Start()
        {
            _context.Mono.StopAllCoroutines();
            _context.Anim.StopPlayback();
            _context.Mono.StartCoroutine(Wait());
        }
        public override void Stop()
        {

        }
        IEnumerator Wait()
        {
            foreach (SpriteRenderer sprite in _context.Sprites)
            {
                sprite.enabled = false;
            }
            _context.Dead.Play();
            _context.Sound.Play();
            waitforsecond = new WaitForSeconds(0.5f);
            yield return waitforsecond;
            
            _context.Mono.gameObject.SetActive(false);
            foreach (SpriteRenderer sprite in _context.Sprites)
            {
                sprite.enabled = true;
            }
            _listener.IAmIsOver(this);
        }

    }
    public class Birth: State
    {
        private WaitForSeconds waitforsecond;
        public Birth(StateContext context, IListener listener) : base(context, listener) {}
        public override void Start()
        {
           
            _context.Anim.Play("Start", 0);
           _context.Mono.StartCoroutine(Wait());
        }
        public override void Stop()
        {
            _context.Mono.StopCoroutine(Wait());
        }
        IEnumerator Wait()
        {
            waitforsecond = new WaitForSeconds(_context.Anim.GetCurrentAnimatorStateInfo(0).length);
            yield return waitforsecond;
            _listener.IAmIsOver(this);
           
        }

    }
    public class Selected: State
    {
        public Selected(StateContext context, IListener listener) : base(context, listener) { }
        public override void Start()
        {
            
            _context.Anim.Play("Press", 0);
            Up();
        }
        public override void Stop()
        {

        }
        public void Up()
        {
            foreach (SpriteRenderer sprite in _context.Sprites)
            {
                sprite.sortingOrder = sprite.sortingOrder + 5;
            }
        }
    }
    public class Unselected : State
    {
        
        public Unselected(StateContext context, IListener listener, Cell target) : base(context, listener) { Target = target; INeedMove = true; }
        public override void Start()
        {
            //_context.Mono.StopAllCoroutines();
            //INeedMove = true;
            _context.Anim.Play("UnPress", 0);
            
        }
        public override void Stop()
        {
            Down();
            
        }
        public void Down()
        {
            _context.DefoultLayers();
        }
       
    }
    public class DeathByBomb : State
    {

        public DeathByBomb(StateContext context, IListener listener, Cell target) : base(context, listener) { Target = target; }
        public override void Start()
        {
            INeedMove = true;
            Up();
            _context.Anim.Play("ExplosionEffect", 0);
            _listener.IAmIsOver(this);

        }
        public override void Stop()
        {
            Down();
        }
        public void Down()
        {
            _context.DefoultLayers();
        }
        public void Up()
        {
            foreach (SpriteRenderer sprite in _context.Sprites)
            {
                sprite.sortingOrder = sprite.sortingOrder + 5;
            }
        }

    }
}
