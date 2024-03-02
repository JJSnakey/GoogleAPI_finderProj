using System;
using System.ServiceModel;
using System.Threading.Tasks;

/*
Joshua Greer 1218576515
from CSE 445 - Assignment 3 part 1
for CSE 445 - Assignment 3 part 2
3/3/2024

Required service 18 crime data service


Implementing this code for assignment 3 part 2
This code was written by me for the first part of the assignment,
imported here to make a proper try it page 


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
