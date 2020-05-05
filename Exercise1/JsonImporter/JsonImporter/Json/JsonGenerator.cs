﻿using JsonImporter.Models;
using JsonImporter.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace JsonImporter.Json
{
    internal class JsonGenerator
    {
        private readonly RandomGenerator generator = new RandomGenerator();

        private List<Player> CreatePlayersList(int numberOfPlayers)
        {
            List<Player> players = new List<Player>();

            for (int i = 0; i < numberOfPlayers; i++)
            {
                Player player = new Player
                {
                    Pno = generator.RandomNumber(1, 100),
                    PersonId = generator.RandomNumber(1, 1000),
                    FamilyName = generator.RandomString(10, false),
                    FirstName = generator.RandomString(15, false),
                    InternationalFamilyName = generator.RandomString(15, false),
                    InternationalFirstName = generator.RandomString(15, false),
                    ScoreboardName = generator.RandomString(15, false),
                    TVName = generator.RandomString(15, false),
                    NickName = generator.RandomString(15, false),
                    Website = generator.RandomString(15, false),
                    DateOfBirth = new DateTime(generator.RandomNumber(1970, 2000), generator.RandomNumber(1, 12), generator.RandomNumber(1, 28)),
                    Height = generator.RandomNumber(1, 3),
                    ExternalId = generator.RandomString(15, false),
                    InternationalReference = generator.RandomString(15, false),
                    ShirtNumber = generator.RandomString(15, false),
                    PlayingPosition = generator.RandomString(15, false),
                    Starter = generator.RandomEnum(40),
                    Captain = generator.RandomEnum(10),
                    Active = generator.RandomEnum(60),
                    NationalityCode = generator.RandomString(15, false),
                    NationalityCodeIOC = generator.RandomString(15, false),
                    Nationality = generator.RandomString(15, false)
                };

                players.Add(player);
            }

            return players;
        }

        private Coach CreateCoach()
        {
            Coach coach = new Coach
            {
                PersonId = generator.RandomNumber(1, 100),
                FamilyName = generator.RandomString(10, false),
                FirstName = generator.RandomString(15, false),
                InternationalFamilyName = generator.RandomString(15, false),
                InternationalFirstName = generator.RandomString(15, false),
                ScoreboardName = generator.RandomString(15, false),
                TVName = generator.RandomString(15, false),
                NickName = generator.RandomString(15, false),
                ExternalId = generator.RandomString(15, false),
                NationalityCode = generator.RandomString(15, false),
                NationalityCodeIOC = generator.RandomString(15, false),
                Nationality = generator.RandomString(15, false)
            };

            return coach;
        }

        private Detail CreateDetail()
        {
            Detail detail = new Detail
            {
                TeamName = generator.RandomString(10, false),
                TeamNameInternational = generator.RandomString(10, false),
                TeamId = generator.RandomNumber(1, 1000),
                ExternalId = generator.RandomString(10, false),
                InternationalReference = generator.RandomString(10, false),
                TeamNickname = generator.RandomString(10, false),
                TeamCode = generator.RandomString(10, false),
                TeamCodeLong = generator.RandomString(10, false),
                TeamCodeInternational = generator.RandomString(10, false),
                TeamCodeLongInternational = generator.RandomString(10, false),
                TeamNicknameInternational = generator.RandomString(10, false),
                CountryCode = generator.RandomString(10, false),
                CountryCodeIOC = generator.RandomString(10, false),
                Country = generator.RandomString(10, false),
                Website = generator.RandomString(10, false),
                IsHomeCompetitor = generator.RandomEnum(55)
            };

            return detail;
        }

        private Team[] CreateTeams()
        {
            return new Team[]
            {
                new Team
                {
                    TeamNumber = 1,
                    Detail = CreateDetail(),
                    Players = CreatePlayersList(10),
                    Coach = CreateCoach(),
                    AssistCoach1 = CreateCoach(),
                    AssistCoach2 = CreateCoach()
                },
                new Team
                {
                    TeamNumber = 2,
                    Detail = CreateDetail(),
                    Players = CreatePlayersList(10),
                    Coach = CreateCoach(),
                    AssistCoach1 = CreateCoach(),
                    AssistCoach2 = CreateCoach()
                }
            };
        }

        private List<Message> CreateMessages()
        {
            List<Message> messages = new List<Message>();

            for (int i = 0; i < 10; i++)
            {
                messages.Add(new Message { MessageId = i, Type = "Message type", Teams = CreateTeams() });
            }

            return messages;
        }

        public void GenerateJson(string path)
        {
            string jsonResult = JsonConvert.SerializeObject(CreateMessages());

            try
            {
                using var writer = new StreamWriter(path, false);
                writer.WriteLine(jsonResult.ToString());
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}