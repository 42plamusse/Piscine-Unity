using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour {

	//Vous pouvez directement changer ces valeurs de base dans l'inspecteur si vous voulez personnaliser votre jeu
	[HideInInspector]public int playerHp = 20;
	public int playerMaxHp = 20;
	[HideInInspector]public int playerEnergy = 300;
	public int playerStartEnergy = 300;

	public int delayBetweenWaves = 10;					//Temps entre les vagues
	public int nextWaveEnnemyHpUp = 20; 				//Augmentation de la vie des bots a chaque vague (en %)
	public int nextWaveEnnemyValueUp = 30; 		//Augmentation de l'energie donnee par les bots a chaque vague (en %)
	public int averageWavesLenght = 15;					//Taille moyenne d'une vague d'ennemis
	public int totalWavesNumber = 20;						// Nombre des vagues au total
	[HideInInspector]public bool lastWave = false;
	[HideInInspector]public int currentWave = 1;
	private float tmpTimeScale = 1;
	[HideInInspector]public int score = 0;
	[HideInInspector] public bool isPaused = false;

	public Text hp;
	public Text energy;
	public GameObject pauseMenu;
	public GameObject endGameMenu;
	public string nextScene;

	public static gameManager gm;

	//Singleton basique  : Voir unity design patterns sur google.
	void Awake () {
		if (gm == null)
			gm = this;
	}

	void Start() {
		Time.timeScale = 1;
		playerHp = playerMaxHp;
		playerEnergy = playerStartEnergy;
		pauseMenu.SetActive(false);
	}
    private void Update()
    {
		hp.text = playerHp.ToString();
		energy.text = playerEnergy.ToString();
		if (!isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
			pauseMenu.SetActive(true);
			pause(true);
        }
	}
    //Pour mettre le jeu en pause
    public void pause(bool paused) {
		if (paused == true) {
			tmpTimeScale = Time.timeScale;
			Time.timeScale = 0;
			isPaused = true;
		}
        else
        {
			Time.timeScale = tmpTimeScale;
			isPaused = false;

		}
	}

	//Pour changer la vitesse de base du jeu
	public void changeSpeed(float speed) {
		Time.timeScale = speed;
	}

	//Le joueur perd de la vie
	public void damagePlayer(int damage) {
		playerHp -= damage;
		if (playerHp <= 0)
			gameOver();
		else
			Debug.Log ("Il reste au joueur " + playerHp + "hp");
	}

	//On pause le jeu en cas de game over
	public void gameOver() {
		Time.timeScale = 0;
		displayEndGameMenu(hasWon: false);
		Debug.Log ("Game Over");
	}

	public void displayEndGameMenu(bool hasWon)
    {
		EndGame endGameScript = endGameMenu.GetComponent<EndGame>();
		endGameScript.score.text = score.ToString();
		giveGrade();
		endGameScript.annoucement.text = hasWon ? "Victory !" : "Game Over";
		endGameScript.actionButtonText.text = hasWon ? "Next level" : "Retry ?";
		UnityEngine.Events.UnityAction action;
		if (hasWon)
			action = NextLevel;
		else
			action = Retry;
		endGameScript.action.GetComponent<Button>().onClick.AddListener(
			action);
		endGameMenu.SetActive(true);

	}

	void giveGrade()
    {
		Text grade = endGameMenu.GetComponent<EndGame>().grade;
		float gradeScore = ((float)playerHp / playerMaxHp + (float)playerEnergy / playerStartEnergy) / 2f;
		print(gradeScore);
		if (gradeScore < 0.25f)
			grade.text = "F";
		else if (gradeScore < 0.5f)
			grade.text = "E";
		else if (gradeScore < 0.75f)
			grade.text = "D";
		else if (gradeScore < 0.1f)
			grade.text = "C";
		else if (gradeScore < 1.25f)
			grade.text = "B";
		else if (gradeScore < 1.5f)
			grade.text = "A";
		else if (gradeScore < 1.75f)
			grade.text = "S";
		else if (gradeScore < 2f)
			grade.text = "SS";
		else if (gradeScore < 2.25f)
			grade.text = "SSS";
		else
			grade.text = "SSS+";
	}
	void Retry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	void NextLevel()
    {
		SceneManager.LoadScene(nextScene);
    }
}
