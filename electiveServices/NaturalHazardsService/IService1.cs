using System;
using System.ServiceModel;
using System.Threading.Tasks;

/*
Joshua Greer 1218576515
from CSE 445 - Assignment 3 part 1
for CSE 445 - Assignment 3 part 2
3/3/2024

Required service 19 natural hazards service


Implementing this code for assignment 3 part 2
This code was written by me for the first part of the assignment,
imported here to make a proper try it page 

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
