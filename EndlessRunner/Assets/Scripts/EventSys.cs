using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class EventSys : MonoBehaviour
{
    public Camera MainCamera;
    public Character character;
    public Text ScoreText, GameOverText, TutorialText;
    public DeathWall deathwall;
    private string fullPath = Path.GetFullPath("EndlessRunnerHighScore");
    [SerializeField]
    private float CameraSpeed = 5.0f;
    private Character SubChar;
    private Panel Panel, PanelWithSaw, PanelWith2Saw, PanelWithHole, PanelWithTurel, PanelWithHoleAndWall;
    private Panel[] Panels = new Panel[5];
    private System.Random rand = new System.Random();
    private int Score = 0, Shift = 0;
    private Vector3 Position = new Vector3(2 * 21F, -4.26F -0.03125F);
    private void StartGame()
    {
        Reader.GetHighScore(fullPath);
        Vector3 pos = new Vector3(0, 0, -10);
        MainCamera.transform.position = Vector3.MoveTowards(pos, pos, Time.deltaTime);
        pos = new Vector3(-0.25F, -7, -0.03125F);
        deathwall.transform.position = Vector3.MoveTowards(pos, pos, Time.deltaTime);
        Position = new Vector3(2 * 21F, -4.26F - 0.03125F);
        Score = 0;
        Shift = 0;
        pos = new Vector3(-7, -1F, -0.03125F);
        character = Instantiate(SubChar, pos, SubChar.transform.rotation);
        character.Speed = 11.0F;
        character.jumpForce = 10.0F;
        character.FreezeZ();
        for (int i = 0; i < Panels.Length; ++i)
        {
            Destroy(Panels[i].gameObject);
        }
        SetPanels();
    }
    private void Start()
    {
        Reader.GetHighScore(fullPath);
        SetPanels();
        InvokeRepeating("UpScore", 1.0F, 1.0F);
    }
    private void Awake()
    {
        SubChar = Resources.Load<Character>("Character");
        Panel = Resources.Load<Panel>("Panel");
        PanelWithSaw = Resources.Load<Panel>("PanelWithSaw");
        PanelWith2Saw = Resources.Load<Panel>("PanelWith2Saw");
        PanelWithHole = Resources.Load<Panel>("PanelWithHole");
        PanelWithTurel = Resources.Load<Panel>("PanelWithTurel");
        PanelWithHoleAndWall = Resources.Load<Panel>("PanelWithHoleAndWall");
    }
    private void UpScore()
    {
        ++Score;
    }
    private Panel CreatePanel()
    {
        Panel panel = new Panel();
        double randNum = rand.NextDouble();
        if (randNum <= 0.10)
            panel = Instantiate(PanelWithHole, Position, PanelWithHole.transform.rotation);
        if (randNum > 0.10 && randNum <= 0.25)
            panel = Instantiate(PanelWithHoleAndWall, Position, PanelWithHoleAndWall.transform.rotation);
        if (randNum > 0.25 && randNum <= 0.50)
            panel = Instantiate(PanelWithTurel, Position, PanelWithTurel.transform.rotation);
        if (randNum > 0.50 && randNum <= 0.75)
            panel = Instantiate(PanelWithSaw, Position, PanelWithSaw.transform.rotation);
        if (randNum > 0.75)
            panel = Instantiate(PanelWith2Saw, Position, PanelWith2Saw.transform.rotation);
        return panel;
    }
    private void SetPanels()
    {
        for (int i = 0; i < Panels.Length; ++i)
        {
            Panels[i] = CreatePanel();
            Position.x += 21F;
        }
    }
    private void CheckPanels()
    {
        double randNum = rand.NextDouble();
        var distance = MainCamera.transform.position - Panels[Shift].transform.position;
        distance.y = 0;
        if (distance.sqrMagnitude > 50 * 50)
        {
            Destroy(Panels[Shift].gameObject);
            Panels[Shift] = CreatePanel();
            Position.x += 21F;
            ++Shift;
            if (Shift == Panels.Length)
                Shift = 0;
        }
    }
    private void MoveCamera()
    {
        Vector3 direction = transform.right;
        MainCamera.transform.position += Vector3.MoveTowards(transform.position, transform.position + direction, CameraSpeed * Time.deltaTime);
    }
    void Update ()
    {
        if (Score > 3)
            TutorialText.enabled = false;
        if (character != null)
        {
            ScoreText.text = "SCORE: " + Score + '\n' + "HIGH SCORE: " + Reader.HighScore;
            MoveCamera();
            var distance = MainCamera.transform.position - character.transform.position;
            distance.y = 0;
            if (distance.sqrMagnitude > (20 * 20))
            {
                Destroy(character.gameObject);
            }
            GameOverText.enabled = false;
            CheckPanels();
        }
        else
        {
            GameOverText.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space))
                StartGame();
            if (Score > Reader.HighScore)
                Reader.SetHighScore(fullPath, Score);
        }
	}
}
