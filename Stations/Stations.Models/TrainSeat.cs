﻿using System.ComponentModel.DataAnnotations;

namespace Stations.Models
{
    public class TrainSeat
    {
        public int Id { get; set; }

        [Required]
        public int TrainId { get; set; }

        [Required]
        public Train Train { get; set; }

        [Required]
        public int SeatingClassId { get; set; }

        [Required]
        public SeatingClass SeatingClass { get; set; }

        [Required]
        [Range(0,int.MaxValue)]
        public int Quantity { get; set; }
    }
}