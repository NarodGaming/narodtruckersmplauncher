' Created by Narod in 2017
' This code is under the MIT License
' Find the repository at: https://github.com/NarodGaming/narodtruckersmplauncher
' You must keep this message at the top of all the code files.

Imports System.Net
Imports System.IO
Imports System.Web.Script.Serialization ' json parsing

Public Class FirstRun

    Private Sub yart_id_TextChanged(sender As Object, e As EventArgs) Handles yart_id.TextChanged
        yart_id_status.Image = My.Resources.loading_circle
        ' validate id
        Dim playerinfo As String = "https://api.truckersmp.com/v2/player/" + yart_id.Text
        Dim pclient As WebClient = New WebClient()
        Dim preader As StreamReader = New StreamReader(pclient.OpenRead(playerinfo))
        Dim pjson As String = preader.ReadToEnd()


        Dim rules As Object = New JavaScriptSerializer().Deserialize(Of Object)(pjson)
        Dim errorname = rules("error")

        If errorname = "false" Then
            yart_id_status.Image = My.Resources.green_tick
            yart_leave_button.Visible = True
        Else
            yart_id_status.Image = My.Resources.red_cross
            yart_leave_button.Visible = False
        End If
    End Sub

    Private Sub yart_leave_button_Click(sender As Object, e As EventArgs) Handles yart_leave_button.Click
        My.Settings.FirstRun = False
        My.Settings.ID = yart_id.Text
        My.Settings.Save()
        Form1.Show()
        Me.Dispose()
    End Sub

    Private Sub yart_link_lbl_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles yart_link_lbl.LinkClicked
        Process.Start("https://steamid.io/")
    End Sub
End Class