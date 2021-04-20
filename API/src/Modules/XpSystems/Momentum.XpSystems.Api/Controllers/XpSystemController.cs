﻿using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Momentum.XpSystems.Api.ViewModels;
using Momentum.XpSystems.Application.Queries;
using Momentum.XpSystems.Application.Commands;
using Momentum.XpSystems.Application.DTOs;
using Momentum.XpSystems.Application.DTOs.Rank;
using Momentum.XpSystems.Application.DTOs.Cosmetic;

namespace Momentum.XpSystems.Api.Controllers
{
    [Route("api/admin/xpsys")]
    [ApiController]
    public class XpSystemController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public XpSystemController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetXpSystemAsync()
        {
            var xpSystems = await _mediator.Send(new GetXpSystemQuery());

            if (xpSystems == null)
            {
                return NotFound();
            }

            var xpSystemViewModel = _mapper.Map<XpSystemViewModel>(xpSystems);

            return Ok(xpSystemViewModel);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateXpSystemAsync([FromBody] XpSystemViewModel model)
        {
            //User.IsInRole(Roles.Admin.ToString())

            await _mediator.Send(new CreateOrUpdateXpSystemCommand
            {
                RankXp = _mapper.Map<RankXpDto>(model.RankXp),
                CosmeticXp = _mapper.Map<CosmeticXpDto>(model.CosXp)
            });

            return NoContent();
        }

    }
}
