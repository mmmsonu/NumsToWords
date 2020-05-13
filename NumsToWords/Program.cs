using System;
using System.Collections.Generic;

namespace NumsToWords
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            dynamic inputs = Console.ReadLine();

            var gw = new GenerateWords(inputs.ToString());
            if (!gw.IsInputsValid())
            {
                Console.WriteLine(gw.GetErrorMessage);
                return;
            }
            var isSuccess = gw.StartProcess();
            if (isSuccess)
            {
                Console.WriteLine("Final output --> " + gw.GetGeneratedWords);
            }
        }
    }
}


public class GenerateWords
{
    private string _inputs = "";
    private string _errmsg = "";
    private string _outputs = "";

    const int digitMaxLen = 11;

    public GenerateWords(string inputs)
    {
        _inputs = inputs.Trim();
    }

    public string GetErrorMessage {
        get
        {
            return _errmsg;
        }
    }

    public string GetGeneratedWords
    {
        get
        {
            return _outputs;
        }
    }

    public bool IsInputsValid()
    {
        bool retval = true;

        double num;
        bool isNumber = double.TryParse(_inputs, out num);

        if (string.IsNullOrWhiteSpace(_inputs.Trim()))
        {
            retval = false;
            _errmsg = "Please provide input !!";
        }
        else if (_inputs.Contains("."))
        {
            retval = false;
            _errmsg = "Decmals not allowed !!";
        }
        else if (_inputs.Length > digitMaxLen)
        {
            retval = false;
            _errmsg = $"Number upto {digitMaxLen} digit are allowed !!";
        }
        else if (!isNumber)
        { 
            retval = false;
            _errmsg = "Inputs should be number only !!";
        }
        return retval;
    }

    internal class Series
    {
        public string SeriesName { get; set; }
        public string SeriesValue { get; set; }
    }

    internal bool StartProcess()
    {

        bool retval = true;
        
        string[] arrSingle = new string[10] { "One","Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Zero" };
        string[] arr11to19 = new string[9] { "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        string[] arr20to90 = new string[8] { "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninty" };

        const string TenthPositions = "10,20,30,40,50,60,70,80,90";

        var formatoutput = "";
        var formatinput =  _inputs.PadLeft(digitMaxLen,'0');
        formatinput = formatinput.Substring(formatinput.Length - digitMaxLen);  // Taking 11

        var series = new List<Series>();
        series.Add(new Series() { SeriesName = "Arab", SeriesValue = formatinput.Substring(0, 2) });
        series.Add(new Series() { SeriesName = "Crore", SeriesValue = formatinput.Substring(2, 2) });
        series.Add(new Series() { SeriesName = "Lakh", SeriesValue = formatinput.Substring(4, 2) });
        series.Add(new Series() { SeriesName = "Thousand", SeriesValue = formatinput.Substring(6, 2) });
        series.Add(new Series() { SeriesName = "Hundred", SeriesValue = "0" + formatinput.Substring(8, 1) });
        series.Add(new Series() { SeriesName = "", SeriesValue = formatinput.Substring(9, 2) });

        foreach (var item in series)
        {
            int i = Convert.ToInt32(item.SeriesValue);
            int firstPosDigit = Convert.ToInt32(item.SeriesValue.Substring(0, 1));
            int secondPosDigit = Convert.ToInt32(item.SeriesValue.Substring(1, 1));

            if (i != 0)
            {
                if (i >= 1 && i <= 9)
                {
                    formatoutput = formatoutput + " " + arrSingle[i];
                }
                else if (i >= 11 && i <= 19)
                {
                    formatoutput = formatoutput + " " + arr11to19[i];
                }
                else if (TenthPositions.Contains(item.SeriesValue))
                {
                    formatoutput = formatoutput + " " + arr20to90[i];
                }
                else if ((i > 20) &&
                        (secondPosDigit != 0))
                {
                    //This condition executes for any condition where i is greater then 20 and digit on 2nd position is != 0
                    formatoutput = formatoutput + " " + arr20to90[firstPosDigit];
                    formatoutput = formatoutput + " " + arrSingle[secondPosDigit];
                }

                //Adding suffixes to digit
                formatoutput = formatoutput + " " + item.SeriesName;
            }
        } 

        Console.WriteLine(formatinput);

        _outputs = "INR " + formatoutput + " only";

        return retval;
    }
}
