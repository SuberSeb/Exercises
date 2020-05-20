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

        private static int messageId = 1;
        private static int teamId = 1;
        private static int detailId = 1;
        private static int coachId = 1;
        private static int playerId = 1;

        private List<Player> CreatePlayersList(int numberOfPlayers)
        {
            List<Player> players = new List<Player>();

            for (int i = playerId; i < playerId + numberOfPlayers; i++)
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

            playerId += numberOfPlayers;

            return players;
        }

        private Coach CreateCoach()
        {
            Coach coach = new Coach
            {
                PersonId = coachId,
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

            coachId++;

            return coach;
        }

        private Detail CreateDetail()
        {
            Detail detail = new Detail
            {
                DetailId = detailId,
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

            detailId++;

            return detail;
        }

        private List<Team> CreateTeams()
        {
            List<Team> teams = new List<Team>()
            {
                new Team
                {
                    TeamId = teamId,
                    TeamNumber = 1,
                    Detail = CreateDetail(),
                    Players = CreatePlayersList(10),
                    Coach = CreateCoach(),
                    AssistCoach1 = CreateCoach(),
                    AssistCoach2 = CreateCoach()
                },
                new Team
                {
                    TeamId = teamId + 1,
                    TeamNumber = 2,
                    Detail = CreateDetail(),
                    Players = CreatePlayersList(10),
                    Coach = CreateCoach(),
                    AssistCoach1 = CreateCoach(),
                    AssistCoach2 = CreateCoach()
                }};

            teamId += 2;

            return teams;
        }

        private List<Message> CreateMessages(int numberOfMessages)
        {
            List<Message> messages = new List<Message>();

            for (int i = messageId; i < messageId + numberOfMessages; i++)
            {
                messages.Add(
                    new Message
                    {
                        MessageId = i,
                        Type = $"teams",
                        Teams = CreateTeams()
                    });
            }

            messageId += numberOfMessages;

            return messages;
        }

        public static void CreateIds()
        {
            using (var db = new ApplicationDbContext())
            {
                if (db.Players.OrderByDescending(player => player.PersonId).FirstOrDefault() != null)
                    playerId = 1 + db.Players
                        .OrderByDescending(player => player.PersonId)
                        .FirstOrDefault().PersonId;

                if (db.Coaches.OrderByDescending(coach => coach.PersonId).FirstOrDefault() != null)
                    coachId = 1 + db.Coaches
                        .OrderByDescending(coach => coach.PersonId)
                        .FirstOrDefault().PersonId;

                if (db.Details.OrderByDescending(detail => detail.DetailId).FirstOrDefault() != null)
                    detailId = 1 + db.Details
                        .OrderByDescending(detail => detail.DetailId)
                        .FirstOrDefault().DetailId;

                if (db.Teams.OrderByDescending(team => team.TeamId).FirstOrDefault() != null)
                    teamId = 1 + db.Teams
                        .OrderByDescending(team => team.TeamId)
                        .FirstOrDefault().TeamId;

                if (db.Messages.OrderByDescending(message => message.MessageId).FirstOrDefault() != null)
                    messageId = 1 + db.Messages
                        .OrderByDescending(message => message.MessageId)
                        .FirstOrDefault().MessageId;
            };
        }

        public string Generate(int numberOfMessages)
        {
            string jsonResult = String.Empty;

            CreateIds();

            if (numberOfMessages >= 1)
            {
                try
                {
                    var messages = CreateMessages(numberOfMessages);
                    jsonResult = JsonConvert.SerializeObject(messages);
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