using AutoMapper;
using Learn.API.Filters;
using Learn.Core.DTO.UpdateDTOs;
using Learn.Core.DTO;
using Learn.Core.Models;
using Learn.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Learn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : CustomBaseController
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        public GroupsController(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles="Root,Root.Groups,Root.Groups.Get")]
        public async Task<IActionResult> All()
        {
            var groups = _groupService.GetAll();
            var dtos = _mapper.Map<List<GroupDto>>(groups).ToList();
            return CreateActionResult(CustomResponseDto<List<GroupDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Group>))]
        [HttpGet("{id}")]
        [Authorize(Roles = "Root,Root.Groups,Root.Groups.Get")]
        public async Task<IActionResult> GetById(int id)
        {
            var group = await _groupService.GetByIdAsync(id);
            var groupDto = _mapper.Map<GroupDto>(group);
            return CreateActionResult(CustomResponseDto<GroupDto>.Success(200, groupDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Group>))]
        [HttpGet("[action]")]
        [Authorize(Roles = "Root,Root.Groups,Root.Groups.Delete")]
        public async Task<IActionResult> Remove(int id)
        {
            int userId = 1;
            var group = await _groupService.GetByIdAsync(id);
            group.UpdatedBy = userId;
            _groupService.ChangeStatus(group);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        [Authorize(Roles = "Root,Root.Groups,Root.Groups.Add")]
        public async Task<IActionResult> Save(GroupDto groupDto)
        {
            int userId = 1;
            var processedEntity = _mapper.Map<Group>(groupDto);

            processedEntity.UpdatedBy = userId;
            processedEntity.CreatedBy = userId;

            var group = await _groupService.AddAsync(processedEntity);
            var groupResponseDto = _mapper.Map<GroupDto>(group);
            return CreateActionResult(CustomResponseDto<GroupDto>.Success(201, groupDto));

        }
        [HttpPut]

        public async Task<IActionResult> Update(GroupUpdateDto groupDto)
        {
            var userId = 1;

            var currentGroup = await _groupService.GetByIdAsync(groupDto.Id);
            currentGroup.UpdatedBy = userId;
            currentGroup.Name = groupDto.Name;
            _groupService.Update(currentGroup);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
