using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace combinedWSDLforWebstrar
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
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