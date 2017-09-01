using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPE200Lab1
{
    public partial class MainForm : Form
    {
        private bool containDot;
        private bool isAllowBack;
        private bool isAfterOperater;
        private bool isAfterEqual;
        private string firstOperand ;
        private string secondOperand;
        private string operate;

        private string operateMod;
        private string keepMod;
        private string firstNum;
        private string secondNum ;
        private string resultMod;
        private bool isMod = false;
        private string operateOfMod;
        string mem="";

        CalculatorEngine engine = new CalculatorEngine();

        private void resetAll()
        {
            lblDisplay.Text = "0";
            isAllowBack = true;
            containDot = false;
            isAfterOperater = false;
            isAfterEqual = false;
            firstNum = null;
            firstOperand = null;
        }
        
        public MainForm()
        {
            InitializeComponent();
            resetAll();
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterOperater)
            {
                lblDisplay.Text = "0";
            }
            if(lblDisplay.Text.Length is 8)
            {
                return;
            }
            isAllowBack = true;
            string digit = ((Button)sender).Text;// งงดาด
            if(lblDisplay.Text is "0")
            {
                lblDisplay.Text = "";
            }
            lblDisplay.Text += digit;
            isAfterOperater = false;
        }
        
        private void btnOperator_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterOperater)
            {
                return;
            }

            operateOfMod = operate;
            operate = ((Button)sender).Text;

            if (firstOperand != null )
            {
                secondOperand = lblDisplay.Text;
                if(operate == "%")
                {
                    isMod = true;
                    resultMod = engine.calculate(operate, firstOperand, lblDisplay.Text);
                    firstNum = firstOperand;
                    lblDisplay.Text = resultMod;
                    operate = operateOfMod;
                }
                else {
                    string result = engine.calculate(operate, firstOperand, secondOperand);
                    if (result is "E" || result.Length > 8)
                    {
                        lblDisplay.Text = "Error";
                    }
                    else
                    {
                        lblDisplay.Text = result;
                        firstOperand = result;
                    }
                }
                
            }
            switch (operate)
            {
                case "+": 
                case "-": 
                case "X": 
                case "÷":
                    firstOperand = lblDisplay.Text;
                    
                    break;
               case "%":
                    operateMod = "%";
                    isMod = true;
                    resultMod = engine.calculate(operate, firstOperand,lblDisplay.Text);
                    lblDisplay.Text =  resultMod;
                    operate = operateOfMod;
            
                    break;
            }
            isAfterOperater = true;
            containDot = false;
            isAllowBack = false;
        }
        private void btnEqual_Click(object sender, EventArgs e){

            string result;
            if (isAfterEqual)
            {
                lblDisplay.Text = engine.calculate(operate, lblDisplay.Text, firstOperand);
                return;
            }
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isMod){
                string secondOperand = lblDisplay.Text;
                result = engine.calculate( operate , firstNum, secondOperand);
                isMod = false;
            }
            else{
                string secondOperand = lblDisplay.Text;
                result = engine.calculate(operate,firstOperand,secondOperand);
                firstOperand = secondOperand;
            }
            
            if (result is "E" || result.Length > 8){
                lblDisplay.Text = "Error";
            }
            else{
                lblDisplay.Text = result;
            }
            
            isAfterEqual = true;
            
        }

        private void btnDot_Click(object sender, EventArgs e){
            if (lblDisplay.Text is "Error"){
                return;
            }
            if (isAfterEqual){
                resetAll();
            }
            if (lblDisplay.Text.Length is 8){
                return;
            }
            if (!containDot){
                lblDisplay.Text += ".";
                containDot = true;
            }
        }

        private void btnSign_Click(object sender, EventArgs e){
            if (lblDisplay.Text is "Error"){
                return;
            }
            // already contain negative sign
            if (lblDisplay.Text.Length is 8){
                return;
            }
            if(lblDisplay.Text[0] is '-'){
                lblDisplay.Text = lblDisplay.Text.Substring(1, lblDisplay.Text.Length - 1);
            }
            else{
                lblDisplay.Text = "-" + lblDisplay.Text;
            }
        }

        private void btnClear_Click(object sender, EventArgs e){
            resetAll();
        }

        private void btnBack_Click(object sender, EventArgs e){
            if (lblDisplay.Text is "Error"){
                return;
            }
            if (isAfterEqual){
                return;
            }
            if (!isAllowBack){
                return;
            }
            if(lblDisplay.Text != "0"){
                string current = lblDisplay.Text;
                char rightMost = current[current.Length - 1];
                if(rightMost is '.'){
                    containDot = false;
                }
                lblDisplay.Text = current.Substring(0, current.Length - 1);
                if(lblDisplay.Text is "" || lblDisplay.Text is "-"){
                    lblDisplay.Text = "0";
                }
            }
        }

        private void btnSqrt_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                return;
            }
            string current = lblDisplay.Text;
            double sqrtResult = Math.Sqrt(Convert.ToDouble(current));
            lblDisplay.Text = sqrtResult.ToString();
        }
        
        private void btnM_Click(object sender, EventArgs e)
        {
            string memoIcon;
            string current = lblDisplay.Text;
            memoIcon = ((Button)sender).Text;
            switch (memoIcon)
            {
                case "MC":
                    mem = "0";
                    break;
                case "MS":
                    mem = current; break;
                case "MR":
                    lblDisplay.Text = mem;
                    break;
                case "M+":
                    mem = Convert.ToString(Convert.ToDouble(mem) + Convert.ToDouble(current));
                    break;
                case "M-":
                    mem = Convert.ToString(Convert.ToDouble(mem) - Convert.ToDouble(current));
                    break;
            } 

        }
        private void btnOneover_Click(object sender, EventArgs e)
        {
            string numOver;
            double overResult;
            numOver = lblDisplay.Text;
            overResult = (1 / (Convert.ToDouble(numOver)));
            lblDisplay.Text = Convert.ToString(overResult);
        }
    }
}
