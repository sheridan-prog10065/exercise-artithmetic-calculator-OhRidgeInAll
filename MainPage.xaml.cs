using System.Diagnostics;
using Microsoft.Maui.Graphics.Text;

namespace MathOperators;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    //Take the values provided in the input fields and perform the selected operation on them
    private void OnCalculateFields(object sender, EventArgs e)
    {
        //Get User Input for operation
        double firstOperand = double.Parse(_txtLeftOp.Text);
        double secondOperand = double.Parse(_txtRightOp.Text);
        char opEntry = ((string)_pckOperand.SelectedItem)[0]; //cast to string is possible because SelectedItem is a String

        //Perform the operation and obtain result --> Method
        double result = PerformArithmeticOperation(opEntry, firstOperand, secondOperand);
        //Output result to field. Show the work
        _txtMathExp.Text = $"{firstOperand} {opEntry} {secondOperand} = {result}";
    }

    //Perform the Arithmetic Operation and obtain the result
    private double PerformArithmeticOperation(char opEntry, double firstOperator, double secondOperator)
    {
        switch (opEntry)
        {
            case '+':
                return firstOperator + secondOperator;
            
            case '-':
                return firstOperator - secondOperator;
            
            case '*':
                return firstOperator * secondOperator;
            
            case '/':
                return PerformDivision(firstOperator, secondOperator);
            
            case '%':
                return firstOperator % secondOperator;
            
            default:
                Debug.Assert(false ,"unknown arithmetic operation. Unable to perform arithmetic operation.");
                return 0;
        }
    }

    private double PerformDivision(double firstOperator, double secondOperator)
    {
        string divOp = _pckOperand.SelectedItem as string;
        if (divOp.Contains("Int", StringComparison.OrdinalIgnoreCase))
        {
            //Integer division
            int result = (int)firstOperator / (int)secondOperator;
            return result;
        }
        else
        {
            //Real division
            return firstOperator / secondOperator;
        }
    }
}