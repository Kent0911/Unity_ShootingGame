using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    // プレイヤーを移動させるポイント間
    public Transform [ ] mRoutePoints;
    [Range(0, 200)]
    public float mSpeed = 10f;
    bool isHitRootPoint;

    [Range(0, 50)]
    public float mMoveSpeed = 10f;
    public float mMoveRange = 40f;

    public float _initiaLife = 100;
    public float mLife = 100;

    public Image mLifeGage;
    public Bullet mBulletPrefab;

    Enemy _enemy;

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
        } else if (other.gameObject.tag == "Enemy") {
            mLife -= 10f;
            mLifeGage.fillAmount = mLife / _initiaLife;

            other.gameObject.SetActive(false);
            Object.Destroy(other.gameObject); // 当たった敵は削除

            if (mLife <= 0f) {
                Camera.main.transform.SetParent(null);
                gameObject.SetActive(false);
                var sceneManager = Object.FindAnyObjectByType<SceneManager>();
                sceneManager.ShowGameOver();
            }
        } else if (other.gameObject.tag == "ClearRoutePoint") {
            other.gameObject.SetActive(false);
            isHitRootPoint = true;
            var sceneManager = Object.FindAnyObjectByType<SceneManager>();
            sceneManager.ShowClear();
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ground") {
            mLife = 0f;
            mLifeGage.fillAmount = mLife / _initiaLife;

            Camera.main.transform.SetParent(null);
            gameObject.SetActive(false);
            var sceneManager = Object.FindObjectOfType<SceneManager>();
            sceneManager.ShowGameOver();
        }
    }

    public void ShotBullet(Vector3 _targetPos) {
        var bullet = Object.Instantiate(mBulletPrefab, transform.position, Quaternion.identity);
        bullet.Init(transform.position, _targetPos);
    }

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update() {
        _enemy = FindObjectOfType<Enemy>();
        if (Input.GetKey(KeyCode.Space)) {
            ShotBullet(_enemy.transform.position);
        }
    }
}
