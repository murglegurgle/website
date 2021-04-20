﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Momentum.XpSystems.Application.DTOs;
using Momentum.XpSystems.Core.Repositories;
using Serilog;

namespace Momentum.XpSystems.Application.Queries
{
    public class GetXpSystemQuery : IRequest<XpSystemDto> { }

    public class GetXpSystemQueryHandlerHandler : IRequestHandler<GetXpSystemQuery, XpSystemDto>
    {
        private readonly IMapper _mapper;
        private readonly IXpSystemRepository _xpSystemRepository;
        private readonly ILogger _logger;

        public GetXpSystemQueryHandlerHandler(IMapper mapper, IXpSystemRepository xpSystemRepository, ILogger logger)
        {
            _mapper = mapper;
            _xpSystemRepository = xpSystemRepository;
            _logger = logger;
        }

        public async Task<XpSystemDto> Handle(GetXpSystemQuery request, CancellationToken cancellationToken)
        {
            var xpSystem = new XpSystemDto();

            try
            {
                xpSystem = await _xpSystemRepository.Get();
            }
            catch (Exception e)
            {
                _logger.Error(e, "XpSystem not initialized");
                throw;
            }

            return _mapper.Map<XpSystemDto>(xpSystem);
        }
    }
}
