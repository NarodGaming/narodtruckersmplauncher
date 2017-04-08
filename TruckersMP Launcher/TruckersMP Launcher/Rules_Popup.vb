Public Class Rules_Popup
    Private Sub Rules_Popup_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Function RecieveRules(rules As String)
        rules_important.Text = rules
        Me.Show()
        rules_important.Focus()
        rules_important.SelectionStart = rules_important.Text.Length
    End Function

    Private Sub btn_decline_Click(sender As Object, e As EventArgs) Handles btn_decline.Click
        Me.Close()
        MsgBox("You will have to agree to the rules to play online.", MsgBoxStyle.Exclamation, "Warning! You will have to accept to play.")
    End Sub

    Private Sub btn_accept_Click(sender As Object, e As EventArgs) Handles btn_accept.Click
        My.Settings.RulesShown = True
        My.Settings.Save()

        Me.Close()

        Form1.RulesDone()
    End Sub
End Class