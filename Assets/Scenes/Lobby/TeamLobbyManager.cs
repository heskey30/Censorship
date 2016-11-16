﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class TeamLobbyManager : NetworkLobbyManager {

    public List<int> ideas;
    public static List<int> playerIdeas;
    List<int> ideasLeft;

    Dictionary<NetworkConnection, int> teamAssignments;

    Dictionary<int, PlayerStartPosition> initialPositions;

    public override void OnLobbyStartServer()
    {
        if (ideas.Count < maxPlayers)
            Debug.LogWarning("Possible to have players on same team! maxPlayers > ideaCount");

        teamAssignments = new Dictionary<NetworkConnection, int>();
        playerIdeas = new List<int>(ideas);
    }
	
	public override GameObject OnLobbyServerCreateLobbyPlayer (NetworkConnection conn, short playerControllerId) {

        if (playerIdeas.Count <= 0)
        {
            Debug.LogWarning("Run out of team assignments! Defaulting to 0");
            teamAssignments.Add(conn, 0);
        }

        int i = Random.Range(0, playerIdeas.Count);

        teamAssignments.Add(conn, playerIdeas[i]);

        playerIdeas.RemoveAt(i);

        return base.OnLobbyServerCreateLobbyPlayer(conn, playerControllerId);
	}

    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
    {
        foreach (PlayerStartPosition p in FindObjectsOfType<PlayerStartPosition>())
        {
            if (p.ideaIndex == teamAssignments[conn])
            {
                GameObject go = (GameObject)GameObject.Instantiate(gamePlayerPrefab, p.transform.position, Quaternion.identity);
                go.GetComponent<Global>().playerIdeaIndex = teamAssignments[conn];

                go.SetActive(true);

                return go;
            }
        }

        Debug.LogWarning("Unable to find start position with correct idea index! player spawn is null");
        return null;
    }
}