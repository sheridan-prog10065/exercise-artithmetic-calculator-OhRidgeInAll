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
        double firstOperator = double.Parse(_txtLeftOp.Text);
        double secondOperator = double.Parse(_txtRightOp.Text);
        char opEntry = ((string)_pckOperand.SelectedItem)[0]; //cast to string is possible because SelectedItem is a String

        //Perform the operation and obtain result --> Method
        double result = PerformArithmeticOperation(opEntry, firstOperator, secondOperator);
        //Output result to field. Show the work
        _txtMathExp.Text = $"{firstOperator} {opEntry} {secondOperator} = {result}";
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
                return firstOperator / secondOperator;
            
            case '%':
                return firstOperator % secondOperator;
            
            default:
                Debug.Assert(false ,"unknown arithmetic operation. Unable to perform arithmetic operation.");
                return 0;
        }
    }
}