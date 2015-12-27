namespace SportSystem.App.App_Start
{
    using AutoMapper;
    using Models.ViewModels;
    using SportSystem.Models;

    public static class MapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Match, ConciseMatchViewModel>()
                .ForMember(vm => vm.HomeTeamName, opt => opt.MapFrom(m => m.HomeTeam.Name))
                .ForMember(vm => vm.AwayTeamName, opt => opt.MapFrom(m => m.AwayTeam.Name));

            Mapper.CreateMap<Team, ConciseTeamViewModel>()
                .ForMember(vm => vm.VotesCount, opt => opt.MapFrom(t => t.Votes.Count));
        }
    }
}