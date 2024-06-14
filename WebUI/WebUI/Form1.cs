using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using WebUI.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;

namespace WebUI
{
    public partial class MainFormUI : Form
    {
        public MainFormUI()
        {
            InitializeComponent();
        }

        private void MainFormUI_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
                SaveCurrenciesToday();

                foreach (var item in LoadFromDb())
                {
                    firstCb.Items.Add(item.Cur_Abbreviation);
                    secondCb.Items.Add(item.Cur_Abbreviation);
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
             resultTextBox.Text = Calculate(SetParams());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            SaveResults(SetResultData());
            List<Result> results = new List<Result>();
            results = LoadResultsFromDb();
            foreach(var el in results)
            {
                listBox1.Items.Add(el.FirstNumber + " " + el.FirstCurrency + " " + el.MathOperation + " " + el.SecondNumber + " " + el.SecondCurrency + " = " + el.Results + " BYN");
            }
        }

        public Params SetParams()
        {
            Params model = new Params();
            model.FirstNumber = textBox1.Text;
            model.SecondNumber = textBox2.Text;
            model.FirstCurrency = firstCb.Text;
            model.SecondCurrency = secondCb.Text;
            model.MathOperation = operationCb.Text;
            return model;
        }

        public Result SetResultData()
        {
            Result model = new Result();
            model.FirstNumber = textBox1.Text;
            model.SecondNumber = textBox2.Text;
            model.FirstCurrency = firstCb.Text;
            model.SecondCurrency = secondCb.Text;
            model.MathOperation = operationCb.Text;
            model.Results = resultTextBox.Text;
            model.Date = DateTime.Now;
            return model;
        }

        string SaveResults(Result model)
        {
            string url = "https://localhost:7201/currencies/saveResult";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(model);
                streamWriter.Write(json);
            }
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                string responseBody = reader.ReadToEnd();
                return responseBody;
            }
        }

        string SaveCurrenciesToday()
        {
            string url = "https://localhost:7201/currencies/saveCurrenciesToday";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
      
        public List<Currency> GetDeparsedCurrencies(string json)
        {
            List<Currency> data = JsonConvert.DeserializeObject<List<Currency>>(json);
            return data;
        }

        public List<Currency> LoadFromDb()
        {
            string url = "https://localhost:7201/currencies/gets";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string responseBody = reader.ReadToEnd();
                return GetDeparsedCurrencies(responseBody);
            }
        }

        public List<Result> LoadResultsFromDb()
        {
            string url = "https://localhost:7201/currencies/getResults";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string responseBody = reader.ReadToEnd();
                return GetDeparsedResults(responseBody);
            }
        }

        public List<Result> GetDeparsedResults(string json)
        {
            List<Result> results = JsonConvert.DeserializeObject<List<Result>>(json);
            return results;
        }

        public string Calculate(Params model)
        {
            string url = "https://localhost:7201/currencies/calculate";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "POST";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(model);
                streamWriter.Write(json);
            }
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                string responseBody = reader.ReadToEnd();
                string result = JsonConvert.DeserializeObject<string>(responseBody);
                return result;
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ///
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
