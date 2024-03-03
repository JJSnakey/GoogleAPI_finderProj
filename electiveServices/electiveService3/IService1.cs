using System;
using System.ServiceModel;
using System.Threading.Tasks;

/*
Joshua Greer 1218576515
CSE 445 Assignment 3 part 2 (individual)
3/3/2024
Elective Service 3
Find Schools service

Detailed comments and known bugs in the Service1.svc.cs file
(workflow on top, bugs/test cases commented beneath)
 */

namespace electiveService3
{
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        Task<string> findSchool(double latitude, double longitude);
    }

}