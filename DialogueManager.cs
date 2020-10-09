using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
	public TextAsset myTextfile;
	
	public GameObject DialogueCanvas;
	public GameObject DialogueLine;
	public Button ContinueButton;
	public Button CloseButton;
	public Button PlayerOptions1;
	public Button PlayerOptions2;
	public Button PlayerOptions3;
	public Button PlayerOptions4;
	public float typingSpeed = 2f;
	
	private TextMeshProUGUI dialogueChar;
	private TextMeshProUGUI dialogueLine;
	private TextMeshProUGUI playerOptions1;
	private TextMeshProUGUI playerOptions2;
	private TextMeshProUGUI playerOptions3;
	private TextMeshProUGUI playerOptions4;
	private string[] sentences;
	private string[] currentLine;
	private int index;
	private int options;
	private int option1Index;
	private int option2Index;
	private int option3Index;
	private int option4Index;
	
    void Start()
    {
        dialogueChar = DialogueLine.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		dialogueLine = DialogueLine.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
		playerOptions1 = PlayerOptions1.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		playerOptions2 = PlayerOptions2.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		playerOptions3 = PlayerOptions3.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		playerOptions4 = PlayerOptions4.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		
		SetOptionsOff();
		SetDialogueOff();
		
		StartDialogue(myTextfile, 0);
    }
	
	void FixedUpdate()
	{
		
	}
	
	public void StartDialogue(TextAsset textfile, int currentIndex)
	{
		sentences = textfile.text.Split(char.Parse("\n"));
		index = currentIndex;
		
		if (sentences[index] != null)
		{
			DialogueLine.gameObject.SetActive(true);
			ContinueButton.gameObject.SetActive(true);
			StartCoroutine(Type());
		}
		
		else
		{
			Debug.Log("Text not find!");
		}
	}
	
	public void CloseDialogue()
	{
		SetOptionsOff();
		SetDialogueOff();
	}
	
	public void CleanText(TextMeshProUGUI text)
	{
		text.SetText("");
	}
	
	public void SetOptionsOff()
	{
		PlayerOptions1.gameObject.SetActive(false);
		PlayerOptions2.gameObject.SetActive(false);
		PlayerOptions3.gameObject.SetActive(false);
		PlayerOptions4.gameObject.SetActive(false);
	}
	
	public void SetDialogueOff()
	{
		DialogueLine.gameObject.SetActive(false);
		ContinueButton.gameObject.SetActive(false);
		CloseButton.gameObject.SetActive(false);
	}
	
	public void NextSentence()
	{
		index ++;
		StartCoroutine(Type());
		SetOptionsOff();
	}
	
	public void Option1()
	{
		index = option1Index - 2;
		ContinueButton.interactable = true;
	}
	
	public void Option2()
	{
		index = option2Index - 2;
		ContinueButton.interactable = true;
	}
	
	public void Option3()
	{
		index = option3Index - 2;
		ContinueButton.interactable = true;
	}
	
	public void Option4()
	{
		index = option4Index - 2;
		ContinueButton.interactable = true;
	}
	
	public void PrintOptions()
	{
		options = int.Parse(currentLine[4]);
		switch (options)
		{
			case 4:
				index ++;
				CleanText(playerOptions1);
				PlayerOptions1.gameObject.SetActive(true);
				currentLine = sentences[index].Split(char.Parse("|"));
				playerOptions1.text = currentLine[2];
				option1Index = int.Parse(currentLine[3]);
				
				index ++;
				CleanText(playerOptions2);
				PlayerOptions2.gameObject.SetActive(true);
				currentLine = sentences[index].Split(char.Parse("|"));
				playerOptions2.text = currentLine[2];
				option2Index = int.Parse(currentLine[3]);
				
				index ++;
				CleanText(playerOptions3);
				PlayerOptions3.gameObject.SetActive(true);
				currentLine = sentences[index].Split(char.Parse("|"));
				playerOptions3.text = currentLine[2];
				option3Index = int.Parse(currentLine[3]);
				
				index ++;
				CleanText(playerOptions4);
				PlayerOptions4.gameObject.SetActive(true);
				currentLine = sentences[index].Split(char.Parse("|"));
				playerOptions4.text = currentLine[2];
				option4Index = int.Parse(currentLine[3]);
				break;
				
			case 3:
				index ++;
				CleanText(playerOptions1);
				PlayerOptions1.gameObject.SetActive(true);
				currentLine = sentences[index].Split(char.Parse("|"));
				playerOptions1.text = currentLine[2];
				option1Index = int.Parse(currentLine[3]);
				
				index ++;
				CleanText(playerOptions2);
				PlayerOptions2.gameObject.SetActive(true);
				currentLine = sentences[index].Split(char.Parse("|"));
				playerOptions2.text = currentLine[2];
				option2Index = int.Parse(currentLine[3]);
				
				index ++;
				CleanText(playerOptions3);
				PlayerOptions3.gameObject.SetActive(true);
				currentLine = sentences[index].Split(char.Parse("|"));
				playerOptions3.text = currentLine[2];
				option3Index = int.Parse(currentLine[3]);
				break;	
				
			case 2:
				index ++;
				CleanText(playerOptions1);
				PlayerOptions1.gameObject.SetActive(true);
				currentLine = sentences[index].Split(char.Parse("|"));
				playerOptions1.text = currentLine[2];
				option1Index = int.Parse(currentLine[3]);
				
				index ++;
				CleanText(playerOptions2);
				PlayerOptions2.gameObject.SetActive(true);
				currentLine = sentences[index].Split(char.Parse("|"));
				playerOptions2.text = currentLine[2];
				option2Index = int.Parse(currentLine[3]);
				break;
		}
		
	}
	
	IEnumerator Type()
	{
		CleanText(dialogueLine);
		CleanText(dialogueChar);
		ContinueButton.interactable = false;
		currentLine = sentences[index].Split(char.Parse("|"));
		dialogueChar.text = currentLine[1] + ":";

		foreach (char letter in currentLine[2].ToCharArray())
		{
			dialogueLine.text += letter;
			yield return new WaitForSeconds(.1f/typingSpeed);
		}
		
		if (currentLine.Length < 5)
		{
			if (int.Parse(currentLine[3]) != 0)
			{
				ContinueButton.gameObject.SetActive(false);
				CloseButton.gameObject.SetActive(true);
				CloseButton.interactable = true;
			}
			
			else
			{
				ContinueButton.interactable = true;
			}
		}
		
		else
		{
			PrintOptions();
		}
		
	}
	
}
