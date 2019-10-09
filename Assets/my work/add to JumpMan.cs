//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class addtoJumpMan : MonoBehaviour
//{
//    public Text countText;
//    public Text winText;
//    private int count;
//    // Start is called before the first frame update
//    void Start()
//    {
//        count = 0;
//        SetCountText();
//        winText.text = "";
//    }
//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.gameObject.CompareTag("Coin"))
//        {
//            other.gameObject.SetActive(false);
//            count = count + 1;
//            SetCountText();
//        }
//    }
//    void SetCountText()
//    {
//        countText.text = "Count: " + count.ToString();
//        if (count >= 230)
//        {
//            winText.text = "You Win";
//        }
//    }
//}
