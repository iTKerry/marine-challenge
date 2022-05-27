using MediatR.Core.Abstractions;
using WebAPI.Services.VesselTracing;

namespace WebAPI.Commands.Traces.UpdateTrace;

public sealed record UpdateTraceCommand(int TraceId, UpdateTraceDto Data) : ICommand;
