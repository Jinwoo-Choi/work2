using System;
using System.Collections.Generic;
using System.Windows;
using System.Timers;
using System.Windows.Media;
using LiveCharts;
using System.Diagnostics;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace work1
{
    /// <summary>
    /// MainPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainPage : Window
    {
        public ChartValues<double> Values1 { get; set; }
        public ChartValues<double> Values2 { get; set; }
        public SeriesCollection SeriesCollection { get; private set; }
        public string[] Labels { get; private set; }
        //public object timer1 { get; private set; }

        mysql manager = new mysql();
        public MainPage()
        {
            InitializeComponent();
            //Values1 = new ChartValues<double> { 3, 4, 6, 3, 2, 6 };
            //Values2 = new ChartValues<double> { 1, 4, 3, 7, 2, 6 };
            //DataContext = this;
            //List<double> allValues = new List<double>();
            ////if (CON.State == ConnectionState.Open)
            ////{
            ////    CON.Close();
            ////}

            ////CON.ConnectionString = ConfigurationManager.ConnectionStrings["conDB"].ConnectionString;
            ////CON.Open();
            ////CMD = new SqlCommand("select * from tblWeeklyAudit", CON);
            ////RDR = CMD.ExecuteReader();
            ////while (RDR.Read())
            ////{
            ////    allValues.Add(Convert.ToDouble(RDR["Defects"]));
            ////}
            //allValues.Add(21);
            //allValues.Add(10);
            //SeriesCollection = new SeriesCollection
            //{
            //    new LineSeries
            //    {
            //        Values = new ChartValues<double>(allValues)
            //    }
            //};

            //Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };

            ////Values1 = new ChartValues<double> { 3, 4, 6, 3, 2, 6 };
            //DataContext = this;

            //범례 위치 설정
            chart.LegendLocation = LiveCharts.LegendLocation.Top;

            //세로 눈금 값 설정
            chart.AxisY.Add(new LiveCharts.Wpf.Axis { MinValue = 0, MaxValue = 1000 });

            //가로 눈금 값 설정
            chart.AxisX.Add(new LiveCharts.Wpf.Axis { Labels = new string[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" } });



            //모든 항목 지우기
            chart.Series.Clear();

            //항목 추가
            chart.Series.Add(new LiveCharts.Wpf.LineSeries()
            {
                Title = "Sample1",
                Stroke = new SolidColorBrush(Colors.Green),
                Values = new LiveCharts.ChartValues<double>(new List<double> { 700, 200, 300, 400, 500, 600, 700, 800, 900, 90, 211, 220 })
            }
            );
            //List<double> SQLValues = new List<double>();
            //SQLValues.Add(Convert.ToDouble(10));
            //SQLValues.Add(20);
            //SQLValues.Add(30);
            //SQLValues.Add(40);
            List<double> SQLValues = new List<double>(manager.GetData("data", 1));
            chart.Series.Add(new LiveCharts.Wpf.LineSeries()
            {
                Title = "Title",
                Stroke = new SolidColorBrush(Colors.Red),
                //Values = new LiveCharts.ChartValues<double>(new List<double> { 70, 20, 100, 140, 50, 60, 70, 80, 90, 100, 111, 120 })
                Values = new LiveCharts.ChartValues<double>(SQLValues)
            }
            );
            Timer timer1 = new System.Timers.Timer();
            timer1.Interval = 1000;
            // Hook up the Elapsed event for the timer. 
            timer1.Elapsed += OnTimedEvent;

            // Have the timer fire repeated events (true is the default)
            timer1.AutoReset = true;

            // Start the timer
            timer1.Enabled = true;
            static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
            {
                Debug.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);

            }
        } 
    }
}

