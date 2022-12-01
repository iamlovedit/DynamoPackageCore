using AutoMapper;
using DynamoPackageService.DTO;
using DynamoPackageService.Models;

namespace DynamoPackageService.Infrastructure;

public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<DynamoPackage, DynamoPackageDTO>();
        CreateMap<PackageVersion, PackageVersionDTO>();
    }
}