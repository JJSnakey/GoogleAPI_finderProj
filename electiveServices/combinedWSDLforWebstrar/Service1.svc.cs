using System;
using System.Threading.Tasks;
using System.Net.Http;

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
    public class Service1 : IService1
    {
//elective service 1 parks----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public async Task<string> findParks(double latitude, double longitude)
        {
            //create http instance
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //endpoint url
                    string apiURL = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=";
                    string subURL = latitude + "%2C" + longitude + "&radius=1600&type=park&key=";
                    string apiKey = "AIzaSyD5NdSZgBOdreBiPCsDjoCGNU20Y_eVu-s";

                    string requestURL = apiURL + subURL + apiKey;      //full request url

                    //make get request
                    HttpResponseMessage response = await client.GetAsync(requestURL);

                    //check for success
                    if (response.IsSuccessStatusCode)
                    {
                        //read content as string
                        string jsonData = await response.Content.ReadAsStringAsync();

                        string partial = jsonData.Substring(0, 2108);   //this JSON is huge, we only need a small part of it. This part is the first (closest) record

                        //now that we have partial data, we can parse it for the name of the park
                        int index = partial.IndexOf("name");
                        if (index == -1)
                        {
                            return "There are no parks within a mile of this location";
                        }
                        else
                        {
                            int j = index + 9; //skip to the name of the park
                            string parkName = "";
                            while (partial[j] != ',' && partial[j] != '}' && partial[j] != '\"')
                            {
                                parkName += partial[j];
                                j++;
                            }

                            return parkName + " is within one mile of (" + latitude + ", " + longitude + ") !";
                        }

                    }
                    else
                    {
                        return "unsuccessful request (no data)";
                    }

                }
                catch (HttpRequestException ex)
                {
                    return "No parks found";
                }
            }
        }
//Elective 2 Theaters------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<string> findTheaters(double latitude, double longitude)
        {
            //create http instance
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //endpoint url
                    string apiURL = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=";
                    string subURL = latitude + "%2C" + longitude + "&radius=6400&type=movie_theater&key=";
                    string apiKey = "AIzaSyD5NdSZgBOdreBiPCsDjoCGNU20Y_eVu-s";

                    string requestURL = apiURL + subURL + apiKey;      //full request url

                    //make get request
                    HttpResponseMessage response = await client.GetAsync(requestURL);

                    //check for success
                    if (response.IsSuccessStatusCode)
                    {
                        //read content as string
                        string jsonData = await response.Content.ReadAsStringAsync();
                        string partial = jsonData.Substring(0, 1950);   //this JSON is huge, we only need a small part of it. This part is the first (closest) record

                        //now that we have partial data, we can parse it for the name of the theater
                        int index = partial.IndexOf("name");
                        if (index == -1)
                        {
                            return "There are no movie theaters within 4 miles of this location";
                        }
                        else
                        {
                            int j = index + 9; //skip to the name of the theater
                            string theaterName = "";
                            while (partial[j] != ',' && partial[j] != '}' && partial[j] != '\"')
                            {
                                theaterName += partial[j];
                                j++;
                            }

                            return theaterName + " is within four miles of (" + latitude + ", " + longitude + ") !";
                        }

                    }
                    else
                    {
                        return "unsuccessful request (no data)";
                    }

                }
                catch (HttpRequestException ex)
                {
                    return "No movie theaters found";
                }
            }
        }

//Elective 4 bus tops------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
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
                            int j = index + 9; //skip to the name of the bus stop
                            string stopName = "";
                            while (partial[j] != ',' && partial[j] != '}' && partial[j] != '\"')
                            {
                                stopName += partial[j];
                                j++;
                            }

                            return "There is a bus stop at " + stopName + ", which is within one mile of (" + latitude + ", " + longitude + ") !";
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

