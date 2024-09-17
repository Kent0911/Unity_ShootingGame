using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPopArea : MonoBehaviour {

    public GameObject [ ] mEnemyList;

    private  void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            foreach (var ememy in mEnemyList) {
                ememy.SetActive(true);
            }
            // ��x�L���������瓖����Ȃ��悤�ɂ���
            var collider = GetComponent<Collider>();
            collider.enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start() {
        foreach(var enemy in mEnemyList) {
            enemy.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
