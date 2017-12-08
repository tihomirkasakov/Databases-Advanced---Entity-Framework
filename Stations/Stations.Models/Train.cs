﻿namespace Stations.Models
{
    using Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Train
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string TrainNumber { get; set; }

        public TrainType? Type { get; set; }

        public ICollection<TrainSeat> TrainSeats { get; set; } = new HashSet<TrainSeat>();
        public ICollection<Trip> Trips { get; set; } = new HashSet<Trip>();
    }
}
