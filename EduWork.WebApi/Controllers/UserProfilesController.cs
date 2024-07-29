using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EduWork.DataAccessLayer;
using EduWork.DataAccessLayer.Entites;
using EduWork.BusinessLayer.Services;
using Common.DTO.Profile;
using EduWork.BusinessLayer.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;
using Common.DTO.ProjectTime;
using EduWork.WebApi.Authentication;

namespace EduWork.WebApi.Controllers
{
    [Authorize]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfilesController : ControllerBase
    {
        private readonly UserProfileService _userProfileService;

        public UserProfilesController(UserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpGet("currentProject")]
        public async Task<ActionResult<CurrentUserProjectDto>> GetUserCurrentProject([FromServices] IIdentity currentUser)
        {
            var result = await _userProfileService.GetUserCurrentProject(currentUser.Email);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfileDto>>> GetUsers()
        {
            return await _userProfileService.GetUsers();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfileDto>> GetUser(int id)
        {
            var result = await _userProfileService.GetUser(id);
            return Ok(result);
        }

        [HttpGet("sickLeave/{id}")]
        public async Task<ActionResult<IEnumerable<SickLeaveRecordDto>>> GetUserSickLeaveRecords(int id)
        {
            var result = await _userProfileService.GetUserSickLeaveRecords(id);
            return Ok(result);
        }

        [HttpGet("annualLeave/{id}")]
        public async Task<ActionResult<IEnumerable<AnnualLeaveDto>>> GetUserAnnualLeaves(int id)
        {
            var result = await _userProfileService.GetUserAnnualLeaves(id);
            return Ok(result);
        }

        [HttpGet("annualLeaveRecords/{id}")]
        public async Task<ActionResult<IEnumerable<AnnualLeaveRecordDto>>> GetUserAnnualLeaveRecords(int id)
        {
            var result = await _userProfileService.GetUserAnnualLeaveRecords(id);
            return Ok(result);
        }

        [HttpGet("myProfile")]
        public async Task<ActionResult<MyProfileDto>> GetUserProfile([FromServices] IIdentity currentUser)
        {
            var result = await _userProfileService.GetUserProfile(currentUser.Email);
            return Ok(result);
        }

        [HttpGet("myStats")]
        public async Task<ActionResult<MyProfileStatsDto>> GetMyProfileStats([FromServices] IIdentity currentUser, bool thisMonth, bool lastMonth)
        {
            var result = await _userProfileService.GetMyProfileStats(currentUser.Email, thisMonth, lastMonth);
            return Ok(result);
        }
    }
}
