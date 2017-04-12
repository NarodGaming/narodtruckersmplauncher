' Created by Narod in 2017
' This code is under the MIT License
' Find the repository at: https://github.com/NarodGaming/narodtruckersmplauncher
' You must keep this message at the top of all the code files.

Public Class Rules_Popup

    Public Function RecieveRules(rules As String) ' recieve rules (stops requirement of reimporting JSON
        rules_important.Text = rules ' sets the textbox to passed through rules list
        Me.Show() ' becomes visible
        rules_important.Focus() ' stops text from being highlighted
        rules_important.SelectionStart = rules_important.Text.Length

        Return Nothing ' fixes a warning
    End Function

    Private Sub btn_decline_Click(sender As Object, e As EventArgs) Handles btn_decline.Click
        Me.Close() ' do not accept
        MsgBox("You will have to agree to the rules to play online.", MsgBoxStyle.Exclamation, "Warning! You will have to accept to play.")
    End Sub

    Private Sub btn_accept_Click(sender As Object, e As EventArgs) Handles btn_accept.Click
        My.Settings.RulesShown = True ' accepted, remember this for the future
        My.Settings.Save()

        Me.Close()

        Form1.RulesDone() ' opens the play panel
    End Sub
End Class