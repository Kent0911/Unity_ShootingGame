using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Range(0, 100)]
    public float mSpeed = 10f;
    public float mDeadSecond = 10f;

    public float mLife = 20f;

    float _time;
    Player _player;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Bullet") {
            mLife -= 10f;
            if (mLife <= 0f) {
                Object.Destroy(gameObject);
                var sceneManager = Object.FindObjectOfType<SceneManager>();
                sceneManager.AddScore(1000);
            }
            Object.Destroy(other.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start() {
        _time = 0f;
        _player = Object.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update() {
        _time += Time.deltaTime;
        if (_time >= mDeadSecond) {
            Object.Destroy(gameObject);
        }
        else {
            var vec = _player.transform.position - transform.position;
            transform.position += vec.normalized * mSpeed * Time.deltaTime;
        }
    }
}
