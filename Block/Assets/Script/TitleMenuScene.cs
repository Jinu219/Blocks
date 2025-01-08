using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleMenuScene : MonoBehaviour
{
    public void LoadTetrisGameScene()
    {
        SceneManager.LoadScene("TetrisGameScene");
    }
    public void LoadBlockGameScene()
    {
        SceneManager.LoadScene("BlockGameScene");
    }
}
