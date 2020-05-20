using JsonImporter.Database;
using JsonImporter.Models;
using JsonImporter.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonImporter.Json
{
    public class JsonGenerator
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly RandomGenerator generator = new RandomGenerator();

        private List<Player> CreatePlayersList(int numberOfPlayers)
        {
            List<Player> players = new List<Player>();
            int lastInsertedId = 0;

            using var db = new ApplicationDbContext();
            if (db.Players.OrderByDescending(player => player.PersonId).FirstOrDefault() == null)
                lastInsertedId = 1;
            else
                lastInsertedId = db.Players
                    .OrderByDescending(player => player.PersonId)
                    .FirstOrDefault().PersonId;


            for (int i = lastInsertedId; i < numberOfPlayers + 1; i++)
            {
                Player player = new Player
                {
                    PersonId = i,
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
            int lastInsertedId = 0;

            using var db = new ApplicationDbContext();
            if (db.Coaches.OrderByDescending(coach => coach.PersonId).FirstOrDefault() == null)
                lastInsertedId = 1;
            else
                lastInsertedId = db.Coaches
                    .OrderByDescending(coach => coach.PersonId)
                    .FirstOrDefault().PersonId;

            Coach coach = new Coach
            {
                PersonId = lastInsertedId,
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
            int lastInsertedId = 0;

            using var db = new ApplicationDbContext();
            if (db.Details.OrderByDescending(detail => detail.DetailId).FirstOrDefault() == null)
                lastInsertedId = 1;
            else
                lastInsertedId = db.Details
                    .OrderByDescending(detail => detail.DetailId)
                    .FirstOrDefault().DetailId;

            Detail detail = new Detail
            {
                DetailId = lastInsertedId,
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
            int lastInsertedId = 0;

            using var db = new ApplicationDbContext();
            if (db.Teams.OrderByDescending(team => team.TeamId).FirstOrDefault() == null)
                lastInsertedId = 1;
            else
                lastInsertedId = db.Teams
                    .OrderByDescending(team => team.TeamId)
                    .FirstOrDefault().TeamId;

            List<Team> teams = new List<Team>()
            {
                new Team
                {
                    TeamId = lastInsertedId,
                    TeamNumber = 1,
                    Detail = CreateDetail(),
                    Players = CreatePlayersList(10),
                    Coach = CreateCoach(),
                    AssistCoach1 = CreateCoach(),
                    AssistCoach2 = CreateCoach()
                },
                new Team
                {
                    TeamId = lastInsertedId + 1,
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
            int lastInsertedId = 0;

            using var db = new ApplicationDbContext();
            if (db.Messages.OrderByDescending(message => message.MessageId).FirstOrDefault() == null)
                lastInsertedId = 1;
            else
                lastInsertedId = db.Messages
                    .OrderByDescending(message => message.MessageId)
                    .FirstOrDefault().MessageId;

            for (int i = lastInsertedId; i < numberOfMessages + 1; i++)
            {
                messages.Add(
                    new Message 
                    { 
                        MessageId = i,
                        Type = $"teams", 
                        Teams = CreateTeams() 
                    });
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