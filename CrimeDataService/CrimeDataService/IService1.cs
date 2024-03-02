using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using System.Reflection.Emit;

/*
Joshua Greer 1218576515
2/25/2024
CSE 445 Assignment 3 part 1 (individual)
Crime Data Service
required service 18 crime data service

detailed comments in IService1.svc.cs (workflow on top, known bugs at bottom)

Hosted at  http://localhost:55023/Service1.svc
(run in browser)
 */

namespace CrimeDataService
{
//I utilize to operations because I have to make 2 api calls to get the data
//crime data gets me a reference to the police department
//crime count uses that reference to get the number of crimes (annually)

    [ServiceContract]
    public interface IService1
    {
        //as specified in req document, except I added a radius parameter to the method signature
        //async requires task<> rather than just an int
        [OperationContract]
        Task<string> crimedata(String city, String stateAbbr);

        //added a second method because it makes sense to split up the work
        [OperationContract]
        Task<string> crimeCount(string ORI);
    }
}
