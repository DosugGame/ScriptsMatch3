using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3 {
    public interface IListener
    {
        public void IAmIsOver<T>(T state);
    }
    public class StateMachine : IListener
    {
        public StateMachine(StateContext context)
        {
            _context = context;
        }
        protected List<State> _states = new List<State>();
        protected State _current;
        protected StateContext _context;
        protected IListenerTiles _messenger;
        public bool INeedMove { get => _current.INeedMove; set { } }
        public Cell TargetCell;
        public bool IMustDie = false;
        public virtual void Initialization()
        {
            _states.Add(new Idle(_context, this));
            _states.Add(new Destruction(_context, this));
            _states.Add(new Selected(_context, this));
            _states.Add(new Unselected(_context, this, TargetCell));
            _states.Add(new Birth(_context, this));
            _states.Add(new Fail(_context, this));
            _states.Add(new DestructionInATrice(_context, this));
            _states.Add(new DeathByBomb(_context, this, TargetCell));
            Switch<Birth>();
            _messenger = _context.Messenger;
        }
        
        public virtual void Switch<T>(Cell target = null) where T: State 
        {

            if (IMustDie) IMustDie = false;
                if (_current != null) _current.Stop();
                foreach (State state in _states)
                {
                    if (state is T) _current = state;
                    if (target != null) TargetCell = target;
                
                }
                if (_current != null) _current.Start();
                
            
            
        }
        public virtual void IAmIsOver<T>( T state)
        {
            if(state is Birth || state is Fail)
            {
                Switch<Idle>();
                if (state is Birth) _messenger.Message(MessengerTiles.Messages.IWasBorn);
                
                //if(TargetCell != null) _current.INeedMove = true;
               
            }
            if (state is Destruction || state is DestructionInATrice) _messenger.Message(MessengerTiles.Messages.iAmDestroyed, _context.Mono);
            if (state is DeathByBomb) IMustDie = true;
        }
    } 
}
