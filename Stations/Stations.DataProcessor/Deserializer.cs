namespace Stations.DataProcessor
{
    using System;
    using Stations.Data;
    using System.Text;
    using Newtonsoft.Json;
    using Models;
    using Dto.Import;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Stations.Models.Enums;
    using System.Globalization;
    using System.Xml.Serialization;
    using System.IO;
    using Microsoft.EntityFrameworkCore;

    public static class Deserializer
	{
		private const string FailureMessage = "Invalid data format.";
		private const string SuccessMessage = "Record {0} successfully imported.";

		public static string ImportStations(StationsDbContext context, string jsonString)
		{
            StringBuilder sb = new StringBuilder();

            var deserializeStations = JsonConvert.DeserializeObject<StationDto[]>(jsonString);

            var validStations = new List<Station>();

            foreach (var stationDto in deserializeStations)
            {
                if (!isValid(stationDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                if (stationDto.Town==null)
                {
                    stationDto.Town = stationDto.Name;
                }

                var stationExists = validStations.Any(s => s.Name == stationDto.Name);
                if (stationExists)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var station = AutoMapper.Mapper.Map<Station>(stationDto);

                validStations.Add(station);

                sb.AppendLine(String.Format(SuccessMessage, stationDto.Name));
            }

            context.Stations.AddRange(validStations);
            context.SaveChanges();

            string result = sb.ToString();
            return result;
		}

        public static string ImportClasses(StationsDbContext context, string jsonString)
		{
            var deserializeClasses = JsonConvert.DeserializeObject<SeatingClassDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            var validClasses = new List<SeatingClass>();

            foreach (var seatingClassDto in deserializeClasses)
            {
                if (!isValid(seatingClassDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var classExists = validClasses.Any(c => c.Name == seatingClassDto.Name
                || c.Abbreviation==seatingClassDto.Abbreviation);
                if (classExists)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var seatingClass = AutoMapper.Mapper.Map<SeatingClass>(seatingClassDto);

                validClasses.Add(seatingClass);

                sb.AppendLine(String.Format(SuccessMessage, seatingClassDto.Name));
            }

            context.SeatingClasses.AddRange(validClasses);
            context.SaveChanges();

            string result = sb.ToString();
            return result;
		}

		public static string ImportTrains(StationsDbContext context, string jsonString)
		{
            var deserializeTrains = JsonConvert.DeserializeObject<TrainDto[]>(jsonString
                ,new JsonSerializerSettings
                {
                    NullValueHandling=NullValueHandling.Ignore
                });

            StringBuilder sb = new StringBuilder();

            var validTrains = new List<Train>();

            foreach (var trainDto in deserializeTrains)
            {
                var trainType = Enum.Parse<TrainType>(trainDto.Type);

                if (!isValid(trainDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var trainNumberExists = validTrains.Any(t => t.TrainNumber == trainDto.TrainNumber);

                if (trainNumberExists)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var validSeats = trainDto.Seats.All(isValid);
                if (!validSeats)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var validClass = trainDto.Seats
                    .All(s => context.SeatingClasses
                        .Any(sc => sc.Name == s.Name && sc.Abbreviation == s.Abbreviation));

                if (!validClass)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var trainSeats = trainDto.Seats.Select(s => new TrainSeat
                {
                    SeatingClass = context.SeatingClasses
                      .SingleOrDefault(sc => sc.Name == s.Name && sc.Abbreviation == s.Abbreviation),
                    Quantity=s.Quantity.Value
                }).ToArray();

                var train = new Train
                {
                    TrainNumber = trainDto.TrainNumber,
                    Type = trainType,
                    TrainSeats = trainSeats
                };

                validTrains.Add(train);

                sb.AppendLine(String.Format(SuccessMessage, trainDto.TrainNumber));
            }
            context.Trains.AddRange(validTrains);
            context.SaveChanges();

            string result = sb.ToString();

            return result;
		}

		public static string ImportTrips(StationsDbContext context, string jsonString)
		{
            var trips = JsonConvert.DeserializeObject<TripDto[]>(jsonString, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var validTrips = new List<Trip>();
            var sb = new StringBuilder();

            foreach (var tripDto in trips)
            {
                if (!isValid(tripDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var train = context.Trains.FirstOrDefault(t => t.TrainNumber == tripDto.Train);
                var originStation = context.Stations.FirstOrDefault(s => s.Name == tripDto.OriginStation);
                var destStation = context.Stations.FirstOrDefault(s => s.Name == tripDto.DestinationStation);

                if (train == null || originStation == null || destStation == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var departureTime = DateTime.ParseExact(tripDto.DepartureTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                var arrivalTime = DateTime.ParseExact(tripDto.ArrivalTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                var status = Enum.Parse<TripStatus>(tripDto.Status);

                TimeSpan timeDifference;
                if (tripDto.TimeDifference != null)
                {
                    timeDifference = TimeSpan.ParseExact(tripDto.TimeDifference, @"hh\:mm", CultureInfo.InvariantCulture);
                }

                if (arrivalTime < departureTime)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var trip = new Trip
                {
                    Train = train,
                    OriginStation = originStation,
                    DestinationStation = destStation,
                    DepartureTime = departureTime,
                    ArrivalTime = arrivalTime,
                    TimeDifference = timeDifference,
                    Status = status
                };

                validTrips.Add(trip);
                sb.AppendLine($"Trip from {tripDto.OriginStation} to {tripDto.DestinationStation} imported.");
            }

            context.Trips.AddRange(validTrips);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

		public static string ImportCards(StationsDbContext context, string xmlString)
		{
            XmlSerializer serializer = new XmlSerializer(typeof(CardDto[]), new XmlRootAttribute("Cards"));
            var deserializeCard= (CardDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            StringBuilder sb = new StringBuilder();

            var validCards = new List<CustomerCard>();

            foreach (var cardDto in deserializeCard)
            {
                if (!isValid(cardDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var cardType = Enum.Parse<CardType>(cardDto.CardType);

                var card = new CustomerCard
                {
                    Name = cardDto.Name,
                    Age = cardDto.Age,
                    Type = cardType
                };

                validCards.Add(card);

                sb.AppendLine(string.Format(SuccessMessage, cardDto.Name));
            }
            context.Cards.AddRange(validCards);
            context.SaveChanges();

            var result = sb.ToString();
            return result;
		}

		public static string ImportTickets(StationsDbContext context, string xmlString)
		{
            var serializer = new XmlSerializer(typeof(TicketDto[]), new XmlRootAttribute("Tickets"));
            var deserializedTickets = (TicketDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            var validTickets = new List<Ticket>();
            var sb = new StringBuilder();

            foreach (var ticketDto in deserializedTickets)
            {
                if (!isValid(ticketDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var departureTime = DateTime.ParseExact(ticketDto.Trip.DepartureTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                var trip = context.Trips
                    .Include(t => t.OriginStation)
                    .Include(t => t.DestinationStation)
                    .Include(t => t.Train)
                    .ThenInclude(tr => tr.TrainSeats)
                    .SingleOrDefault(t => t.DepartureTime == departureTime &&
                                          t.OriginStation.Name == ticketDto.Trip.OriginStation &&
                                          t.DestinationStation.Name == ticketDto.Trip.DestinationStation);

                if (trip == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                CustomerCard card = null;
                if (ticketDto.Card != null)
                {
                    card = context.Cards.SingleOrDefault(c => c.Name == ticketDto.Card.Name);

                    if (card == null)
                    {
                        sb.AppendLine(FailureMessage);
                        continue;
                    }
                }

                var seatingClass = ticketDto.Seat.Substring(0, 2);
                var seatNumber = int.Parse(ticketDto.Seat.Substring(2));

                var seatExists = trip.Train.TrainSeats
                    .SingleOrDefault(s => s.SeatingClass.Abbreviation == seatingClass && s.Quantity >= seatNumber);

                if (seatExists == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var ticket = new Ticket
                {
                    Trip = trip,
                    CustomerCard = card,
                    Price = ticketDto.Price,
                    SeatingPlace = ticketDto.Seat
                };

                var origStation = trip.OriginStation.Name;
                var destStation = trip.DestinationStation.Name;
                var etd = trip.DepartureTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                validTickets.Add(ticket);
                sb.AppendLine($"Ticket from {origStation} to {destStation} departing at {etd} imported.");
            }

            context.Tickets.AddRange(validTickets);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        private static bool isValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}