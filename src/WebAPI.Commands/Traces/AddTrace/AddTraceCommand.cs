using MediatR.Core.Abstractions;
using WebAPI.Services.VesselTracing;

namespace WebAPI.Commands.Traces.AddTrace;

public sealed record AddTraceCommand(params AddTraceDto[] Data) : ICommand;