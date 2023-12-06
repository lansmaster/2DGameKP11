// NULLcode Studio © 2016
// null-code.ru

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml;
using System.IO;

public class DialogueManager : MonoBehaviour {

	public ScrollRect scrollRect;
	public ButtonComponent[] buttonText; // первый элемент списка, всегда будет использоваться для вывода текста NPC, остальные элементы для ответов, соответственно, общее их количество должно быть достаточным
	public string folder = "Russian"; // подпапка в Resources, для чтения
	public int offset = 20;

	private string fileName, lastName;
	private List<Dialogue> node;
	private Dialogue dialogue;
	private Answer answer;
	private float curY, height;
	private static DialogueManager _internal;
	private int id;
	private static bool _active;

	public void DialogueStart(string name)
	{
		if(name == string.Empty) return;
		fileName = name;
		Load();
	}

	public static DialogueManager Internal
	{
		get{ return _internal; }
	}

	public static bool isActive
	{
		get{ return _active; }
	}

	void Awake()
	{
		_internal = this;
		CloseWindow();
	}

	void Load()
	{
		if(lastName == fileName) // проверка, чтобы не загружать уже загруженный файл
		{
			BuildDialogue(0);
			return;
		}

		node = new List<Dialogue>();

		try // чтение элементов XML и загрузка значений атрибутов в массивы
		{
			TextAsset binary = Resources.Load<TextAsset>(folder + "/" + fileName);
			XmlTextReader reader = new XmlTextReader(new StringReader(binary.text));

			int index = 0;
			while(reader.Read())
			{
				if(reader.IsStartElement("node"))
				{
					dialogue = new Dialogue();
					dialogue.answer = new List<Answer>();
					dialogue.npcText = reader.GetAttribute("npcText");
					dialogue.id = GetINT(reader.GetAttribute("id"));
					node.Add(dialogue);

					XmlReader inner = reader.ReadSubtree();
					while(inner.ReadToFollowing("answer"))
					{
						answer = new Answer();
						answer.text = reader.GetAttribute("text");
						answer.toNode = GetINT(reader.GetAttribute("toNode"));
						answer.exit = GetBOOL(reader.GetAttribute("exit"));
						answer.questStatus = GetINT(reader.GetAttribute("questStatus"));
						answer.questValue = GetINT(reader.GetAttribute("questValue"));
						answer.questValueGreater = GetINT(reader.GetAttribute("questValueGreater"));
						answer.questName = reader.GetAttribute("questName");
						node[index].answer.Add(answer);
					}
					inner.Close();

					index++;
				}
			}

			lastName = fileName;
			reader.Close();
		}
		catch(System.Exception error)
		{
			Debug.Log(this + " ошибка чтения файла диалога: " + fileName + ".xml | Error: " + error.Message);
			CloseWindow();
			lastName = string.Empty;
		}

		BuildDialogue(0);
	}

	void AddToList(bool exit, int toNode, string text, int questStatus, string questName, bool isActive)
	{
		buttonText[id].text.text = text;
		buttonText[id].rect.sizeDelta = new Vector2(buttonText[id].rect.sizeDelta.x, buttonText[id].text.preferredHeight + offset);
		buttonText[id].button.interactable = isActive;
		height = buttonText[id].rect.sizeDelta.y;
		buttonText[id].rect.anchoredPosition = new Vector2(0, -height/2 - curY);

		if(exit)
		{
			SetExitDialogue(buttonText[id].button);
			if(questStatus != 0) SetQuestStatus(buttonText[id].button, questStatus, questName);
		}
		else
		{
			SetNextNode(buttonText[id].button, toNode);
			if(questStatus != 0) SetQuestStatus(buttonText[id].button, questStatus, questName);
		}

		id++;

		curY += height + offset;
		RectContent();
	}

	void RectContent()
	{
		scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, curY);
		scrollRect.content.anchoredPosition = Vector2.zero;
	}

	void ClearDialogue()
	{
		id = 0;
		curY = offset;
		foreach(ButtonComponent b in buttonText)
		{
			b.text.text = string.Empty;
			b.rect.sizeDelta = new Vector2(b.rect.sizeDelta.x, 0);
			b.rect.anchoredPosition = new Vector2(b.rect.anchoredPosition.x, 0);
			b.button.onClick.RemoveAllListeners();
		}
		RectContent();
	}

	void SetQuestStatus(Button button, int i, string name) // событие, для управлением статуса, текущего квеста
	{
		string t = name + "|" + i; // склейка имени квеста и значения, которое ему назначено
		button.onClick.AddListener(() => QuestStatus(t));
	}

	void SetNextNode(Button button, int i) // событие, для перенаправления на другой узел диалога
	{
		button.onClick.AddListener(() => BuildDialogue(i));
	}

	void SetExitDialogue(Button button) // событие, для выхода из диалога
	{
		button.onClick.AddListener(() => CloseWindow());
	}

	void QuestStatus(string s) // меняем статус квеста
	{
		string[] t = s.Split(new char[]{'|'});

		if(t[1] == "1")
		{
			QuestManager.SetQuestStatus(t[0], QuestManager.Status.Active);
		}
		else if(t[1] == "2")
		{
			QuestManager.SetQuestStatus(t[0], QuestManager.Status.Disable);
		}
		else if(t[1] == "3")
		{
			QuestManager.SetQuestStatus(t[0], QuestManager.Status.Complete);
		}
	}

	void CloseWindow() // закрываем окно диалога
	{
		_active = false;
		scrollRect.gameObject.SetActive(false);
	}

	void ShowWindow() // показываем окно диалога
	{
		scrollRect.gameObject.SetActive(true);
		_active = true;
	}

	int FindNodeByID(int i)
	{
		int j = 0;
		foreach(Dialogue d in node)
		{
			if(d.id == i) return j;
			j++;
		}

		return -1;
	}

	void BuildDialogue(int current)
	{
		ClearDialogue();

		int j = FindNodeByID(current);

		if(j < 0)
		{
			Debug.LogError(this + " в диалоге [" + fileName + ".xml] отсутствует или указан неверно идентификатор узла.");
			return;
		}

		AddToList(false, 0, node[j].npcText, 0, string.Empty, false); // добавление текста NPC

		for(int i = 0; i < node[j].answer.Count; i++)
		{
			int value = QuestManager.GetCurrentValue(node[j].answer[i].questName);

			// фильтр ответов, относительно текущего статуса квеста
			if(value >= node[j].answer[i].questValueGreater && node[j].answer[i].questValueGreater != 0 || 
				node[j].answer[i].questValue == value && node[j].answer[i].questValueGreater == 0 || 
				node[j].answer[i].questName == null)
			{
				AddToList(node[j].answer[i].exit, node[j].answer[i].toNode, node[j].answer[i].text, node[j].answer[i].questStatus, node[j].answer[i].questName, true); // текст игрока
			}
		}

		EventSystem.current.SetSelectedGameObject(scrollRect.gameObject); // выбор окна диалога как активного, чтобы снять выделение с кнопок диалога
		ShowWindow();
	}

	int GetINT(string text)
	{
		int value;
		if(int.TryParse(text, out value))
		{
			return value;
		}
		return 0;
	}

	bool GetBOOL(string text)
	{
		bool value;
		if(bool.TryParse(text, out value))
		{
			return value;
		}
		return false;
	}
}
	
class Dialogue
{
	public int id;
	public string npcText;
	public List<Answer> answer;
}


class Answer
{
	public string text, questName;
	public int toNode, questValue, questValueGreater, questStatus;
	public bool exit;
}