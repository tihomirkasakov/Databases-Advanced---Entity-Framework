using System;
using Stations.Data;
using System.Globalization;
using System.Linq;
using Stations.Models.Enums;
using Newtonsoft.Json;
using Stations.DataProcessor.Dto.Export;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Stations.DataProcessor
{
    public class Serializer
    {
        public static string ExportDelayedTrains(StationsDbContext context, string dateAsString)
        {
            var date = DateTime.ParseExact(dateAsString, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var trains = context.Trains
                .Where(t => t.Trips.Any(tr => tr.Status == TripStatus.Delayed && tr.DepartureTime <= date))
                .Select(t => new
                {
                    t.TrainNumber,
                    DelayedTrips = t.Trips.Where(tr => tr.Status == TripStatus.Delayed && tr.DepartureTime <= date)
                    .ToArray()
                })
                .Select(t => new TrainDto
                {
                    TrainNumber = t.TrainNumber,
                    DelayedTimes = t.DelayedTrips.Count(),
                    MaxDelayedTime = t.DelayedTrips.Max(tr => tr.TimeDifference).ToString()
                }).OrderByDescending(t => t.DelayedTimes)
                .ThenByDescending(t => t.MaxDelayedTime)
                .ThenBy(t => t.TrainNumber)
                .ToArray();

            var json = JsonConvert.SerializeObject(trains, Newtonsoft.Json.Formatting.Indented);

            return json;
        }

        public static string ExportCardsTicket(StationsDbContext context, string cardType)
        {
            var type = Enum.Parse<CardType>(cardType);
            var cards = context.Cards
                .Where(c => c.Type == type && c.BoughtTickets.Count > 0)
                .OrderBy(c => c.Name);

            var doc = new XDocument(new XElement("Cards"));

            foreach (var c in cards)
            {
                var currentCard = new XElement("Card",
                                        new XAttribute("name", c.Name),
                                        new XAttribute("type", cardType));

                var tickets = new XElement("Tickets");
                foreach (var t in c.BoughtTickets)
                {
                    tickets.Add(new XElement("Ticket",
                                        new XElement("OriginStation", t.Trip.OriginStation.Name),
                                        new XElement("DestinationStation", t.Trip.DestinationStation.Name),
                                        new XElement("DepartureTime", t.Trip.DepartureTime
                                                                       .ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture))));
                }

                currentCard.Add(tickets);
                doc.Root.Add(currentCard);
            }

            var result = doc.ToString();
            return result;
        }
    }
}