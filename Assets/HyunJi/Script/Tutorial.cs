using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject tuto;
	public Sprite[] tutorialImg = new Sprite[5];

	Image img;
	[SerializeField]
	int num;


    void OnEnable()
    {
        num = 0;
        img = this.GetComponent<Image>();
        img.sprite = tutorialImg[0];
    }

    public void NextBtnPress()
	{
		if (num >= 4)
		{
            tuto.SetActive(false);
        }
        else
        {
            num++;
            img.sprite = tutorialImg[num];
        }
    }

	public void PreBtnPress()
	{
		if (num == 0)
		{
            tuto.SetActive(false);
		}
		else
		{
            num--;
            img.sprite = tutorialImg[num];
        }
    }
}
