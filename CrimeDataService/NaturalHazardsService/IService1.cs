using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using RestSharp;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using System.Reflection.Emit;
using System.Threading.Tasks;

/*
Joshua Greer 1218576515
CSE 445 Assignment 3 part 1 (individual)
2/25/2024
Natural Hazards Service
required service 19 natural hazards service

http://localhost:56759/Service1.svc
(open in browser before running tryIT local host)
ServiceReference11 

Detailed comments and known bugs in the Service1.svc.cs file
(workflow on top, bugs/test cases commented beneath)

modified to be specifically for earthquakes because the API has global data 
 */

namespace NaturalHazardsService
{
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        Task<string> NaturalHazards(double latitude, double longitude, int radiusKm, decimal minMag);

        //modified long and lat to double to handle negatives better with api
    }
}
