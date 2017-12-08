﻿using System.Linq;
using AutoMapper;
using Stations.DataProcessor.Dto.Import;
using Stations.Models;

namespace Stations.App
{
	public class StationsProfile : Profile
	{
		// Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
		public StationsProfile()
		{
            CreateMap<StationDto, Station>();
            CreateMap<SeatingClassDto, SeatingClass>();
		}
	}
}
