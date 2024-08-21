using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Bullet mBulletPrefab;



    public void ShotBullet(Vector3 _targetPos) {
        var bullet = Object.Instantiate(mBulletPrefab, transform.position, Quaternion.identity);
        bullet.Init(transform.position, _targetPos, transform.rotation);
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            ShotBullet(transform.position);
        }
    }
}
