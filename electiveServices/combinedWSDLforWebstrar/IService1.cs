using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

/*
Joshua Greer 1218576515
CSE 445 Assignment 3 part 2 (individual)
3/3/2024

I have 5 wsdl services that I am putting here for better upload
plez help me

Elective Service 1 parks
Elective Service 2 theaters
Elective Service 4 bus stops
Crime Data Service
Natural Hazard Data Service

http://localhost:59753/Service1.svc
*/

namespace combinedWSDLforWebstrar
{
    [ServiceContract]
    public interface IService1
    {
        //elective service 1 parks
        [OperationContract]
        Task<string> findParks(double latitude, double longitude);

        //elective service 2 theaters
        [OperationContract]
        Task<string> findTheaters(double latitude, double longitude);

        //elective service 4 bus stops
        [OperationContract]
        Task<string> findBusStation(double latitude, double longitude);

        //crime data
        [OperationContract]
        Task<string> crimedata(String city, String stateAbbr);

        [OperationContract]
        Task<string> crimeCount(string ORI);

        //natural hazard data
        [OperationContract]
        Task<string> NaturalHazards(double latitude, double longitude, int radiusKm, decimal minMag);
    }
}