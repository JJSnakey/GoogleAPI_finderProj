using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

/*
Joshua Greer 1218576515
CSE 445 Assignment 3 part 2 (individual)
3/3/2024

===Try it page===
Run these in local host to test the services:

findParks service (elective service 1)
http://localhost:52574/Service1.svc 
ServiceReference1
MODERATE DIFFICULTY

findTheaters service (elective service 2)
http://localhost:52840/Service1.svc
ServiceReference2
MODERATE DIFFICULTY

find high schools service (elective service 3)---------RESTful Service
http://localhost:55640/Service1.svc
MODERATE DIFFICULTY

find bus stops service (elective service 4)
http://localhost:55641/Service1.svc
ServiceReference4
MODERATE DIFFICULTY

Required services: (from part 1)
crime data service
http://localhost:55023/Service1.svc
ServiceReference5
HARD DIFFICULTY

natural hazard data service
http://localhost:56759/Service1.svc
ServiceReference6
MODERATE DIFFICULTY


combined service
http://localhost:59753/Service1.svc
 */

namespace TryItPageAspNET
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        //local variables-

        //crime data
        string city = "";
        string stateAbbr = "";
        string ORI = "";

        //natural hazard data
        double lati = 0;
        double longi = 0;
        int radiusKm = 0;
        decimal minMag = 0;

        //Elective services
        double latitude = 0;
        double longitude = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        protected void Button1_Click(object sender, EventArgs e)
        {
            //park service
            //activate elective service 1

            //handling these here because can trouble
            latitude = Convert.ToDouble(TextBox1.Text);
            longitude = Convert.ToDouble(TextBox2.Text);

            var client = new ServiceReference11.Service1Client();

            try
            {
                Label1.Text = client.findParks(latitude, longitude);
            }
            catch (Exception ex)
            {
                Label1.Text = "No parks nearby or invalid latitude/longitude";
            }
            client.Close(); //good practice to close the client
        }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        protected void Button2_Click(object sender, EventArgs e)
        {
            //theaters service
            //activate elective service 2

            //handling these here because can trouble
            latitude = Convert.ToDouble(TextBox1.Text);
            longitude = Convert.ToDouble(TextBox2.Text);

            var client = new ServiceReference11.Service1Client();

            try
            {
                Label2.Text = client.findTheaters(latitude, longitude);
            }
            catch (Exception ex)
            {
                Label2.Text = "No Theaters nearby or invalid latitude/longitude";
            }
            client.Close(); //good practice to close the client
        }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        protected void Button3_Click(object sender, EventArgs e)
        {  
            //schools service
            //activate elective service 3

            //handling these here because can trouble
            latitude = Convert.ToDouble(TextBox1.Text);
            longitude = Convert.ToDouble(TextBox2.Text);
            string jsonData = "";

            HttpClient client = new HttpClient();
            string URI = "http://webstrar5.fulton.asu.edu/page4/Service1.svc/findSchool?latitude=" + latitude + "&longitude=" + longitude;

            try
            {
                Task<HttpResponseMessage> response = client.GetAsync(URI);
                jsonData = response.Result.Content.ReadAsStringAsync().Result;
                if (jsonData != "")
                {
                    int length = jsonData.Length;
                    Label3.Text = jsonData.Substring(1, length - 2);
                }
                else
                {
                    Label3.Text = "unsuccessful request (no data)";
                }
            }
            catch (Exception ex)
            {
                Label3.Text = "No high schools nearby or invalid latitude/longitude";
            }
        }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        protected void Button4_Click(object sender, EventArgs e)
        {
            //bus stops service
            //activate elective service 4

            //handling these here because can trouble
            latitude = Convert.ToDouble(TextBox1.Text);
            longitude = Convert.ToDouble(TextBox2.Text);

            var client = new ServiceReference11.Service1Client();

            try
            {
                Label4.Text = client.findBusStation(latitude, longitude);
            }
            catch (Exception ex)
            {
                Label4.Text = "No bus stations nearby or invalid latitude/longitude";
            }
            client.Close(); //good practice to close the client
        }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        protected void Button5_Click(object sender, EventArgs e)
        {
            //set inputs
            city = TextBox3.Text;
            stateAbbr = TextBox4.Text;

            //check for valid state abbreviation
            if(stateAbbr.Length != 2)
            {
                Label5.Text = "State abbreviation must be 2 characters";
                return;
            }
            else
            {
                stateAbbr = stateAbbr.ToUpper();
            }

            //crime data service
            var client = new ServiceReference11.Service1Client();

            try
            {
                ORI = client.crimedata(city, stateAbbr);
                Label5.Text = client.crimeCount(ORI);
            }
            catch (Exception ex)
            {
                Label5.Text = "No crimes or invalid input";
            }
            client.Close(); //good practice to close the client
        }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        protected void Button6_Click(object sender, EventArgs e)
        {
            //natural hazard data service
            //handling these here because they are causing trouble
            lati = Convert.ToDouble(TextBox5.Text);
            longi = Convert.ToDouble(TextBox6.Text);
            radiusKm = Convert.ToInt32(TextBox7.Text);
            minMag = Convert.ToDecimal(TextBox8.Text);

            var client = new ServiceReference11.Service1Client();

            try
            {
                Label6.Text = client.NaturalHazards(lati, longi, radiusKm, minMag);
            }
            catch (Exception ex)
            {
                Label6.Text = "No earthquakes or invalid input";
            }
            client.Close(); //good practice to close the client
        }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            //latitude
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            //longitude
        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {
            //City
        }

        protected void TextBox4_TextChanged(object sender, EventArgs e)
        {
            //State
        }

        protected void TextBox5_TextChanged(object sender, EventArgs e)
        {
            //lati
        }

        protected void TextBox6_TextChanged(object sender, EventArgs e)
        {
            //longi
        }

        protected void TextBox7_TextChanged(object sender, EventArgs e)
        {
            //radiusKm
        }

        protected void TextBox8_TextChanged(object sender, EventArgs e)
        {
            //minMag
        }

    }
}