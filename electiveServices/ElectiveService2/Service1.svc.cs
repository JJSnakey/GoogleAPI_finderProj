using System;
using System.Threading.Tasks;
using System.Net.Http;

/*
Joshua Greer 1218576515
CSE 445 Assignment 3 part 2 (individual)
3/3/2024
Elective Service 2

bugs detailed at bottom
 */

namespace ElectiveService2
{
    /*THE PLAN================================================================================================

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
    }
}

/*
*Notes*

*Test Cases*

 */
