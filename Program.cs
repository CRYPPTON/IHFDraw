using System;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Linq;
using System.IO;

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
            string Group;
            int Position;
            int Selection;
            int Score;
            int Goal;

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
                    Group = "/";
                    Position = 0;
                    Selection = 0;
                    Score = 0;
                    Goal = 0;
                    RepresentationList.Add(new Representation(Country, Continent, Rang, Position, Group, Selection, Score, Goal));
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
            string[] groupA = { "A", "B", "C", "D", "E", "F", "G", "H"};
            string[] groupB = { "A", "B", "C", "D", "E", "F", "G", "H" };
            string[] groupC = { "A", "B", "C", "D", "E", "F", "G", "H" };
            string[] groupD = { "A", "B", "C", "D", "E", "F", "G", "H" };

            int i = 0;
            int j = 0;
            int rInt;
            Random r = new Random();

            foreach (var Representation in SortedList)
            {
                if (j == 8)
                {
                    i++;
                    j = 0;
                    Console.WriteLine();
                }
                if (i == 0) 
                {
                    rInt = r.Next(0, 8 - j);
                    Representation.Group = groupA[rInt];
                    Representation.GroupPosition = 1;
                    groupA = groupA.Where(val => val != groupA[rInt]).ToArray();     
                }

                if (i == 1)
                {
                    rInt = r.Next(0, 8 - j);
                    Representation.Group = groupB[rInt];
                    Representation.GroupPosition = 2;
                    groupB = groupB.Where(val => val != groupB[rInt]).ToArray();
                }
                if (i == 2)
                {
                    rInt = r.Next(0, 8 - j);
                    Representation.Group = groupC[rInt];
                    Representation.GroupPosition = 3;
                    groupC = groupC.Where(val => val != groupC[rInt]).ToArray();
                }
                if (i == 3)
                {
                    rInt = r.Next(0, 8 - j);
                    Representation.Group = groupD[rInt];
                    Representation.GroupPosition = 4;
                    groupD = groupD.Where(val => val != groupD[rInt]).ToArray();
                }

                Representation.Selection = i+1;
                j++;
                Console.WriteLine("Representations: {0}, Continent: {1}, Rang: {2}, Group: {3}, Group Position: {4} Selection: {5}", Representation.Country, Representation.Continent, Representation.IhfRang, Representation.Group, Representation.GroupPosition, Representation.Selection);
            }


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();



            List<Representation> SortedListByGroup = representations.OrderBy(x => x.Group).ToList();

            int t = 0;
            foreach (var Representation in SortedListByGroup)
            {
                if (t % 4 == 0) Console.WriteLine();
                Console.WriteLine("Representations: {0}, Continent: {1},  Group: {2}, Group Position: {3} ", Representation.Country, Representation.Continent,  Representation.Group, Representation.GroupPosition);
                t++;
            }
            generateCSV(SortedListByGroup);
        }


        static void generateCSV(List<Representation>  data)
        {
            var file = @"C:\Users\nikol\OneDrive\Desktop\grupe.csv";
            string text = "";
            List<Representation> groupList = new List<Representation>();


            using (var stream = File.CreateText(file))
            {
                int i = 1;

                foreach(var team in data)
                {
                    
                    groupList.Add(team);
                    string group = team.Group;

                    if (i%4 == 0 && i>0)
                    {
                        
                        groupList = groupList.OrderBy(x => Int32.Parse(x.IhfRang)).ToList();
                        generateMatch(groupList);


                        foreach (var k in groupList)
                        {
                            string name = k.Country;
                            string pos = k.GroupPosition.ToString();
                            
                            text += pos + ". " + name + ",";
                        }
                        
                        string csvRow = string.Format("{0}, {1}", group, text);
                        stream.WriteLine(csvRow);
                        text = "";
                        groupList = new List<Representation>();
                    }


                    i++;
                    
                }
            }
        }

        static void generateMatch(List<Representation> group)
        {
            Console.WriteLine();
            Console.WriteLine();
            var team1 = "";
            var team2 = "";
            var team3 = "";
            var team4 = "";
            var score1 = 0;
            var score2 = 0;
         

            

            foreach (var k in group)
            {
                if (k.GroupPosition == 1)
                {
                    team1 = k.Country;
                }
                if (k.GroupPosition == 2)
                {
                    team2 = k.Country;
                }
                if (k.GroupPosition == 3)
                {
                    team3 = k.Country;
                }
                if (k.GroupPosition == 4)
                {
                    team4 = k.Country;
                }
            }


            Random rnd = new Random();
            foreach (var t in group)
                {
                if (t.GroupPosition == 1)
                {

                    score1 = rnd.Next(10, 50);
                    score2 = rnd.Next(10, 50);
                    if (score1 > score2)
                    {
                        t.Goal = score1;
                        t.Score += 3;

                    }
                    if (score1 == score2) 
                    {
                        t.Goal = score1;
                        t.Score ++;
                    }
                    if (score1 < score2)
                    {
                        t.Goal = score1;        
                    }

                    Console.WriteLine("{0} - {1}, {2} : {3}", team1, team2, score1, score2);
                        score1 = rnd.Next(10, 50);
                        score2 = rnd.Next(10, 50);
                        Console.WriteLine("{0} - {1}, {2} : {3}", team1, team3, score1, score2);
                        score1 = rnd.Next(10, 50);
                        score2 = rnd.Next(10, 50);
                        Console.WriteLine("{0} - {1}, {2} : {3}", team1, team4, score1, score2);
                    } 
                    if (t.GroupPosition == 2) 
                    {
                        score1 = rnd.Next(10, 50);
                        score2 = rnd.Next(10, 50);
                        Console.WriteLine("{0} - {1}, {2} : {3}", team2, team3, score1, score2);
                        score1 = rnd.Next(10, 50);
                        score2 = rnd.Next(10, 50);
                        Console.WriteLine("{0} - {1}, {2} : {3}", team2, team4, score1, score2);
                    }
                    if (t.GroupPosition == 3)
                    {
                        score1 = rnd.Next(10, 50);
                        score2 = rnd.Next(10, 50);
                        Console.WriteLine("{0} - {1}, {2} : {3}", team3, team4, score1, score2);
                    }


            }
         
        }

            
    }
}
