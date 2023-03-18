using AutoMapper;
using TicTacToe.Contracts.Commands;
using TicTacToe.Contracts.Responses;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Application.Mappers
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Game, GameResponse>()
				.ForMember(dest => dest.BluePlayer,
					opt => opt.MapFrom(src => src.BluePlayer))
				.ForMember(dest => dest.RedPlayer,
					opt => opt.MapFrom(src => src.RedPlayer));

			CreateMap<Player, PlayerResponse>()
				.ForMember(dest => dest.Games, opt =>
				{
					opt.MapFrom(src => src.GetGames());
				});

			CreateMap<CreateGameCommand, Game>();
		}
	}
}
