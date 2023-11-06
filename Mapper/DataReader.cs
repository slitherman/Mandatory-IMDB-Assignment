using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory_IMDB_Assignment.Mapper
{

    internal class DataReader
    {

        public List<T> ReadDataFromFile<T>(string filePath, Func<string[], T> dataMapper, int maxRecords = 50000)
        {
            List<T> dataCollection = new List<T>();
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    reader.ReadLine(); // Skip the header
                    int count = 0;
                    while (!reader.EndOfStream && count < maxRecords)
                    {
                        string line = reader.ReadLine();
                        string[] values = line.Split('\t').Select(x => x.Trim()).ToArray();

                        if (values.Length >= 3 && values.Length <= 9)
                        {
                            try
                            {
                                T data = dataMapper(values);
                                dataCollection.Add(data);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error processing line: " + line);
                                Console.WriteLine("Exception: " + ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Skipping line with invalid column count: " + line);
                        }

                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading data: " + ex.Message);
            }
            return dataCollection;
        }
    }
}
