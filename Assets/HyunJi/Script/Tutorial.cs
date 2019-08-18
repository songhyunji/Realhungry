using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{

	public Sprite[] tutorialImg = new Sprite[5];

	Image img;
	[SerializeField]
	int num;

    // Start is called before the first frame update
    void Start()
    {
		num = 0;
		img = this.GetComponent<Image>();
    }

	public void NextBtnPress()
	{
		num++;
		if (num > 4)
		{
			SceneManager.LoadScene("Main");
		}
		else
		{
			img.sprite = tutorialImg[num];
		}
	}

	public void PreBtnPress()
	{
		num--;
		if (num < 0)
		{
			SceneManager.LoadScene("Title");
		}
		else
		{
			img.sprite = tutorialImg[num];
		}
	}
}