//crime data service------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //variables
        string ORIcode = "";

        // we changed it from lattitude/longitude to city/stateAbbr because the given api's don't exist anymore
        public async Task<string> crimedata(String city, String stateAbbr)
        {
            //method variables
            int backtrackLength = 17 + 9;// + city.Length + 9;

            //abrivation check
            if (stateAbbr.Length != 2)
            {
                return "State abbreviation must be 2 characters";
            }
            else
            {
                stateAbbr = stateAbbr.ToUpper();
            }

            //create http instance
            using (HttpClient client = new HttpClient())
            {
                try
                {

                    //endpoint url
                    string apiURL = "https://api.usa.gov/crime/fbi/cde/";
                    string subURL = "/agency/byStateAbbr/" + stateAbbr + "?API_KEY=";
                    string key = "VabgMzS6bvqtEsdBIfFz17CVOFSvDiWblcyoL2NX";

                    string requestURL = apiURL + subURL + key;

                    //make get request
                    HttpResponseMessage response = await client.GetAsync(requestURL);

                    //check for success
                    if (response.IsSuccessStatusCode)
                    {
                        //read content as string
                        string jsonData = await response.Content.ReadAsStringAsync();

                        string partial = jsonData.Substring(0, 40000);

                        //time to parse the data and get the ori code
                        int index = partial.IndexOf(city);
                        if (index == -1)
                        {
                            return "failed to find city in state";
                        }
                        else
                        {
                            ORIcode = partial.Substring(index - backtrackLength, 9);
                            //we got the ori code and can use it to make our next request===================
                            return ORIcode;
                        }

                    }
                    else
                    {
                        return "failed to fetch data \nmake sure state abbreviation is valid (2 uppercase letters), and that city name is a city in the state\nMay error if city does not have a police department";
                    }

                }
                catch (HttpRequestException ex)
                {
                    return "Error: " + ex.Message;
                }

            }

        }
        //==================================================================================================
        public async Task<string> crimeCount(string ORI)
        {
            int sum = 0;

            //create http instance
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //we are using the most recent year for info (2020) for our API Request
                    string apiURL = "https://api.usa.gov/crime/fbi/cde/";
                    string subURL = "arrest/agency/" + ORI + "/all?from=2020&to=2020&API_KEY=";
                    string key = "VabgMzS6bvqtEsdBIfFz17CVOFSvDiWblcyoL2NX";

                    string requestURL = apiURL + subURL + key;

                    //make get request
                    HttpResponseMessage response = await client.GetAsync(requestURL);

                    //check for success
                    if (response.IsSuccessStatusCode)
                    {

                        //read content as string
                        string jsonData = await response.Content.ReadAsStringAsync();

                        string partial = jsonData.Substring(0, 1355);

                        //we have the data, now we need to pull the numbers and add them up
                        int index = partial.IndexOf("2020");
                        if (index == -1)
                        {
                            return "failed to calculate number of crimes";
                        }
                        else
                        {
                            string modified = partial.Substring(index);
                            //now we want to get all integers that follow ':' characters, and add them to the sum
                            //when we reach the "']' we know we are finished
                            for (int i = 0; i < modified.Length; i++)
                            {
                                if (modified[i] == ':')
                                {
                                    int j = i + 1;
                                    string num = "";
                                    while (modified[j] != ',' && modified[j] != ']')
                                    {
                                        num += modified[j];
                                        j++;
                                    }
                                    sum += int.Parse(num);
                                }
                            }
                        }
                        return "There is an average of " + sum + " crimes annually in this area.";

                    }
                    else
                    {
                        return "too large of database for my free api key, too much crime in the area";
                    }

                }
                catch (HttpRequestException ex)
                {
                    return "Error: " + ex.Message;
                }
            }

        }
//Natural Hazards--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public async Task<string> NaturalHazards(double latitude, double longitude, int radiusKm, decimal minMag)
        {
            //we are actually going to do specifically earthquakes because this API has global data
            int eqCount = 0;

            //create http instance
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //endpoint url
                    string apiURL = "https://earthquake.usgs.gov/fdsnws/event/1/";
                    //query string, we will show # of earthquakes over a year
                    string subURL = "query?format=geojson&starttime=2022-01-01&endtime=2022-01-31&minmagnitude=" + minMag +
                        "&latitude=" + latitude + "&longitude=" + longitude + "&maxradiuskm=" + radiusKm;

                    string requestURL = apiURL + subURL;      //full request url

                    //make get request
                    HttpResponseMessage response = await client.GetAsync(requestURL);

                    //check for success
                    if (response.IsSuccessStatusCode)
                    {
                        //read content as string
                        string jsonData = await response.Content.ReadAsStringAsync();

                        string partial = jsonData.Substring(0, 330);

                        //now that we have partial data, we can parse it for the count of earthquakes
                        int index = partial.IndexOf("count");
                        if (index == -1)
                        {
                            return "failed to find the count of earthquakes";
                        }
                        else
                        {
                            int j = index + 7; //skip to the number
                            string num = "";
                            while (partial[j] != ',' && partial[j] != '}')
                            {
                                num += partial[j];
                                j++;
                            }
                            eqCount += int.Parse(num);
                            // 
                            return "There has been " + eqCount + " earthquakes of this description in the last year";
                        }

                    }
                    else
                    {
                        return "unsuccessful request (no data)";
                    }

                }
                catch (HttpRequestException ex)
                {
                    return "unsuccessful request (no data)";
                }
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
