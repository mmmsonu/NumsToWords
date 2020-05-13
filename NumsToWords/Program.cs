using System;

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
            gw.StartProcess();

            Console.WriteLine("ok");

        }
    }
}


public class GenerateWords
{
    private string _inputs = "";
    private string _errmsg = "";

    public GenerateWords(string inputs)
    {
        _inputs = inputs;
    }

    public string GetErrorMessage {
        get
        {
            return _errmsg;
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
        else if (_inputs.Length > 11)
        {
            retval = false;
            _errmsg = "Number upto 11 digit are allowed !!";
        }
        else if (!isNumber)
        { 
            retval = false;
            _errmsg = "Inputs should be number only !!";
        }
        return retval;
    }

    internal void StartProcess()
    {
        var formatinput = "00000000000" + _inputs;
        Console.WriteLine(formatinput);
    }
}