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

    internal bool StartProcess()
    {

        bool retval = true;

        var formatinput =  _inputs.PadLeft(digitMaxLen,'0');
        formatinput = formatinput.Substring(formatinput.Length - digitMaxLen);  // Taking 11
        Console.WriteLine(formatinput);

        return retval;
    }
}