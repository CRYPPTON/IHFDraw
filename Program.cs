using System;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Linq;

namespace IHFdraw
{
    class Program
    {
        static void Main(string[] args)
        {
            // get data from csv and put in representations variable

            List<Representation> representations = getData();
           

            // check qualifier numbber by continent

            if (CheckAll(representations))
            {
                Console.WriteLine("Number of qualifier is correct...");
            }
            else {
                throw new InvalidOperationException("The number of participating countries from one continent is higher than allowed!");
            }

            makeDraw(representations);

        }

        static List<Representation> getData()
        {
            string Country;
            string Continent;
            string Rang;

            var path = @"C:\Users\nikol\OneDrive\Desktop\HandBallDrawApp\IHFdraw\IHFdraw\data\ulaz.csv";

            List<Representation> RepresentationList = new List<Representation>();

            using (TextFieldParser csvParser = new TextFieldParser(path))
            {

                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();
                    Country = fields[0];
                    Continent = fields[1];
                    Rang = fields[2];
                    RepresentationList.Add(new Representation(Country, Continent, Rang));
                }
            }
            
            return RepresentationList;
        }
        static bool CheckAll(List<Representation> representations)
        {
            int allowedAfr = 5,
                allowedAsia = 5,
                allowedEur = 14,
                allowedAmer = 3,
                allowedOcea = 1,
                allowedSouAmer = 4;

            foreach (var Representation in representations)
            {
                 switch (Representation.Continent)
                 {
                     case "Afrika":
                         allowedAfr--;
                         break;
                     case "Azija":
                         allowedAsia--;
                         break;
                     case "Evropa":
                         allowedEur--;
                         break;
                     case "Juzna Amerika":
                         allowedSouAmer--;
                         break;
                     case "Severna i Centralna Amerika":
                         allowedAmer--;
                         break;
                     case "Okeanija":
                         allowedOcea--;
                         break;
                 }
            }

            if (allowedAfr + allowedAsia + allowedEur + allowedAmer != 0) return false;
           
            return (allowedOcea == 1 && allowedSouAmer == -1) || (allowedOcea == 0 && allowedSouAmer == 0) ? true : false;
        }

        static void makeDraw(List<Representation> representations)
        {

            List<Representation> SortedList = representations.OrderBy(x => Int32.Parse(x.IhfRang)).ToList();
            string[][] selection = new string[4][];
            int numGroup = 4;

            foreach (var Representation in SortedList)
            {
                Console.WriteLine("Representations: {0}, Continent: {1}, Rang: {2}", Representation.Country, Representation.Continent, Representation.IhfRang);
            }
            Console.WriteLine();
            Console.WriteLine();

            for ( numGroup = 0; numGroup < 4; numGroup++) 
            {
                selection[numGroup] = new string[8];
            }
           

            
            int i = 0;
            int j = 0;

            foreach (var Representation in SortedList)
            {
                if (j == 8)
                {
                    i++;
                    j = 0;
                    Console.WriteLine();
                    Console.WriteLine();
                }
                // Console.WriteLine("Representations: {0}, Continent: {1}, Rang: {2}", Representation.Country, Representation.Continent, Representation.IhfRang);
                selection[i][j] = Representation.Country;  
                Console.WriteLine(selection[i][j]);
                j++;
           }



            string[][] group = new string[8][];

            for (numGroup = 0; numGroup < 8; numGroup++)
            {
                group[numGroup] = new string[4];
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            // add I rank teams
            int rInt;
            Random r = new Random();
            for (i = 0; i < 8; i++)
            {   
                
                rInt = r.Next(0, 8-i);                  //missed rand 
                
                
                group[i][0] = selection[0][rInt];
                


                selection[0] = selection[0].Where(val => val != selection[0][rInt]).ToArray();

                Console.WriteLine("Grupa: {0}, Drzava: {1}", i+1 ,group[i][0]);
            }


        }


            
    }
}
