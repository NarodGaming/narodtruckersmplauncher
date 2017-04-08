' Created by Narod in 2017
' This code is under the MIT License
' Find the repository at: https://github.com/NarodGaming/narodtruckersmplauncher
' You must keep this message at the top of all the code files.

Public Class CrashHandler
    Public Function HandleCrash(ex As Exception) ' when a crash is passed on through try & catch
        Me.Show() ' become visible to stop program closing
        If ex.HelpLink = Nothing Then ' to stop a nullrefrenceexception
            ex.HelpLink = "A help link could not be found."
        End If ' log is started
        crash_TextLog.Text = "-- YART (Yet Another TruckersMP Launcher) has unfortunately crashed --

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