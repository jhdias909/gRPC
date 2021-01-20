using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using gRPCService;
using Microsoft.AspNetCore.Mvc;

namespace gRPCClient.Controllers
{
    public class CountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Count()
        {
            // The port number(5001) must match the port of the gRPC server.
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Contador.ContadorClient(channel);
            var contadorRequest = new ContadorRequest{ Name = "Test" };

            Stopwatch timer = new Stopwatch();
            timer.Start();
            var reply = await client.CountAsync(contadorRequest);
            timer.Stop();
            reply.RequestTime = timer.ElapsedMilliseconds.ToString();
            return Ok(reply);
        }
    }
}