using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Maui.Graphics.Text;

namespace MathOperators;

public partial class MainPage : ContentPage
{
    /// <summary>
    /// Remembers the expressions calculated by user
    /// </summary>
    private ObservableCollection<string> _expList;
    public MainPage()
    {
        InitializeComponent();
        
        _expList = new ObservableCollection<string>();
        _lstExpHistory.ItemsSource = _expList;

    }

    //Take the values provided in the input fields and perform the selected operation on them
    private async void OnCalculateFields(object sender, EventArgs e)
    {
        try
        {
            //Get User Input for operation
            var firstOperand = ReadFirstOperand();
            var secondOperand = ReadSecondOperand();
            var opEntry = ReadOpEntry();

            //Perform the operation and obtain result --> Method
            double result = PerformArithmeticOperation(opEntry, firstOperand, secondOperand);
            //Output result to field. Show the work
            string expression = $"{firstOperand} {opEntry} {secondOperand} = {result}";
        
            //Remember the expression
            _expList.Add(expression);

            _txtMathExp.Text = null;
            _txtMathExp.Text = $"{firstOperand} {opEntry} {secondOperand} = {result}";
        }
        catch (ArgumentNullException ex)
        {
            //The user did not provide any input
            await DisplayAlert("Error", "Please provide the required input!", "OK");
        }
        catch (FormatException ex)
        {
            //The user has provided input but it is incorrect (e.g. not a number)
            await DisplayAlert("Error", "Please provide the correct input!", "OK");
        }
        catch (DivideByZeroException ex)
        {
            //The user is executing a division and the denominator is zero
            await DisplayAlert("Error", "Please provide non-zero denominator!", "OK");
        }
    }

    private double ReadSecondOperand()
    {
        string operandInput = _txtRightOp.Text;
        double secondOperand;
        
        if (String.IsNullOrWhiteSpace(operandInput))
        {
            throw new CalculatorException("Please provide a number for the second operand!");
        }

        if(double.TryParse(operandInput, out secondOperand) == false)
        {
            throw new CalculatorException("That's not a number!");
        }
            
        return secondOperand;
    }

    private char ReadOpEntry()
    {
        //Obtain Input
        string opInput = _pckOperand.SelectedItem as string;
        
        //Validate Input

        if (String.IsNullOrWhiteSpace(opInput))
        {
            throw new CalculatorException("Please select one of the arithmetic operators");
        }

        char opEntry = opInput[0];
        
        //Use Input
        return opEntry;
    }

    private double ReadFirstOperand()
    {
        string operandInput = _txtLeftOp.Text;
        double firstOperand;
        
        if (String.IsNullOrWhiteSpace(operandInput))
        {
            throw new CalculatorException("Please provide a number for the second operand!");
        }

        if(double.TryParse(operandInput, out firstOperand) == false)
        {
            throw new CalculatorException("That's not a number!");
        }
            
        return firstOperand;
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