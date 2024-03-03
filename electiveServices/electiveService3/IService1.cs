using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.ServiceModel.Web;      //wsdl to rest step 1

/*
RESTful

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
        [WebGet(UriTemplate = "findSchool?latitude={latitude}&longitude={longitude}" , ResponseFormat = WebMessageFormat.Json)]
        Task<string> findSchool(double latitude, double longitude);
    }

}
/*
RESTful 
step 2 restful:
add this line to markup
Factory="System.ServiceModel.Activation.WebServiceHostFactory"

step 3 restful:
remove <system.serviceModel> from web.config


Testing:
http://localhost:55640/Service1.svc/findSchool?latitude=33.4&longitude=-111.8

*/