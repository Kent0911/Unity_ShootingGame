using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // プレイヤーを移動させるポイント間
    public Transform [ ] mRoutePoints;
    [Range(0, 200)]
    public float mSpeed = 10f;
    bool isHitRootPoint;

    [Range(0, 50)]
    public float mMoveSpeed = 10f;
    public float mMoveRange = 40f;

    public Bullet mBulletPrefab;

    IEnumerator Move() {
        var prevPointPos = transform.position;
        var basePosition = transform.position;
        var movePos = Vector2.zero;

        foreach (var nextPoint in mRoutePoints) {
            isHitRootPoint = false;
            while (!isHitRootPoint) {

                var vec = nextPoint.position - prevPointPos;
                vec.Normalize();

                basePosition += vec * mSpeed * Time.deltaTime;

                movePos.x += Input.GetAxis("Horizontal") * mMoveSpeed * Time.deltaTime;
                movePos.y += Input.GetAxis("Vertical") * mMoveSpeed * Time.deltaTime;
                movePos = Vector2.ClampMagnitude(movePos, mMoveRange);
                var worldMovePos = Matrix4x4.Rotate(transform.rotation).MultiplyVector(movePos);

                transform.position = basePosition + worldMovePos;

                transform.position += vec * mSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vec, Vector3.up), 0.5f);

                yield return null;
            }
            prevPointPos = nextPoint.position;
        }
    }

    private void OnTriggerEnter(Collider other) { 
        if (other.gameObject.tag == "mRoutePoints") {
            other.gameObject.SetActive(false);
            isHitRootPoint = true;
        }
    }



    public void ShotBullet(Vector3 _targetPos) {
        var bullet = Object.Instantiate(mBulletPrefab, transform.position, Quaternion.identity);
        bullet.Init(transform.position, _targetPos, transform.rotation);
    }

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            ShotBullet(transform.position);
        }
    }
}
