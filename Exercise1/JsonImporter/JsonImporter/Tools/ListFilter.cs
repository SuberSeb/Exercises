using JsonImporter.Models;
using System.Collections.Generic;

namespace JsonImporter.Tools
{
    internal class ListFilter
    {
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