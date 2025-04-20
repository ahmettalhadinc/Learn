using AutoMapper;
using Learn.Core.DTO;
using Learn.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Service.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Customer,CustomerDto>().ReverseMap();
            CreateMap<Department,DepartmentDto>().ReverseMap();
            CreateMap<Group,GroupDto>().ReverseMap();
            CreateMap<GroupInRole,GroupInRoleDto>().ReverseMap();
            CreateMap<Payment,PaymentDto>().ReverseMap();
            CreateMap<Product,ProductDto>().ReverseMap();
            CreateMap<Role,RoleDto>().ReverseMap();
            CreateMap<Sale,SaleDto>().ReverseMap();
            CreateMap<User,UserDto>().ReverseMap();
        }
    }
}
