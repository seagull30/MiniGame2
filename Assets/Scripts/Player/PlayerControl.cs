using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private float _elapsedTime;

    private bool _isOverload = false;
    private bool _isCoroutineRunning = false;
    bool _isContinuousTouch = false;


    private bool _isCeiling = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _overloadBar = GetComponentInChildren<Slider>();
        _overloadBar.maxValue = _maxOverloadLevel;
        _overloadBar.value = 0f;

    }
    private void Update()
    {
        checkInput();
        if (_isOverload)
            overload();
        else
        {
            move();
            refueling();
        }
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
                StopAllCoroutines();
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
                StopAllCoroutines();
                _isCoroutineRunning = false;
            }
            _isMove = false;
        }
    }

    private void move()
    {
        if (!_isMove)
            return;

        _elapsedTime += Time.deltaTime;

        if (!_isCeiling)
        {
            _moveDistance = _moveSpeed * Time.deltaTime;
            transform.position += new Vector3(0f, _moveDistance, 0f);
        }
        else
        {

        }

        //_rigidbody.AddForce(new Vector2(0f, moveSpeed));

        if (!_isCoroutineRunning && _elapsedTime > 2f)
        {
            StartCoroutine(changeValue(_accumulatedOverload));
        }
    }

    private void refueling()
    {
        if (_isMove)
            return;
        _elapsedTime += Time.deltaTime;

        _moveDistance = -(_fallingSpeed * Time.deltaTime);

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

    IEnumerator changeValue(float amounToChange)
    {
        _isCoroutineRunning = true;
        while (true)
        {

            _overloadBar.value += amounToChange;
            if (_overloadBar.value >= _maxOverloadLevel)
            {
                _elapsedTime = 0f;
                _overloadBar.value = _maxOverloadLevel;
                _isOverload = true;
                _isCoroutineRunning = false;
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

    private void restart()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeadZone"))
        {
            GameManager.Instance.GameOver();
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
