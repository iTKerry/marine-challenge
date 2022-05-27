using MediatR.Core.Abstractions;
using WebAPI.Services.VesselTracing;

namespace WebAPI.Commands.Vessels.UpdateVessel;

public record UpdateVesselCommand(int IMO, UpdateVesselDto Data) : ICommand;