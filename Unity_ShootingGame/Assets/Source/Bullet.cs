using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [Range(0, 10)]
    public float mDeadSecond = 3f;
    [Range(0, 200)]
    public float mSpeed = 100f;

    public Vector3 mStartPos { get; set; }

    public Vector3 mTargetPos { get; set; }

    Enemy _enemy;

    public void Init(Vector3 _startPos, Vector3 _targetPos) {
        mStartPos = _startPos;
        mTargetPos = _targetPos;
        StartCoroutine(Move());
    }

    IEnumerator Move() {
        float time = 0f;
        transform.position = mStartPos;
        var vec = (mTargetPos - mStartPos).normalized;
        while (time < mDeadSecond) {
            transform.position += vec * mSpeed * Time.deltaTime;
            time += Time.deltaTime; 
            yield return null;
        }

        Object.Destroy(gameObject);
    }
}
