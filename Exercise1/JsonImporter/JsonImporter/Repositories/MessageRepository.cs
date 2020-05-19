using JsonImporter.Database;
using JsonImporter.Models;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace JsonImporter.Repositories
{
    internal class MessageRepository
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private static readonly string connectionString =
            "Host=localhost;Port=5432;Database=JsonImporter;Username=postgres;Password=06090609";

        public static int SaveMessagesEF(List<Message> messages)
        {
            var timer = new Stopwatch();

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    timer.Start();
                    db.Messages.AddRange(messages);
                    int messagesAdded = db.SaveChanges();
                    timer.Stop();

                    Console.WriteLine($"{messagesAdded} rows was successfully added to database. Elapsed time: {timer.ElapsedMilliseconds} ms.");
                    logger.Info($"{messagesAdded} rows was successfully added to database. Elapsed time: {timer.ElapsedMilliseconds} ms.");

                    return messagesAdded;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while adding messages to database: " + ex);
                logger.Error("Error while adding messages to database: " + ex);

                return 0;
            }
        }

        public static void SaveMessagesBulk(List<Message> messages)
        {
            try
            {
                BulkInsertMessageBinary(messages);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while adding messages to database: " + ex);
                logger.Error("Error while adding messages to database: " + ex);
            }
        }

        public static void BulkInsertMessageBinary(List<Message> messages)
        {
            var clock = new Stopwatch();

            var messagesColumns = "\"Type\"";

            string detailsColumns =
                "\"TeamName\", \"TeamNameInternational\", \"ExternalId\", \"InternationalReference\", " +
                "\"TeamNickname\", \"TeamCode\", \"TeamCodeLong\", \"TeamCodeInternational\", " +
                "\"TeamCodeLongInternational\", \"TeamNicknameInternational\", \"CountryCode\", \"CountryCodeIOC\", " +
                "\"Country\", \"Website\", \"IsHomeCompetitor\"";
            
            string coachesColumns = "\"FamilyName\", \"FirstName\", \"InternationalFamilyName\", " +
                "\"InternationalFirstName\", \"ScoreboardName\", \"TVName\", \"NickName\", " +
                "\"ExternalId\", \"NationalityCode\", \"NationalityCodeIOC\", \"Nationality\"";
            
            string teamsColumns = "\"TeamNumber\", \"DetailId\", \"CoachId\", \"AssistCoachId1\", " +
                "\"AssistCoachId2\", \"MessageId\"";

            string playersColumns = "\"Pno\", \"FamilyName\", \"FirstName\", \"InternationalFamilyName\", " +
                "\"InternationalFirstName\", \"ScoreboardName\", \"TVName\", \"NickName\", " +
                "\"Website\", \"DateOfBirth\", \"Height\", \"ExternalId\"," +
                "\"InternationalReference\", \"ShirtNumber\", \"PlayingPosition\", \"Starter\"," +
                "\"Captain\", \"Active\", \"NationalityCode\", \"NationalityCodeIOC\", \"Nationality\", \"TeamId\"";

            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using (var cmd = new NpgsqlCommand($"SET search_path TO public", connection))
                cmd.ExecuteNonQuery();

            clock.Start();

            //Details import
            using (var importer = connection.BeginBinaryImport($"COPY \"public\".\"Details\" ({detailsColumns}) FROM STDIN (FORMAT BINARY)"))
            {
                foreach (var detail in FilterDetails(messages))
                {
                    importer.StartRow();
                    importer.Write(detail.TeamName, NpgsqlDbType.Text);
                    importer.Write(detail.TeamCodeInternational, NpgsqlDbType.Text);
                    importer.Write(detail.ExternalId, NpgsqlDbType.Text);
                    importer.Write(detail.InternationalReference, NpgsqlDbType.Text);
                    importer.Write(detail.TeamNickname, NpgsqlDbType.Text);
                    importer.Write(detail.TeamCode, NpgsqlDbType.Text);
                    importer.Write(detail.TeamCodeLong, NpgsqlDbType.Text);
                    importer.Write(detail.TeamCodeInternational, NpgsqlDbType.Text);
                    importer.Write(detail.TeamCodeLongInternational, NpgsqlDbType.Text);
                    importer.Write(detail.TeamNicknameInternational, NpgsqlDbType.Text);
                    importer.Write(detail.CountryCode, NpgsqlDbType.Text);
                    importer.Write(detail.CountryCodeIOC, NpgsqlDbType.Text);
                    importer.Write(detail.Country, NpgsqlDbType.Text);
                    importer.Write(detail.Website, NpgsqlDbType.Text);
                    importer.Write((int)detail.IsHomeCompetitor, NpgsqlDbType.Smallint);
                }

                importer.Complete();
            }

            //Coaches import
            using (var importer = connection.BeginBinaryImport($"COPY \"public\".\"Coaches\" ({coachesColumns}) FROM STDIN (FORMAT BINARY)"))
            {
                foreach (var coach in FilterCoach(messages))
                {
                    importer.StartRow();
                    importer.Write(coach.FamilyName, NpgsqlDbType.Text);
                    importer.Write(coach.FirstName, NpgsqlDbType.Text);
                    importer.Write(coach.InternationalFamilyName, NpgsqlDbType.Text);
                    importer.Write(coach.InternationalFirstName, NpgsqlDbType.Text);
                    importer.Write(coach.ScoreboardName, NpgsqlDbType.Text);
                    importer.Write(coach.TVName, NpgsqlDbType.Text);
                    importer.Write(coach.NickName, NpgsqlDbType.Text);
                    importer.Write(coach.ExternalId, NpgsqlDbType.Text);
                    importer.Write(coach.NationalityCode, NpgsqlDbType.Text);
                    importer.Write(coach.NationalityCodeIOC, NpgsqlDbType.Text);
                    importer.Write(coach.Nationality, NpgsqlDbType.Text);                    
                }

                importer.Complete();
            }

            //Messages import
            using (var importer = connection.BeginBinaryImport($"COPY \"public\".\"Messages\" ({messagesColumns}) FROM STDIN (FORMAT BINARY)"))
            {
                foreach (var message in messages)
                {
                    importer.StartRow();
                    importer.Write(message.Type, NpgsqlDbType.Text);
                }

                importer.Complete();
            }

            //Teams import
            using (var importer = connection.BeginBinaryImport($"COPY \"public\".\"Teams\" ({teamsColumns}) FROM STDIN (FORMAT BINARY)"))
            {
                foreach (var team in FilterTeams(messages))
                {
                    importer.StartRow();
                    importer.Write(team.TeamNumber, NpgsqlDbType.Integer);
                    importer.Write(team.Detail.DetailId, NpgsqlDbType.Integer);
                    importer.Write(team.Coach.PersonId, NpgsqlDbType.Integer);
                    importer.Write(team.AssistCoach1.PersonId, NpgsqlDbType.Integer);
                    importer.Write(team.AssistCoach2.PersonId, NpgsqlDbType.Integer);
                    importer.Write(team.MessageId, NpgsqlDbType.Integer);
                }

                importer.Complete();
            }

            //Players import
            using (var importer = connection.BeginBinaryImport($"COPY \"public\".\"Players\" ({playersColumns}) FROM STDIN (FORMAT BINARY)"))
            {
                foreach (var player in FilterPlayers(messages))
                {
                    importer.StartRow();
                    importer.Write(player.Pno, NpgsqlDbType.Integer);
                    importer.Write(player.FamilyName, NpgsqlDbType.Text);
                    importer.Write(player.FirstName, NpgsqlDbType.Text);
                    importer.Write(player.InternationalFamilyName, NpgsqlDbType.Text);
                    importer.Write(player.InternationalFirstName, NpgsqlDbType.Text);
                    importer.Write(player.ScoreboardName, NpgsqlDbType.Text);
                    importer.Write(player.TVName, NpgsqlDbType.Text);
                    importer.Write(player.NickName, NpgsqlDbType.Text);
                    importer.Write(player.Website, NpgsqlDbType.Text);
                    importer.Write(player.DateOfBirth, NpgsqlDbType.Timestamp);
                    importer.Write(player.Height, NpgsqlDbType.Double);
                    importer.Write(player.ExternalId, NpgsqlDbType.Text);
                    importer.Write(player.InternationalReference, NpgsqlDbType.Text);
                    importer.Write(player.ShirtNumber, NpgsqlDbType.Text);
                    importer.Write(player.PlayingPosition, NpgsqlDbType.Text);
                    importer.Write((int)player.Starter, NpgsqlDbType.Smallint);
                    importer.Write((int)player.Captain, NpgsqlDbType.Smallint);
                    importer.Write((int)player.Active, NpgsqlDbType.Smallint);
                    importer.Write(player.NationalityCode, NpgsqlDbType.Text);
                    importer.Write(player.NationalityCodeIOC, NpgsqlDbType.Text);
                    importer.Write(player.Nationality, NpgsqlDbType.Text);
                    importer.Write(player.TeamId, NpgsqlDbType.Integer);
                }

                importer.Complete();
            }

            clock.Stop();

            Console.WriteLine($"Messages was successfully added to database. Elapsed time: {clock.ElapsedMilliseconds} ms.");
            logger.Info($"Messages was successfully added to database. Elapsed time: {clock.ElapsedMilliseconds} ms.");
        }

        public static List<Coach> FilterCoach(List<Message> messages)
        {
            List<Coach> coaches = new List<Coach>();

            foreach (Message message in messages)
            {
                coaches.Add(message.Teams[0].Coach);
                coaches.Add(message.Teams[0].AssistCoach1);
                coaches.Add(message.Teams[0].AssistCoach2);

                coaches.Add(message.Teams[1].Coach);
                coaches.Add(message.Teams[1].AssistCoach1);
                coaches.Add(message.Teams[1].AssistCoach2);
            }

            return coaches;
        }

        public static List<Team> FilterTeams(List<Message> messages)
        {
            List<Team> teams = new List<Team>();

            foreach (Message message in messages)
            {
                teams.AddRange(message.Teams);
            }

            return teams;
        }

        public static List<Player> FilterPlayers(List<Message> messages)
        {
            List<Player> players = new List<Player>();

            foreach (Message message in messages)
            {
                players.AddRange(message.Teams[0].Players);
                players.AddRange(message.Teams[1].Players);
            }

            return players;
        }

        public static List<Detail> FilterDetails(List<Message> messages)
        {
            List<Detail> details = new List<Detail>();

            foreach (Message message in messages)
            {
                details.Add(message.Teams[0].Detail);
                details.Add(message.Teams[1].Detail);
            }

            return details;
        }
    }
}