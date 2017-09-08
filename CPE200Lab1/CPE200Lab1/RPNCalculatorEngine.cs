using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CPE200Lab1
{
    class RPNCalculatorEngine : CalculatorEngine
    {
        public string Process(string str)
        {
            string result;
            Stack rpnStack = new Stack();
            String[] parts = str.Split(' ');
            for (int i = 0; i < parts.Length; i++)
            {
                if (isNumber(parts[i]))
                {
                    rpnStack.Push(parts[i]);
                }
                else if (parts[i] == " ")
                {
                    break;
                }
                else
                if (isOperator(parts[i]))
                {
                    String second = rpnStack.Pop().ToString();
                    String first = rpnStack.Pop().ToString();
                    result = calculate(parts[i], first, second);
                    rpnStack.Push(result);
                }
            }
            result = rpnStack.Pop().ToString();
            return result;
        }
    }
}
