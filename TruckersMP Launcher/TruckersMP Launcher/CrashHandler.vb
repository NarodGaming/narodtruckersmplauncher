Public Class CrashHandler
    Public Function HandleCrash(ex As Exception)
        Me.Show()
        If ex.HelpLink = Nothing Then
            ex.HelpLink = "A help link could not be found."
        End If
        crash_TextLog.Text = "-- Narod's TruckersMP Launcher has crashed --

We apologise for any inconvinience caused. Please send this bug report to Narod on TruckersMP Forums for support

-- Basic Crash Information --
Exception Name: " + ex.Message + " 
Exception Trace: " + ex.Source + " 

-- Extended Crash Information --
Exception Stack Trace: " + ex.StackTrace + " 
Exception Method: " + ex.TargetSite.ToString + " 
Exception Microsoft Help: " + ex.HelpLink + " 

-- Program Information --
Program Version: " + Application.ProductVersion + " 
Program Validity: " + Application.CompanyName + " 
Program Forms: " + Application.OpenForms.ToString + " 
Program Path: " + Application.ExecutablePath + " 

If there is any information in this log you believe is private, please censor this before sending it to us."
    End Function

    Private Sub crash_Close_Click(sender As Object, e As EventArgs) Handles crash_Close.Click
        Application.Exit()
    End Sub

    Private Sub crash_Restart_Click(sender As Object, e As EventArgs) Handles crash_Restart.Click
        Application.Restart()
    End Sub
End Class