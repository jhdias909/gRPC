using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace gRPCService
{
    public class ContadorService : Contador.ContadorBase
    {
        private static long _count;


        private readonly ILogger<ContadorService> _logger;
        public ContadorService(ILogger<ContadorService> logger)
        {
            _logger = logger;
        }

        public override Task<ContadorReply> Count(ContadorRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ContadorReply
            {
                Message = $"Name: {request.Name}, Count: {_count++}"
            });
        }
    }
}
