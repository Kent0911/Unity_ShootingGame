using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {
    [SerializeField]
    public TMP_Text mTextGameOver;
    [SerializeField]
    public TMP_Text mTextClear;

    public TMP_Text mScoreText;
    int mCurrentScore = 0;

    public void ShowGameOver() {
        mTextGameOver.gameObject.SetActive(true);
    }

    public void ShowClear() {
        mTextClear.gameObject.SetActive(true);
    }

    public void AddScore(int _score) {
        mCurrentScore += _score;
        mScoreText.SetText(mCurrentScore.ToString());
    }

    // Start is called before the first frame update
    void Start() {
        mTextGameOver.gameObject.SetActive(false);
        mTextClear.gameObject.SetActive(false);

        mScoreText.SetText(mCurrentScore.ToString());
    }

    // Update is called once per frame
    void Update() {
        
    }
}
