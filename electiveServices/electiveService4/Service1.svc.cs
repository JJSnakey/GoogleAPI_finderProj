using System;
using System.Threading.Tasks;
using System.Net.Http;

/*
Joshua Greer 1218576515
CSE 445 Assignment 3 part 2 (individual)
3/3/2024
Elective Service 4
Find bus stops service


http://localhost:55641/Service1.svc
local host testing

bugs detailed at bottom
 */

namespace electiveService4
{
    /*THE PLAN================================================================================================

    input parameters: 
    latitude
    longitude
    
    predefined parameters: 
    radius 1600 meters
    api key
    type = bus_station

    api key = AIzaSyD5NdSZgBOdreBiPCsDjoCGNU20Y_eVu-s
    google will not let me pass 200$ / month, so be careful and delete when done
    (graders, I am watching you) 
    ps if you have your own key please use it, you can create for free on google cloud

    example query:
    https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=33.4%2C-111.8&radius=1600&type=bus_station&key=AIzaSyD5NdSZgBOdreBiPCsDjoCGNU20Y_eVu-s

    ----------------------------------------------------------------------------------------------------------------
    We using the google places api to find bus stops within 1600 meters of a given location
    If there is a nearby bus stop, we will return the name of the bus stops
    If there is no nearby bus stops, we will return "no nearby bus stops"

    most errors are not finding any bus stops
     */
    public class Service1 : IService1
    {
        public async Task<string> findBusStation(double latitude, double longitude)
        {
            //create http instance
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //endpoint url
                    string apiURL = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=";
                    string subURL = latitude + "%2C" + longitude + "&radius=1600&type=bus_station&key=";
                    string apiKey = "AIzaSyD5NdSZgBOdreBiPCsDjoCGNU20Y_eVu-s";

                    //full request url
                    string requestURL = apiURL + subURL + apiKey;

                    //make get request
                    HttpResponseMessage response = await client.GetAsync(requestURL);

                    //check for success
                    if (response.IsSuccessStatusCode)
                    {
                        //read content as string
                        string jsonData = await response.Content.ReadAsStringAsync();

                        string partial = jsonData.Substring(0, 1382);   //this JSON is huge, we only need a small part of it. This part is the first (closest) record

                        //now that we have partial data, we can parse it for the name of the bus stop
                        int index = partial.IndexOf("name");
                        if (index == -1)
                        {
                            return "There are no bus stops within 1 mile of this location";
                        }
                        else
                        {
                            int j = index + 9; //skip to the name of the theater
                            string stopName = "";
                            while (partial[j] != ',' && partial[j] != '}' && partial[j] != '\"')
                            {
                                stopName += partial[j];
                                j++;
                            }

                            return "There is a bus stop at" + stopName + ", which is within one mile of (" + latitude + ", " + longitude + ") !"; ;
                        }

                    }
                    else
                    {
                        return "unsuccessful request (no data)";
                    }

                }
                catch (HttpRequestException ex)
                {
                    return "No bus stops found";
                }
            }
        }
    }
}

/*
 * *Notes *
This API finds anything with the type "movie_theater"


*Test Cases*
input (latitude, longitude)     |  expected output:                
                                | description

33.4, -111.8                    | Southern Avenue and Stapley Drive
                                | this lat/long is Mesa AZ, AMC is the closest

33.4, -111.9                    | Cinemark Mesa 16 
                                | this lat/long is Tempe AZ, Cinemark is closest

40.7, -74.0                     | Regal Union Square
                                | this lat/long is New York City, there are lots of areas tagged as parks

39.07, -75.13                   | Error
                                | this lat/long is in the middle of the ocean, no parks
*/