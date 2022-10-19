using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ProgAssign1
{
    public class DirWalker
    {
        private IList<Customer> customerList = new List<Customer>();

        public IList<Customer> GetCustomerList()
        {
            return customerList;
        }

        private int skipCount = 0;

        public int GetSkipCount()
        {
            return skipCount;
        }

        public void walk(String path)
        {
            string[] list = Directory.GetDirectories(path);

            if (list == null) return;

            foreach (string dirpath in list)
            {
                if (Directory.Exists(dirpath))
                {
                    walk(dirpath);
                    Console.WriteLine("Dir:" + dirpath);
                }
            }
            string[] fileList = Directory.GetFiles(path);
            foreach (string filepath in fileList)
            {
                using (var reader = new StreamReader(filepath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();

                        if (line != null && line.ToString().ToLower().Contains("first name")) continue;
                        var values = line.Split(';');

                        if (values != null && values.Length > 0)
                        {
                            string[] lineValues = values[0].Split(',');

                            Customer customer = ConvertCSVToCustomer(lineValues, path);

                            if (!IsAnyNullOrEmpty(customer))
                            {
                                customerList.Add(customer);
                            }
                            else
                            {
                                skipCount++;
                            }
                        }
                        else
                        {
                            skipCount++;
                        }
                    }
                }

                Console.WriteLine("File:" + filepath);
            }
        }

        public void ConvertListToCSV(string outputPath)
        {
            string csvHeaderRow = String.Join(",", typeof(Customer).GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(x => x.Name).ToArray<string>()) + Environment.NewLine;
            string csv = csvHeaderRow + String.Join(Environment.NewLine, customerList.Select(x => x.ToString()).ToArray());

            string path = outputPath + "\\result.csv";
            WriteCSV(customerList, path);
        }

        bool IsAnyNullOrEmpty(object myObject)
        {
            foreach (PropertyInfo pi in myObject.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    string value = (string)pi.GetValue(myObject);
                    if (string.IsNullOrEmpty(value))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        Customer ConvertCSVToCustomer(string[] lineValues, string path)
        {
            Customer customer = new Customer();

            if (lineValues.Length >= 10)
            {
                customer.FirstName = Convert.ToString(lineValues[0]);
                customer.LastName = Convert.ToString(lineValues[1]);
                customer.StreetNumber = Convert.ToString(lineValues[2]);
                customer.Street = Convert.ToString(lineValues[3]);
                customer.City = Convert.ToString(lineValues[4]);
                customer.Province = Convert.ToString(lineValues[5]);
                customer.PostalCode = Convert.ToString(lineValues[6]);
                customer.Country = Convert.ToString(lineValues[7]);
                customer.PhoneNumber = Convert.ToString(lineValues[8]);
                customer.EmailAddress = Convert.ToString(lineValues[9]);

                var temp = path.Split('\\');

                if (temp.Length > 3)
                {
                    customer.DateDetail = temp[temp.Length - 3] + "-" + temp[temp.Length - 2] + "-" + temp[temp.Length - 1];
                }
            }

            return customer;
        }

        void WriteCSV<T>(IEnumerable<T> items, string path)
        {
            Type itemType = typeof(T);
            var props = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine(string.Join(", ", props.Select(p => p.Name)));

                foreach (var item in items)
                {
                    writer.WriteLine(string.Join(", ", props.Select(p => p.GetValue(item, null))));
                }
            }
        }

    }
}
