using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Core.Application;
using System.Text.Json.Serialization;

namespace Application.UseCases.Automovil.Commands.CrearAutomovil
{
    public class CrearAutomovilCommand : IRequest<int>
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public int Fabricacion { get; set; }

        [JsonIgnore]
        public string? NumeroMotor {  get; set; }

        [JsonIgnore]
        public string? NumeroChasis { get; set; }
    }
}
