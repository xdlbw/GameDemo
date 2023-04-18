using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign_Ikun : MonoBehaviour
{
    public GameObject DialogBox;
    public Text DialogText;
    [TextArea(1, 4)] public string[] SignText;
    public int CurrentIndex;
    public GameObject EnterDialog;

    private bool isPlayerInSign;
    //private TypeWritterEffect writterEffect;
    // Start is called before the first frame update
    void Start()
    {
        CurrentIndex = 0;
        DialogText.text = SignText[CurrentIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInSign)
        {
            SoundManager.PlayhelloClip();
            DialogText.text = SignText[CurrentIndex];
            DialogBox.SetActive(true);
            EnterDialog.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInSign = true;
            EnterDialog.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            SoundManager.StopAudioClip();
            isPlayerInSign = false;
            DialogBox.SetActive(false);
            EnterDialog.SetActive(false);
        }
    }

    public void CloseDialog()
    {
        DialogBox.SetActive(false);
        CurrentIndex = 0;
    }

    public void ContinueDialog()
    {
        CurrentIndex++;
        if (CurrentIndex < SignText.Length)
        {
            if(CurrentIndex == 1)
            {
                SoundManager.StopAudioClip();
                SoundManager.PlayCaiXuKunClip();
            }
            else if(CurrentIndex == 2)
            {
                SoundManager.StopAudioClip();
                SoundManager.PlayHabitClip();
            }
            else
            {
                SoundManager.StopAudioClip();
                SoundManager.PlayJiNiTaiMeiClip();
            }
            DialogText.text = SignText[CurrentIndex];
        }
        else
        {
            SoundManager.StopAudioClip();
            CloseDialog();
            CurrentIndex = 0;
        }
    }
}
