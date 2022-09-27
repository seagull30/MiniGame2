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
    private float _accumulatedOverload = 10f;
    [SerializeField]
    private float _overloadReduction = -10f;

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

    private Animator _animator;
    private static class AnimationID
    {
        public static readonly int OVERLOAD = Animator.StringToHash("Overload");
        public static readonly int ISMOVE = Animator.StringToHash("IsMove");
    }


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
        _isDead = false;
    }


    private void Update()
    {
        checkInput();
        if (_isOverload)
            overload();
        else
        {
            move();
            falling();
        }
        changeColor();
        updateScore();
    }

    private void checkInput()
    {
        if (_isOverload)
        {
            _isMove = false;
            return;
        }
        if (Input.GetKey(KeyCode.Space))
        {
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
            }
            _isMove = true;
        }
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

        if (!_isCoroutineRunning && _elapsedTime > 1.5f)
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
                _animator.SetTrigger(AnimationID.OVERLOAD);
                break;
            }
            if (_overloadBar.value <= 0f)
            {
                _overloadBar.value = 0f;
                _isOverload = false;
                _isCoroutineRunning = false;
                break;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeadZone"))
        {
            GameManager.Instance.GameOver();
            _isDead = true;
            gameObject.SetActive(false);
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
