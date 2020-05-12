using JsonImporter.Models;
using JsonImporter.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JsonImporter.Json
{
    public class JsonGenerator
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly RandomGenerator generator = new RandomGenerator();

        private List<Player> CreatePlayersList(int numberOfPlayers)
        {
            List<Player> players = new List<Player>();

            for (int i = 1; i < numberOfPlayers + 1; i++)
            {
                Player player = new Player
                {
                    Pno = generator.RandomNumber(1, 100),
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

        private List<Team> CreateTeams()
        {
            List<Team> teams = new List<Team>() 
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
                }};

            return teams;
        }

        private List<Message> CreateMessages(int numberOfMessages)
        {
            List<Message> messages = new List<Message>();

            for (int i = 0; i < numberOfMessages; i++)
            {
                messages.Add(new Message { Type = "Message type", Teams = CreateTeams() });
            }

            return messages;
        }

        public string Generate(int numberOfMessages)
        {
            string jsonResult = String.Empty;

            if (numberOfMessages >= 1)
            {
                try
                {
                    jsonResult = JsonConvert.SerializeObject(CreateMessages(numberOfMessages));
                    logger.Info("JSON was serialized successfully");
                }
                catch (JsonException ex)
                {
                    logger.Error("Error while serializing JSON: " + ex);
                }
            }
            else
            {
                logger.Error("Invalid number of messages");
            }

            return jsonResult;
        }
    }
}