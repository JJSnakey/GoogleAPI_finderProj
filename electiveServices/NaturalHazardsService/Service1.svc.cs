using System;
using System.Threading.Tasks;
using System.Net.Http;

/*
Joshua Greer 1218576515
from CSE 445 - Assignment 3 part 1
for CSE 445 - Assignment 3 part 2
3/3/2024

Required service 19 natural hazards service


Implementing this code for assignment 3 part 2
This code was written by me for the first part of the assignment,
imported here to make a proper try it page 


modified to be specifically for earthquakes because the API has global data
http://localhost:56759/Service1.svc
(open in browser before running tryIT local host)

api info at https://earthquake.usgs.gov/fdsnws/event/1/
known bugs at bottom
*/

namespace NaturalHazardsService
{
    /*THE PLAN================================================================================================
    easier than last time
    collect inputs
    use the inputs to make a get request to the api
    parse the json response for "count" of earthquakes
    return the count as a string
     */ 
    public class Service1 : IService1
     {
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
                    string subURL = "query?format=geojson&starttime=2022-01-01&endtime=2022-01-31&minmagnitude="+ minMag +
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
     }
}

/*
 Note*
 MOST ERRORS ARE CUZ THE DATA IS NOT THERE

 The API server does not like when you do small radius searches, it will return an error message
 The API server does not like when you do large magnitude, it will return an error message
 
 Most successful with magnitude 3.0 and radius 1000 


Test Cases:
Input (lat,long, radius, minMag) -> Expected Output (count of earthquakes)

30 -110 1000 3.0 -> 41
30 30 1000 3.0 -> 11
50 50 1000 2.0 -> 1
50 50 500 2.0 -> error (there are none)
50 50 1000 3.0 -> 1
 */