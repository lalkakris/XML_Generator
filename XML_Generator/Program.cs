﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace XML_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
           
            var Names = File.ReadAllLines("Names.txt").ToList();
            var LastNames = File.ReadAllLines("LastNames.txt").ToList();
            var rand = new Random();
            var People = new List<Person>();
            var Payments = new List<Payment>();

            for (long i = 0; i < 1000000; i++)
            {
                var person = new Person
                {
                    ID = i + 1,
                    Name = Names[rand.Next(Names.Count)],
                    LastName = LastNames[rand.Next(LastNames.Count)]
                };
                People.Add(person);
            };

            for (long i = 0; i < 1000000; i++)
            {
                var payment = new Payment 
                { 
                    ID = i + 1, 
                    Sum = rand.Next(1, 1000), 
                    Date = new DateTime().RandomDay(), 
                    PersonId = rand.Next(People.Count) 
                };
                Payments.Add(payment);
            };

            //XML serializing:
            var xmlSerializerPayments = new XmlSerializer(typeof(List<Payment>));
            var xmlSerializerPeople = new XmlSerializer(typeof(List<Person>));
           
            var stringWriter1 = new StringWriter();
            xmlSerializerPeople.Serialize(stringWriter1, People);
            var xml1 = stringWriter1.ToString();
            File.WriteAllText("BaseOfNames.xml", xml1);
            
            var stringWriter2 = new StringWriter();
            xmlSerializerPayments.Serialize(stringWriter2, Payments);
            var xml2 = stringWriter2.ToString();
            File.WriteAllText("BaseOfPayments.xml", xml2);


            ////Json serializing:
            //var jsonPeople = JsonConvert.SerializeObject(People, Newtonsoft.Json.Formatting.Indented);
            //File.WriteAllText("BaseOfNames.json", jsonPeople);
            //var jsonPayments = JsonConvert.SerializeObject(Payments, Newtonsoft.Json.Formatting.Indented);
            //File.WriteAllText("BaseOfPayments.json", jsonPayments);

        }
  
    }
    public static class DateTimeExtension
    {
        public static string RandomDay(this DateTime start)
        {
            var rand = new Random();
            start = new DateTime(2015, 1, 1);
            var range = (DateTime.Today - start).Days;
            return start.AddDays(rand.Next(range)).ToShortDateString(); ;
 
        }
    }
}
