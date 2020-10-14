'Project:     Lab 4
'Programmer:  Ronish Shrestha
'Date:        Fall 2018
'Description: This project maintains a checking account balance.
'             The requested transaction is calculated and 
'             the new balance is displayed.
'             A summary includes all transactions.

Public Class CheckingForm

    'Class Level
    Dim BalanceDecimal As Decimal = 100 'class level, doesnt go away till program restarts       
    Const SERVICE_CHARGE_DECIMAL As Decimal = 10 'be our default service charge
    Dim TotalDepositCountInteger, TotalChecksCountInteger, TotalServiceCountInteger As Integer 'keep track of # of deposits
    Dim TotalDepositAmountDecimal, TotalChecksAmountDecimal, TotalServiceAmountInteger As Decimal 'keep track of deposit amounts

    Private Sub ExitButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitButton.Click
        'End the program

        Me.Close()
    End Sub

    Private Sub CalculateTextBox_Click(sender As Object, e As EventArgs) Handles CalculateTextBox.Click
        'Calculate the transaction and display the new balance.
        Dim AmountDecimal As Decimal

        'If (DepositRadioButton.Checked = True) Or (CheckRadioButton.Checked = True) Or (ChargeRadioButton.Checked = True) Then
        If DepositRadioButton.Checked Or CheckRadioButton.Checked Or ChargeRadioButton.Checked Then
            Try
                AmountDecimal = Decimal.Parse(AmountTextBox.Text)

                'Calculate each transaction and keep track of summary information.

                'If DepositRadioButton.Checked = True Then
                'If(DepositRadioButton.Checked = True) then
                If DepositRadioButton.Checked Then
                    'they selected a deposit, we need to add to our balance
                    BalanceDecimal = BalanceDecimal + AmountDecimal  'add to our balance
                    'BalanceDecimal += AmountDecimal
                    'increment out totals
                    TotalDepositCountInteger += 1
                    'TotalDepositAmountDecimal = TotalDepositAmountDecimal + AmountDecimal
                    TotalDepositAmountDecimal += AmountDecimal

                ElseIf CheckRadioButton.Checked Then
                    'they selected a check, we need check for sufficient funds  
                    If AmountDecimal > BalanceDecimal Then
                        'not enough money
                        MessageBox.Show("You don't have enough money in your account",
                                        "Insufficient Funds")
                        BalanceDecimal = BalanceDecimal - SERVICE_CHARGE_DECIMAL
                    Else
                        'we have enough money, remove from the account
                        BalanceDecimal -= AmountDecimal

                        TotalChecksCountInteger += 1
                        'TotalChecksCountInteger = TotalChecksCountInteger +1
                        TotalChecksAmountDecimal += AmountDecimal

                    End If

                Else
                    'its not deposit, its not a check, must be a service charge
                    'subtracr from our balance
                    BalanceDecimal = BalanceDecimal - AmountDecimal  ' subtract seevice charge from balance
                    TotalServiceAmountInteger += AmountDecimal        ' add total service charge 
                    TotalServiceCountInteger += 1                     ' increment service charge counter


                End If
                ' output our result 
            Catch AmountException As FormatException
                MessageBox.Show("Please make sure that only numeric data has been entered.",
                    "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                With AmountTextBox
                    .Focus()
                    .SelectAll()
                End With
            Catch AnyException As Exception
                MessageBox.Show("Error: " & AnyException.Message)
            End Try
        Else
            MessageBox.Show("Please select deposit, check, or service charge", "Input needed")
        End If
        'output our result
        BalanceTextBox.Text = BalanceDecimal.ToString("C")


        'reset the form
        'try not to duplicate code
        ' ClearTextBox.PerformClick() 'can cause the CLICK for the button
        'call the event handler
        ClearTextBox_Click(sender, e)
    End Sub

    Private Sub CheckingForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'only One Time, When the program Starts.

        'output my initial blalance
        BalanceTextBox.Text = BalanceDecimal.ToString("c")


    End Sub

    Private Sub ClearTextBox_Click(sender As Object, e As EventArgs) Handles ClearTextBox.Click
        'reset our form for next transaction
        'If DepositRadioButton.Checked Then
        'unchekc it
        'DepositRadioButton.Checked = False
        'ElseIf CheckRadioButton.Checked Then
        'CheckRadioButton.Checked = False
        'Else
        'ChargeRadioButton.Checked = False
        'End If

        DepositRadioButton.Checked = True   ' Unselect any radiobutton in this group that was selected
        DepositRadioButton.Checked = False 'unselect depositradiobutton

        AmountTextBox.Clear() 'clear it
        AmountTextBox.Focus() 'put the cursor 
    End Sub

    Private Sub SummaryButton_Click(sender As Object, e As EventArgs) Handles SummaryButton.Click
        ' Display Summary
        Dim MessageString As String
        MessageString = "Total # of Deposits: " & TotalDepositCountInteger.ToString & vbCrLf
        'MessageString = "Total # of Deposits: " & TotalDepositCountInteger.ToString & ControlChars.NewLine
        'MessageString = "Total # of Deposits: " & TotalDepositCountInteger.ToString & Environment.NewLine   
        ' MessageString= MessageString &
        ' MessageString &= "Total amount of Deposits: " & TotalDepositAmountDecimal.ToString("c") ControlChars.NewLine
        'MessageString &= "Total # of checks: " & TotalChecksCountInteger.ToString & ControlChars.NewLine
        ' MessageString &= "Total Amount of checks: " & TotalChecksAmountDecimal.ToString & ControlChars.NewLine

        MessageString = String.Format("Total # of Deposits: {1}{0}" &
                                      "Total Amount of Deposits : {2:c}{0}" &
                                      "Total # of checks: {3}{0}" &
                                      "Total Amount of checks: {4:c}{0}" &
                                      "Total # of service charge: {5}{0}" &
                                      "Total Amount of Service Charge: {6:c}{0}",
                                      ControlChars.NewLine,
                                      TotalDepositCountInteger, TotalDepositAmountDecimal,
                                      TotalChecksCountInteger, TotalChecksAmountDecimal,
                                      TotalServiceCountInteger, TotalServiceAmountInteger
                                      )

        MessageBox.Show(MessageString, "Account Summary")


    End Sub
End Class

