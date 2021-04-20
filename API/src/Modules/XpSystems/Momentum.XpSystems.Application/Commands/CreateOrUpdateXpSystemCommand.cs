﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Momentum.XpSystems.Application.DTOs;
using Momentum.XpSystems.Application.DTOs.Cosmetic;
using Momentum.XpSystems.Application.DTOs.Rank;
using Momentum.XpSystems.Core.Repositories;
using Serilog;

namespace Momentum.XpSystems.Application.Commands
{
    public class CreateOrUpdateXpSystemCommand : IRequest
    {
        public RankXpDto RankXpDto { get; set; }
        public CosmeticXpDto CosmeticXp { get; set; }
    }

    public class CreateOrUpdateXpSystemCommandHandler : IRequestHandler<CreateOrUpdateXpSystemCommand>
    {
        private readonly IXpSystemRepository _xpSystemRepository;
        private readonly ILogger _logger;

        public CreateOrUpdateXpSystemCommandHandler(IXpSystemRepository xpSystemRepository, ILogger logger)
        {
            _xpSystemRepository = xpSystemRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(CreateOrUpdateXpSystemCommand request, CancellationToken cancellationToken)
        {
            var xpSystem = new XpSystemDto();

            try
            {
                xpSystem = await _xpSystemRepository.Get();
            }
            catch (Exception e)
            {
                _logger.Error(e, "XpSystem not initialized");
            }

            xpSystem.RankXp = request.RankXpDto;

            xpSystem.CosmeticXp = request.CosmeticXp;

            await _xpSystemRepository.CreateOrUpdate(xpSystem);

            return Unit.Value;
        }
    }
}
