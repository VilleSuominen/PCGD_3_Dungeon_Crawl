using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenuController : MonoBehaviour
{
    GameObject mainText1;
    GameObject mainText2;
    GameObject mainText3;
    GameObject activePage;
    GameObject nextButton;
    GameObject backButton;
    Text pageNumber;

    // Start is called before the first frame update
    void Awake()
    {
        mainText1 = transform.Find("MainText1").gameObject;
        mainText2 = transform.Find("MainText2").gameObject;
        mainText3 = transform.Find("MainText3").gameObject;
        nextButton = transform.Find("NextButton").gameObject;
        backButton = transform.Find("BackButton").gameObject;
        pageNumber = transform.Find("PageNumber").gameObject.GetComponent<Text>();
    }

    void OnDisable()
    {
         mainText1.SetActive(true);
         mainText2.SetActive(false);
         mainText3.SetActive(false);
         nextButton.SetActive(true);
         backButton.SetActive(false);
         pageNumber.text = "1/3";
    }

    public void ShowNextText()
    {
        if (mainText1.activeSelf == true)
        {            
            mainText1.SetActive(false);
            mainText2.SetActive(true);
            pageNumber.text = "2/3";
            backButton.SetActive(true);
        }

        else if (mainText2.activeSelf == true)
        {
            mainText2.SetActive(false);
            mainText3.SetActive(true);
            pageNumber.text = "3/3";
            nextButton.SetActive(false);
        }
    }

    public void ShowPreviousText()
    {
        if (mainText3.activeSelf == true)
        {
            mainText3.SetActive(false);
            mainText2.SetActive(true);
            pageNumber.text = "2/3";
            nextButton.SetActive(true);
        }

        else if (mainText2.activeSelf == true)
        {
            mainText2.SetActive(false);
            mainText1.SetActive(true);
            pageNumber.text = "1/3";
            backButton.SetActive(false);
        }
    }
}
