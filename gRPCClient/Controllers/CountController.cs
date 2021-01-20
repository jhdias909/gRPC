using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Grpc.Net.Client;
using gRPCService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace gRPCClient.Controllers
{
    public class CountController : Controller
    {
        public IActionResult IndexgRPC()
        {
            return View();
        }

        public IActionResult IndexREST()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Count_gRPC()
        {
            // The port number(5001) must match the port of the gRPC server.
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Contador.ContadorClient(channel);
            var contadorRequest = new ContadorRequest { Name = "Test" };

            Stopwatch timer = new Stopwatch();
            timer.Start();
            var reply = await client.CountAsync(contadorRequest);
            timer.Stop();
            reply.RequestTime = timer.ElapsedMilliseconds.ToString();
            return Ok(reply);
        }

        [HttpGet]
        public async Task<IActionResult> Count_REST()
        {
            var contadorRequest = new ContadorRequest { Name = "Test" };
            var json = JsonConvert.SerializeObject(contadorRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();

            Stopwatch timer = new Stopwatch();
            timer.Start();
            var response = await client.PostAsync("https://localhost:44339/Count/Count", data);
            var content = await response.Content.ReadAsStringAsync();
            var reply = JsonConvert.DeserializeObject<RESTCommon.ContadorReply>(content);
            timer.Stop();

            reply.RequestTime = timer.ElapsedMilliseconds.ToString();
            return Ok(reply);
        }
    }
}