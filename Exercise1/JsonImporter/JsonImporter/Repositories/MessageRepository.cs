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
            var messageColumns = "\"Type\"";
            string detailColumns =
                "\"TeamName\", \"TeamNameInternational\", \"ExternalId\", \"InternationalReference\", " +
                "\"TeamNickname\", \"TeamCode\", \"TeamCodeLong\", \"TeamCodeInternational\", " +
                "\"TeamCodeLongInternational\", \"TeamNicknameInternational\", \"CountryCode\", \"CountryCodeIOC\", " +
                "\"Country\", \"Website\", \"IsHomeCompetitor\""; 

            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using (var cmd = new NpgsqlCommand($"SET search_path TO public", connection))
                cmd.ExecuteNonQuery();

            clock.Start();

            //Details import
            using (var importer = connection.BeginBinaryImport($"COPY \"public\".\"Details\" ({detailColumns}) FROM STDIN (FORMAT BINARY)"))
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

            //Messages import
            using (var importer = connection.BeginBinaryImport($"COPY \"public\".\"Messages\" ({messageColumns}) FROM STDIN (FORMAT BINARY)"))
            {
                foreach (var message in messages)
                {
                    importer.StartRow();
                    importer.Write(message.Type, NpgsqlDbType.Text);
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