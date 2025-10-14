using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using Application.Repositories;
using Domain.Entities;


namespace Application.UseCases.Automovil.Queries.GetAllAutomoviles
{
    public class GetAllAutomovilHandler : IRequestHandler<GetAllAutomovilQuery, IEnumerable<Domain.Entities.Automovil>>
    {
        private readonly IAutomovilRepository _repository;

        public GetAllAutomovilHandler(IAutomovilRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Domain.Entities.Automovil>> Handle(GetAllAutomovilQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}