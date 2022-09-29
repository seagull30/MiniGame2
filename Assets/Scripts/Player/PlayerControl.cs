using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [SerializeField]
    private float _moveSpeed = 3f;

    [SerializeField]
    private float _fallingSpeed = 2f;

    private float _moveDistance;

    private float _maxOverloadLevel = 100f;

    [SerializeField]
    private float _accumulatedOverload;
    [SerializeField]
    private float _overloadReduction;
    [SerializeField]
    private float _repeatingGauge;
    [SerializeField]
    private float _cooldownTime;

    private bool _isMove = false;
    private Slider _overloadBar;
    private Image _overloadBarColor;

    private float _elapsedTime;
    private int points;

    private bool _isOverload = false;
    private bool _isCoroutineRunning = false;
    private bool _isContinuousTouch = false;

    public UnityAction<float> playerOnCiling;

    private static readonly Color32[] colors =
    {
        new Color32(0,255,24,255),
        new Color32(241,139,26,255),
        new Color32(226,34,30, 255),
    };

    private bool _isCeiling = false;
    private bool _isDead;
    private bool _isStart;

    private Animator _animator;
    private static class AnimationID
    {
        public static readonly int OVERLOAD = Animator.StringToHash("Overload");
        public static readonly int ISMOVE = Animator.StringToHash("IsMove");
    }

    private readonly string _flySound = "PlayerFly";


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _overloadBar = GetComponentInChildren<Slider>();
        _overloadBar.maxValue = _maxOverloadLevel;
        _overloadBar.value = 0f;
        _overloadBarColor = _overloadBar.transform.Find("Fill Area").GetComponentInChildren<Image>();
        _animator = GetComponent<Animator>();
        StartCoroutine(getScore());
    }

    private void Start()
    {
        GameManager.Instance.player = gameObject.GetComponent<PlayerControl>();
        GameManager.Instance.player.playerOnCiling += GameManager.Instance.PlayerOnCiling;
        GameManager.Instance._ispause = false;
        SoundManager.instance.StopAllSE();
        _isStart = false;
        _isDead = false;
        Invoke("checkTimeover", 3f);

    }


    private void Update()
    {
        checkInput();
        if (_isStart)
        {
            if (_isOverload)
                overload();
            else
            {
                move();
                falling();
            }
            changeColor();
            //스코어 일시 정지 시켜야 함
            updateScore();
        }
    }

    private void checkInput()
    {
        if (_isOverload)
        {
            _isMove = false;
            SoundManager.instance.StopAllSE();
            return;
        }
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _isStart = true;

                if (!_isMove)
                {
                    if (_isContinuousTouch)
                    {
                        _overloadBar.value += 15;
                        if (_overloadBar.value >= _maxOverloadLevel)
                        {
                            _overloadBar.value = _maxOverloadLevel;
                            _isOverload = true;
                        }
                    }
                    _elapsedTime = 0f;
                    _isCoroutineRunning = false;
                    SoundManager.instance.PlaySE(_flySound);
                }
                _isMove = true;
            }
        }
#elif UNITY_EDITOR
        if (Input.GetKey(KeyCode.Space))
        {
            _isStart = true;
            if (!_isMove)
            {
                if (_isContinuousTouch)
                {
                    _overloadBar.value += _repeatingGauge;
                    if (_overloadBar.value >= _maxOverloadLevel)
                    {
                        _overloadBar.value = _maxOverloadLevel;
                        _isOverload = true;
                    }
                }
                _elapsedTime = 0f;
                _isCoroutineRunning = false;
                SoundManager.instance.PlaySE(_flySound);
            }
            _isMove = true;
        }
# endif 
        else
        {
            if (_isMove)
            {
                if (_elapsedTime < 1f)
                {
                    _isContinuousTouch = true;
                }
                else
                {
                    _isContinuousTouch = false;
                }
                _elapsedTime = 0f;
                _isCoroutineRunning = false;
                SoundManager.instance.StopAllSE();
            }
            _isMove = false;
        }
    }

    private void move()
    {
        if (!_isMove)
            return;

        _animator.SetBool(AnimationID.ISMOVE, true);

        _elapsedTime += Time.deltaTime;

        _moveDistance = _moveSpeed * Time.deltaTime;

        if (!_isCeiling)
        {
            transform.position += new Vector3(0f, _moveDistance, 0f);

            //_rigidbody.MovePosition(transform.position + new Vector3(0f, _moveDistance, 0f));
        }
        else
        {
            playerOnCiling.Invoke(_moveDistance);
        }

        if (!_isCoroutineRunning && _elapsedTime > 2f)
        {
            StartCoroutine(changeValue(_accumulatedOverload));
        }
    }

    private void falling()
    {
        if (_isMove)
            return;

        _animator.SetBool(AnimationID.ISMOVE, false);


        _elapsedTime += Time.deltaTime;

        _moveDistance = -(_fallingSpeed * Time.deltaTime);

        transform.position += new Vector3(0f, _moveDistance, 0f);

        //_rigidbody.MovePosition(transform.position + new Vector3(0f, _moveDistance, 0f));


        transform.position += new Vector3(0f, _moveDistance, 0f);

        if (!_isCoroutineRunning && _elapsedTime > _cooldownTime)
        {
            StartCoroutine(changeValue(_overloadReduction));
        }

    }

    private void overload()
    {
        _moveDistance = -(_fallingSpeed * Time.deltaTime);

        transform.position += new Vector3(0f, _moveDistance, 0f);

        _elapsedTime += Time.deltaTime;

        if (!_isCoroutineRunning && _elapsedTime >= 1.5f)
            StartCoroutine(changeValue(-25f));
    }

    private void changeColor()
    {
        if (_overloadBar.value <= 40)
        {
            _overloadBarColor.color = colors[0];
        }
        else if (_overloadBar.value <= 80)
        {
            _overloadBarColor.color = colors[1];
        }
        else
        {
            _overloadBarColor.color = colors[2];
        }
    }

    IEnumerator changeValue(float amounToChange)
    {
        _isCoroutineRunning = true;
        while (true)
        {
            if (!_isCoroutineRunning)
                yield break;
            _overloadBar.value += amounToChange;
            if (_overloadBar.value >= _maxOverloadLevel)
            {
                _elapsedTime = 0f;
                _overloadBar.value = _maxOverloadLevel;
                _isOverload = true;
                _isCoroutineRunning = false;
                SoundManager.instance.StopAllSE();
                _animator.SetTrigger(AnimationID.OVERLOAD);
                _animator.SetBool(AnimationID.ISMOVE, false);
                yield break;
            }
            if (_overloadBar.value <= 0f)
            {
                _overloadBar.value = 0f;
                _isOverload = false;
                _isCoroutineRunning = false;
                yield break;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator getScore()
    {
        while (true)
        {
            if (_isDead)
                yield break;
            if (_isMove)
                points = (Mathf.FloorToInt(_elapsedTime)) + 1;
            else
                points = 1;

            yield return new WaitForSeconds(0.1f);
        }
    }
    private void updateScore()
    {
        GameManager.Instance.addScore(points);
    }

    private void checkTimeover()
    {
        if (!_isStart)
            DIe();
    }

    private void DIe()
    {
        GameManager.Instance.GameOver();
        _isDead = true;
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeadZone"))
        {
            DIe();
        }
        if (other.CompareTag("Ceiling"))
        {
            _isCeiling = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ceiling"))
        {
            _isCeiling = false;
        }
    }
}
