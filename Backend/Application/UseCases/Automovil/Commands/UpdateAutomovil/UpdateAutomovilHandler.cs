using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Domain.Entities;
using Application.Repositories;

namespace Application.UseCases.Automovil.Commands.UpdateAutomovil
{
    public class UpdateAutomovilHandler : IRequestHandler<UpdateAutomovilCommand, bool>
    {
        private readonly IAutomovilRepository _repository;

        public UpdateAutomovilHandler(IAutomovilRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateAutomovilCommand request, CancellationToken cancellationToken)
        {
            var automovilExistente = await _repository.GetByIdAsync(request.Id);
            if (automovilExistente == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(request.NumeroMotor))
            {
                var otroConMismoMotor = await _repository.GetByMotorAsync(request.NumeroMotor);
                if (otroConMismoMotor != null && otroConMismoMotor.Id != request.Id)
                {
                    throw new InvalidOperationException($"Ya existe otro automóvil con el número de motor: {request.NumeroMotor}");
                }

                automovilExistente.NumeroMotor = request.NumeroMotor;
            }

            if (!string.IsNullOrWhiteSpace(request.Color))
            {
                automovilExistente.Color = request.Color;
            }

            await _repository.UpdateAsync(automovilExistente);
            return true;
        }
    }
}
