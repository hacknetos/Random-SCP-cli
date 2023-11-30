using System;
using System.Net;
using System.Xml.Linq;

namespace Random_SCP
{
    internal class Program
    {
        /** 
         * <summary>
         *  A Diconary of all Know Elements in the Html
         * </summary>
         *
         */
        static Dictionary<string,string> elemente= new Dictionary<string,string>();


            /**
             * <summary>
             * Start of the Programm 
             * </summary>
             *  <param name="args">a Array of Strings with arguments </param>
            */
        static void Main(string[] args)
        {
            
            Random random = new Random();

            //int scpNumber = random.Next(0,8000);
            int scpNumber = 5233;
            
            //Baseurl of the scp Wiki 
            string baseUrl = "https://scp-wiki.wikidot.com/scp-";


            if (scpNumber<100)
            {
                 baseUrl = "https://scp-wiki.wikidot.com/scp-0" + scpNumber;
            }
            else
            {
                 baseUrl = "https://scp-wiki.wikidot.com/scp-" + scpNumber;
            }




            Console.WriteLine("The Random SCP IS "+scpNumber);

            string htmlCode = DowloadeHtml(baseUrl);

            Console.WriteLine(htmlCode);
        }
        /** 
            <summary>
                Downloads The Html From The Url Thet is Generatet in the Main
            </summary>
            <param name="url" Type="string">The in the Main Generatet Url For the SCP Site.</param>
            <returns>the Return value is The Formatierte Html Code</returns>
        
         */
        static string DowloadeHtml(string url) 
        {
            string htmlCode = "No Code Exist Sorry";
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString(url);
            }
            return Aufhübscher(htmlCode);
        }
        /**
         * <summary>
         *  Transform the html code in better partes
         * </summary>
         * <param name="htmlCode">the Code from the <see cref="DowloadeHtml(string)"/></param>
         * <returns>a String with all mager Ifos without any disruptions</returns>
         * <seealso cref="DowloadeHtml(string)"/>
         */
        static string Aufhübscher(string htmlCode)
        {
            GetTheTitle(htmlCode);


            //Remuve the Header
            htmlCode = htmlCode.Split("</head>")[1];

            //Filter Img Out
            htmlCode = htmlCode.Split("<div id=\"page-content\">")[1];
            htmlCode = htmlCode.Split("<div class=\"footer-wikiwalk-nav\">")[0];
            htmlCode = htmlCode.Split("<img")[1];
            string[] codeSpliter = htmlCode.Split("class=\"image\" />");

            elemente.Add("img", codeSpliter[0].Replace("src=\"", "").Split('"')[0]);
           
            htmlCode = codeSpliter[1];
            htmlCode = htmlCode.Replace("<p>", " ;?;Details;?; ");
            htmlCode = htmlCode.Replace("</p>", " ;?;Details;?; ");
            codeSpliter = htmlCode.Split(" ;?;Details;?; ");
            Console.WriteLine("Waite");
            return htmlCode;
        }

        /**
         * <summary>
         *  extract the Titel out of the header and add him to the elemente Diconary
         * </summary> 
         * 
         * <param name="htmlCode">is the Code From the Donwloads Function</param>
         * 
         * <seealso cref="DowloadeHtml(string)"/>
         * <seealso cref="Aufhübscher(string)"/>
         * <seealso cref="elemente"/>
         * 
         */
        static void GetTheTitle(string htmlCode)
        {
            string[] codeSpliter = htmlCode.Split("<title>");
            codeSpliter = codeSpliter[codeSpliter.Length - 1].Split("</title>");
            elemente.Add("Titel", codeSpliter[0]);
        }
    }
}