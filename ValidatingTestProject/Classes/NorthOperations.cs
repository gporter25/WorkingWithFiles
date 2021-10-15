using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using Operations.NorthWindClasses;

namespace ValidatingTestProject.Classes
{
    public class NorthOperations
    {
        public delegate void OnReadLine(CustomerItem sender);
        public static event OnReadLine ReadLineHandler;
        public delegate void OnErrorReadingLine(NorthErrorContainer errorContainer);
        public static event OnErrorReadingLine ReadLineErrorHandler;

        public static List<CustomerItem> TextFileParser(string fileName)
        {
            List<CustomerItem> list = new List<CustomerItem>();
            string[] parts = {};
            int index = 0;

            using (var parser = new TextFieldParser(fileName))
            {
                parser.Delimiters = new[] { "," };

                while (!parser.EndOfData)
                {
                    try
                    {
                        parts = parser.ReadFields();
                        if (index == 0)
                        {
                            index += 1;
                            continue;
                        }
                        index += 1;
                        var customerItem = new CustomerItem()
                        {
                            CompanyName = parts[0],
                            City = parts[1],
                            PostalCode = parts[2],
                            ContactFirstName = parts[3],
                            ContactLastName = parts[4],
                            CountryIdentifier = Convert.ToInt32(parts[5]),
                            Phone = parts[6]
                        };

                        list.Add(customerItem);
                    }
                    catch (MalformedLineException malformedLineException)
                    {

                        ReadLineErrorHandler?.Invoke(
                            new NorthErrorContainer()
                            {
                                Exception = malformedLineException,
                                Line = string.Join(",", parts),
                                LineNumber = index
                            });
                        
                    }
                    catch (Exception exception)
                    {
                        ReadLineErrorHandler?.Invoke(
                            new NorthErrorContainer()
                            {
                                Exception = exception,
                                Line = string.Join(",", parts),
                                LineNumber = index
                            });
                    }

                }

            }

            return list;

        }
        public static List<CustomerItem> DelegateReadFile1(string fileName)
        {

            List<CustomerItem> list = new List<CustomerItem>();
            string line = "";
            int index = 0;

            var fileStream = new StreamReader(fileName);

            while ((line = fileStream.ReadLine()) != null)
            {
                var parts = line.Split(',');

                try
                {
                    var customerItem = new CustomerItem()
                    {
                        CompanyName = parts[0],
                        City = parts[1],
                        PostalCode = parts[2],
                        ContactFirstName = parts[3],
                        ContactLastName = parts[4],
                        CountryIdentifier = Convert.ToInt32(parts[5]),
                        Phone = parts[6]
                    };

                    list.Add(customerItem);
                    index += 1;
                }
                catch (Exception exception)
                {

                    ReadLineErrorHandler?.Invoke(
                        new NorthErrorContainer()
                        {
                            Exception = exception, 
                            Line = line, 
                            LineNumber = index
                        });

                }
            }

            return list;
        }


        public static void DelegateReadFile2(string fileName)
        {
            string line = "";
            int index = 0;

            var fileStream = new StreamReader(fileName);

            while ((line = fileStream.ReadLine()) != null)
            {
                var parts = line.Split(',');

                if (index == 0)
                {
                    index += 1;
                    continue;
                    
                }
                else
                {
                    index += 1;
                }

                try
                {
                    var customerItem = new CustomerItem()
                    {
                        CompanyName = parts[0], 
                        City = parts[1], 
                        PostalCode = parts[2], 
                        ContactFirstName = parts[3], 
                        ContactLastName = parts[4], 
                        CountryIdentifier = Convert.ToInt32(parts[5]), 
                        Phone = parts[6]
                    };

                    ReadLineHandler?.Invoke(customerItem);

                }
                catch (Exception exception)
                {

                    ReadLineErrorHandler?.Invoke(
                        new NorthErrorContainer()
                        {
                            Exception = exception,
                            Line = line,
                            LineNumber = index
                        });

                }
            }
        }

    }
}
