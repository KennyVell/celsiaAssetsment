using AutoMapper;
using celsiaAssetsment.DTOs;
using celsiaAssetsment.Models;

namespace celsiaAssetsment.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AdminDTO, Admin>();
            CreateMap<Admin, AdminDTO>().ReverseMap();

            CreateMap<ClientDTO, Client>();
            CreateMap<Client, ClientDTO>().ReverseMap();

            CreateMap<InvoiceDTO, Invoice>();
            CreateMap<Invoice, InvoiceDTO>().ReverseMap();

            CreateMap<TransactionDTO, Transaction>();
            CreateMap<Transaction, TransactionDTO>().ReverseMap();

            CreateMap<PlatformDTO, Platform>();
            CreateMap<Platform, PlatformDTO>().ReverseMap();
        }
    }
}