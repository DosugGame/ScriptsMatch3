using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Match3
{
    public class FruitMenu : MonoBehaviour
    {
        private Animator _anim;
        private bool _nextAnim = false;
        private bool _dead = false;
        private ParticleSystem _deadEffect;
        [SerializeField] private Color _myColor;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _deadEffect = GetComponentInChildren<ParticleSystem>();
            var main = _deadEffect.main;
            main.startColor = new ParticleSystem.MinMaxGradient(_myColor);
        }
        public void Animation(int number)
        {
            if (number == 1) _anim.Play("Press", 0);
            else if (number == 1) _anim.Play("Press", 0);
        }
        private void Start()
        {
            StartCoroutine(Bird());
        }
        IEnumerator Bird()
        {
            foreach (SpriteRenderer render in GetComponentsInChildren<SpriteRenderer>())
            {
                render.enabled = false;
            }
            float second = Random.Range(1, 10);
            yield return new WaitForSeconds(second);
            second = Random.Range(1, 3);
            foreach (SpriteRenderer render in GetComponentsInChildren<SpriteRenderer>())
            {
                render.enabled = true;
            }
            _anim.Play("Start", 0);
            yield return new WaitForSeconds(second);
            _nextAnim = true;
        }
        private void Update()
        {
            if (_dead) { StopAllCoroutines(); gameObject.SetActive(false); }
            else  if (_nextAnim) NextAnim();
        }
        private void NextAnim()
        {
            _nextAnim = false;
            int number = Random.Range(0,5);
            switch (number)
            {
                case 1: StartCoroutine(Press()); break;
                case 2: StartCoroutine(Fail()); break;
                case 3: StartCoroutine(Idle()); break;
                case 4: StartCoroutine(Dead()); break;
                default:
                    StartCoroutine(Idle()); 
                    break;
            }

        }
        IEnumerator Press()
        {
            float second = Random.Range(1, 2);
            _anim.Play("Press", 0);
            yield return new WaitForSeconds(second);
            _anim.Play("UnPress", 0);
            yield return new WaitForSeconds(second);
            _nextAnim = true;
        }
        IEnumerator Fail()
        {
            float second = Random.Range(1, 2);
            _anim.Play("Fail", 0);
            yield return new WaitForSeconds(second);
            _nextAnim = true;
        }
        IEnumerator Idle()
        {
            float second = Random.Range(2, 7);
            _anim.Play("BeforeIdle", 0);
            yield return new WaitForSeconds(second);
            _nextAnim = true;
        }
        IEnumerator Dead()
        {
            float second = Random.Range(1, 3);
            _anim.Play("Dead", 0);
            yield return new WaitForSeconds(second);
            foreach (SpriteRenderer render in GetComponentsInChildren<SpriteRenderer>())
            {
                render.enabled = false;
            }
            _deadEffect.Play();
            yield return new WaitForSeconds(1.5f);
            _dead = true;
        }
    }
}
