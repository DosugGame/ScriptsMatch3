using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Match3 {

    public interface IDestroyable
    {
        public void Destroy();
    }
    public interface IDestroyableInATrice
    {
        public void DestroyInATrice();

    }
    public interface ICanBeAnIcicle
    {
        public void TileIsIcicle();
        public void TileIsNotIcicle();
    }
    public class Tile : MonoBehaviour, IDestroyable, IDestroyableInATrice, ICanBeAnIcicle
    {
        [SerializeField] protected Color _myColor;
        public bool IFree { get; protected set; } = true;
        protected int _iD;
        public virtual  int ID { get => _iD; set { if (!IAmActive) { _iD = value; } } }
        protected Animator _anim;
        protected float _distance;
        public Cell _myCell;
        public bool IAmActive { get; protected set; } = false;
        public  Transform _transform { get; protected set; }
        protected float _speed;
        protected IListenerTiles _messenger;
        protected StateMachine _state;
        protected WaitForSeconds _waitBeforeDestroy = new WaitForSeconds(0.2f);
        protected ParticleSystem FXDead;

        
        
        protected virtual  void FixedUpdate()
        {
            
            
                if (_state.INeedMove && _state.TargetCell.position != null)
                {
                
                    _transform.position = Vector3.MoveTowards(_transform.position, _state.TargetCell.position, Time.fixedDeltaTime * _speed);
                    float dis = Vector3.Distance(_transform.position, _state.TargetCell.position);
                    if (dis < 0.001f) {   IAmInPosition();  }
                }
            
            
           
        }
        
         public virtual void Destroy()
        {

            _state.Switch<Destruction>();

        }
        public void TileIsIcicle()
        {
            IFree = false;
        }
        public void TileIsNotIcicle()
        {
            IFree = true;
        }
        public virtual void ExplosionEffect(Coordinate direction, float power)
        {
            Vector3 targetposition = new Vector3(_myCell.position.x + direction.x * power, _myCell.position.y + direction.y * power, 0);
            Cell target = new Cell(_myCell.x, _myCell.y, targetposition);
            _state.Switch<DeathByBomb>(target);
            _speed /= 3;
        }
         
        public virtual void DestroyInATrice()
        {
            _state.Switch<DestructionInATrice>();
        }

        private void OnMouseDown()
        {
            if(IFree) _messenger.Message(MessengerTiles.Messages.IAmpressed, this);
            else _state.Switch<Fail>();

        }
        public virtual void MoveToCell(Cell target, Movement movement = Movement.NewCell)
        {
            switch (movement)
            {

                case Movement.NewCell:
                    _state.Switch<Unselected>(target);
                    
                    break;
                case Movement.Home:
                    _state.Switch<Unselected>(_myCell);
                    break;
               
                case Movement.Fall:
                    _state.Switch<Unselected>(target);
                    break;
                default:
                    break;
            }
            
        }

        private void OnMouseUp()
        {
            _messenger.Message(MessengerTiles.Messages.IAmReleased, this); 
            
        }
        public virtual void Stop()
        {
            _state.Switch<Fail>();
            StopAllCoroutines();
            gameObject.SetActive(false);
        }
        protected  virtual void IAmInPosition()
        {
            if (_state.IMustDie) { _state.Switch<DestructionInATrice>(); _speed *= 3; }
            else
            {
                _myCell = _state.TargetCell;
                _state.Switch<Idle>();
                
                 { _messenger.Message(MessengerTiles.Messages.IAmInPosition, this);  }
                
                
            }

            

           
        }
        public virtual void TookMe()
        {
            _state.Switch<Selected>();
        }
        public virtual void Fail()
        {
            _state.Switch<Fail>();
        }
        public virtual void GetResourse(float size, Cell cell, IListenerTiles messenger, int id, in float speed)
        {
            _messenger = messenger;
            _anim = GetComponent<Animator>();
            List<SpriteRenderer> renders = new List<SpriteRenderer>();
            foreach (SpriteRenderer render in GetComponentsInChildren<SpriteRenderer>())
            {
                renders.Add(render);
            }
            FXDead = GetComponentInChildren<ParticleSystem>();
            var main = FXDead.main;
            main.startColor = new ParticleSystem.MinMaxGradient(_myColor);
            StateContext context = new StateContext(_anim, this, renders, FXDead, messenger, GetComponent<AudioSource>());
            _state = new StateMachine(context);
            _state.Initialization();
            _distance = size;
            _transform = GetComponent<Transform>();
            _transform.localScale = new Vector3(size, size, _transform.localScale.z);
            GetCell(cell);
            _iD = id;
            _speed = speed;
            IAmActive = true;
            


        }
        
        
        public void GetCell(Cell cell)
        {
            _myCell = cell;
        }
        


    }
}
