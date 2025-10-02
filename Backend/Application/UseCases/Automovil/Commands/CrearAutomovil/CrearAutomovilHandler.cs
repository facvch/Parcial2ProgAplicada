using Application.Repositories;
using MediatR;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Automovil.Commands.CrearAutomovil
{
    public class CrearAutomovilHandler : IRequestHandler<CrearAutomovilCommand, int>
    {
        private readonly IAutomovilRepository _repository;
        public CrearAutomovilHandler(IAutomovilRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> Handle(CrearAutomovilCommand request, CancellationToken cancellationToken)
        {
            var _numeroMotor = GenerarNumeroMotor();
            var _numeroChasis = GenerarNumeroChasis();

            var existeMotor = await _repository.GetByMotorAsync(request.NumeroMotor);
            if (existeMotor != null)
            {
                throw new InvalidOperationException($"Ya existe un automóvil con el número de motor: {request.NumeroMotor}");
                _numeroMotor = GenerarNumeroMotor();
            }

            var existeChasis = await _repository.GetByChasisAsync(request.NumeroChasis);
            if (existeChasis != null)
            {
                throw new InvalidOperationException($"Ya existe un automóvil con el número de chasis: {request.NumeroChasis}");
                _numeroChasis = GenerarNumeroChasis();
            }
            var automovil = new Domain.Entities.Automovil
            {
                Marca = request.Marca,
                Modelo = request.Modelo,
                Color = request.Color,
                Fabricacion = request.Fabricacion,
                NumeroMotor = _numeroMotor,
                NumeroChasis = _numeroChasis
            };
            await _repository.AddAsync(automovil);
            return automovil.Id;
        }

        private string GenerarNumeroMotor()
        {
            return $"MOT-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";
        }

        private string GenerarNumeroChasis()
        {
            return $"{Guid.NewGuid().ToString("N").Substring(0, 17).ToUpper()}";
        }
    }
}
