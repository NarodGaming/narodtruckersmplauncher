' Created by Narod in 2017
' This code is under the MIT License
' Find the repository at: https://github.com/NarodGaming/narodtruckersmplauncher
' You must keep this message at the top of all the code files.

Imports System.IO ' required for checking files
Imports System.Net ' general
Imports System.Security.Cryptography ' comparing md5's for updating
Imports Newtonsoft.Json.Linq ' json parsing
Imports System.Web.Script.Serialization ' json parsing
Imports System.Threading

Public Class Form1

    Private Const BUF_SIZE As Integer = 65536
    ''' <summary>
    ''' Returns the file integrity checksum hash, otherwise an empty string.
    ''' </summary>
    Public Shared Function IntegrityCheck(ByVal filePath As String) As String ' method for finding md5
        Dim dataBuffer(BUF_SIZE - 1) As Byte ' setup variables
        Dim dataBufferDummy(BUF_SIZE - 1) As Byte
        Dim dataBytesRead As Integer = 0
        Dim hashResult As String = String.Empty
        Dim hashAlg As HashAlgorithm = Nothing
        Dim fs As FileStream = Nothing

        Try
            hashAlg = New MD5CryptoServiceProvider ' create md5 algorithm
            fs = New FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None, BUF_SIZE) ' opens file
            Do
                dataBytesRead = fs.Read(dataBuffer, 0, BUF_SIZE) ' read bytes
                hashAlg.TransformBlock(dataBuffer, 0, dataBytesRead, dataBufferDummy, 0)
            Loop Until dataBytesRead = 0 ' loop
            hashAlg.TransformFinalBlock(dataBuffer, 0, 0)
            hashResult = BitConverter.ToString(hashAlg.Hash).Replace("-", "").ToLower
        Catch ex As IOException ' game file not found, should be handled earlier, but better to be safe
            Return Nothing
        Catch ex As UnauthorizedAccessException ' no permission
            CrashHandler.HandleCrash(ex)
            MsgBox("A security exception has been thrown, try running the launcher as an admin.")
            Form1.Dispose()
        Finally
            If Not fs Is Nothing Then
                fs.Close()
                fs.Dispose()
                fs = Nothing
            End If
            If Not hashAlg Is Nothing Then
                hashAlg.Clear()
                hashAlg = Nothing
            End If
        End Try
        Return hashResult ' return the md5
    End Function

    Public TickTock As Integer = 30 ' 30 second wait on the timer
    Public Excepted As Exception ' exceptions
    Public NewsShown As Boolean ' for checking if user has navigated off TruckersMP
    Public FirstTime As Boolean = My.Settings.FirstRun

    Public Class sresponse
        Public Property id As Integer
        Public Property game As String
        'Public Property ip As String           these are not yet used, but may be in the future
        'Public Property port As Integer
        Public Property name As String
        Public Property shortname As String
        Public Property online As Boolean
        Public Property players As Integer
        Public Property queue As Integer
        Public Property maxplayers As Integer
        Public Property speedlimiter As Integer
    End Class

    'Public Class Permissions                          used, but not uncommented
    'Public Property isGameAdmin As Boolean
    'End Class

    Public Class presponse
        Public Property pid As Integer
        Public Property pname As String
        Public Property avatar As String
        Public Property joinDate As String
        Public Property steamID64 As Long
        Public Property groupName As String
        'Public Property permissions As Permissions     commented out same as class
    End Class

    Public Class bresponse ' json
        Public Property expiration As String
        Public Property timeAdded As String
        Public Property active As Boolean
        Public Property reason As String
        Public Property adminName As String
        Public Property adminID As Integer
    End Class

    Public Class rresponse ' json
        'Public Property error As Boolean
        Public Property rules As String
        'Public Property revision As Integer
    End Class

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Hide()

        If File.Exists(Application.StartupPath + "\update.bat") Then
            File.Delete(Application.StartupPath + "\update.bat")
        End If

        pnl_server.Visible = False

        If My.Settings.FirstRun = True Then
            FirstRun.Show()
            Me.Close()
        End If

        Dim playerinfo As String = "https://api.truckersmp.com/v2/player/" + My.Settings.ID
        Dim pclient As WebClient = New WebClient()
        Dim preader As StreamReader = New StreamReader(pclient.OpenRead(playerinfo))
        Dim pjson As String = preader.ReadToEnd()

        Try
            Dim nameinfo As Object = New JavaScriptSerializer().Deserialize(Of Object)(pjson)
            Dim response = nameinfo("response")
            Dim pname = nameinfo("response")("name")
            Dim avatar = nameinfo("response")("avatar")
            Dim groupID = nameinfo("response")("groupID")

            yart_lbl_welcome_name.Text = "Welcome, " + pname
            yart_avatar_pic.ImageLocation = avatar
            Select Case groupID
                Case "1"
                    yart_personalise_text.Text = "Welcome back. Enjoy your trucking!"
                Case "2"
                    yart_personalise_text.Text = "Welcome back. Thanks for your continued development of TruckersMP."
                Case "3"
                    yart_personalise_text.Text = "Welcome back. It's time to catch those rammers."
                Case "4"
                    yart_personalise_text.Text = "Welcome back. Thanks for helping manage TruckersMP."
                Case "5"
                    yart_personalise_text.Text = "Welcome back. Thanks for helping TruckersMP users. Enjoy your trucking."
                Case "6"
                    yart_personalise_text.Text = "Welcome back. -Unknown Role-"
                Case "7"
                    yart_personalise_text.Text = "Welcome back. -Unknown Role-"
                Case "8"
                    yart_personalise_text.Text = "Welcome back. It's time to catch those rammers."
                Case "9"
                    yart_personalise_text.Text = "Welcome back, on behalf of all TruckersMP users, thank you for previously moderating the servers and / or forums."
                Case "10"
                    yart_personalise_text.Text = "Welcome back. Thanks for helping manage TruckersMP."
                Case "11"
                    yart_personalise_text.Text = "Welcome back. Thanks for helping on the forums! Enjoy your trucking."
                Case "12"
                    yart_personalise_text.Text = "Welcome back. Thanks for helping manage TruckersMP."
                Case "13"
                    yart_personalise_text.Text = "Welcome back. -Unknown Role-"
                Case "14"
                    yart_personalise_text.Text = "Welcome back. Thanks for helping manage TruckersMP."
                Case "15"
                    yart_personalise_text.Text = "Welcome back. Hope you get some cool footage / screenshots!"
            End Select

            If yart_personalise_text.Text = "Label1" Then
                yart_personalise_text.Text = "Welcome back. -Unknown Role- -Unknown groupID-"
            End If

        Catch timeout As WebException
            CrashHandler.HandleCrash(timeout)
            Me.Dispose()
        Catch ex As Exception
            My.Settings.FirstRun = True
            FirstRun.Show()
            Me.Dispose()
        End Try

        Try

        Catch ex As Exception

        End Try

        Dim checkversion As WebClient = New WebClient() ' creates webclient for checking version
        checkversion.CachePolicy = New System.Net.Cache.RequestCachePolicy(Cache.RequestCacheLevel.NoCacheNoStore) ' to stop caching
        checkversion.Headers.Add("Cache-control", "no-cache")
        checkversion.Headers.Add("Cache-control", "no-store")
        checkversion.Headers.Add("pragma", "no-cache")
        checkversion.Headers.Add("Expries", "-1")
        Dim versionresponse As String = checkversion.DownloadString("https://narodgaming.ml/yart/currentver.txt?t=" + Date.Now.ToLocalTime) ' adds the date to prevent caching

        Dim YARTVersion As String = versionresponse.Split(" ")(0)
        Dim YARTDate As String = versionresponse.Split(" ")(1)

        If Not (YARTVersion + "-" + YARTDate) = Application.ProductVersion + "-290517" Then ' if out of date
            Dim wouldliketoupdate As MsgBoxResult = MsgBox("A new version is available, would you like to download it?", MsgBoxStyle.YesNo, "An update is available!") ' tells the user to update
            If wouldliketoupdate = MsgBoxResult.Yes Then
                Me.Dispose()
                Thread.Sleep(50)
                LauncherUpdater.Show()
            Else
                MsgBox("OK. Be warned though - new versions will include bugfixes, extra features and much more!", MsgBoxStyle.Information, "Information") ' warns the user
                lbl_latest_launcher_ver.Text = "Latest Launcher Version: " + versionresponse + "!"
            End If
        Else ' up to date
            lbl_latest_launcher_ver.Text = "Latest Launcher Version: " + versionresponse
        End If

        lbl_launcher_ver.Text = "Launcher Version: " + Application.ProductVersion + "-290517" ' sets up product version

        Try
            Dim version As String = "https://api.truckersmp.com/v2/version" ' gets TruckersMP version
            Dim vclient As WebClient = New WebClient() ' uses webclient
            Dim vreader As StreamReader = New StreamReader(vclient.OpenRead(version))
            Dim vjson As String = vreader.ReadToEnd()


            Dim rules As Object = New JavaScriptSerializer().Deserialize(Of Object)(vjson) ' json
            Dim vtruckersmp = rules("name")
            Dim vntruckersmp = rules("numeric")
            Dim struckersmp = rules("stage")
            Dim ets2checkdict = rules("ets2mp_checksum")
            Dim ets2dllhash = rules("ets2mp_checksum")("dll")
            Dim ets2adbhash = rules("ets2mp_checksum")("adb")
            Dim atscheckdict = rules("atsmp_checksum")
            Dim atsdllhash = rules("atsmp_checksum")("dll")
            Dim atsadbhash = rules("atsmp_checksum")("adb")
            Dim timereleased = rules("time")
            Dim ets2suppver = rules("supported_game_version")
            Dim atssuppver = rules("supported_ats_game_version")

            lbl_truckersmp_ver.Text = "TruckersMP Latest Version: " + vtruckersmp
            lbl_supp_ets2.Text = "Supported ETS2 Version: " + ets2suppver
            lbl_supp_ats.Text = "Supported ATS Version: " + atssuppver
            lbl_date_release.Text = "Latest Released on: " + timereleased

            Dim ATSInstall = My.Computer.Registry.GetValue(
    "HKEY_LOCAL_MACHINE\SOFTWARE\TruckersMP", "InstallLocationATS", Nothing) + "\bin\win_x64\amtrucks.exe"

            Dim ETSInstall = My.Computer.Registry.GetValue(
    "HKEY_LOCAL_MACHINE\SOFTWARE\TruckersMP", "InstallLocationETS2", Nothing) + "\bin\win_x64\eurotrucks2.exe"

            If ATSInstall = "\bin\win_x64\amtrucks.exe" Then
                btn_atsmp.Enabled = False
                btn_atssp.Enabled = False
            Else
                Dim CurrATSVersion As FileVersionInfo = FileVersionInfo.GetVersionInfo(ATSInstall)
                yart_current_ats_ver.Text = "Current ATS Version: " + CurrATSVersion.ProductVersion + "s"

                If Not CurrATSVersion.ProductVersion + "s" = atssuppver Then
                    yart_current_ats_ver.Text = yart_current_ats_ver.Text + "!"
                End If
            End If

            If ETSInstall = "\bin\win_x64\eurotrucks2.exe" Then
                btn_ets2mp.Enabled = False
                btn_ets2sp.Enabled = False
            Else
                Dim CurrETS2Version As FileVersionInfo = FileVersionInfo.GetVersionInfo(ETSInstall)
                yart_current_ets2_ver.Text = "Current ETS2 Version: " + CurrETS2Version.ProductVersion + "s"

                If Not CurrETS2Version.ProductVersion + "s" = ets2suppver Then
                    yart_current_ets2_ver.Text = yart_current_ets2_ver.Text + "!"
                End If
            End If

            Dim atsupdateneeded As Boolean = False
            Dim ets2updateneeded As Boolean = False

            If Not File.Exists("C:\ProgramData\TruckersMP\data\ets2\data1.adb") Then
                ets2updateneeded = True
            End If
            If Not File.Exists("C:\ProgramData\TruckersMP\data\ats\data1.adb") Then
                atsupdateneeded = True
            End If

            If ets2updateneeded = False Then
                If Not IntegrityCheck("C:\ProgramData\TruckersMP\data\ets2\data1.adb") = ets2adbhash Then
                    ets2updateneeded = True
                End If
            ElseIf atsupdateneeded = False Then
                If Not IntegrityCheck("C:\ProgramData\TruckersMP\data\ats\data1.adb") = atsadbhash Then
                    atsupdateneeded = True
                End If
            End If

            If ets2updateneeded = True Then
                btn_ets2mp.Text = "Update Euro Truck Simulator 2 MP"
                lbl_current_truckersmp_ver.Text = "Current TruckersMP Version: Out-Of-Date"
            End If
            If atsupdateneeded = True Then
                btn_atsmp.Text = "Update American Truck Simulator MP"
                lbl_current_truckersmp_ver.Text = "Current TruckersMP Version: Out-Of-Date"
            End If
            If ets2updateneeded = False And atsupdateneeded = False Then
                lbl_current_truckersmp_ver.Text = "Current TruckersMP Version: Up-To-Date"
            End If

            If atsadbhash.ToString.Contains("0000") Then
                btn_atsmp.Text = "Open TruckersMP Launcher (ATSMP)" + vbCrLf + "(?)"
                tip_ats_btn.SetToolTip(btn_atsmp, "YART was unable to determine the current version of TruckersMP" + vbCrLf + "This is because of the TruckersMP API not working properly... sorry!")
                lbl_current_truckersmp_ver.Text = "Current TruckersMP Version: Unknown (?)"
                tip_truckersmp_lbl.SetToolTip(lbl_current_truckersmp_ver, "YART was unable to determine the current version of TruckersMP" + vbCrLf + "This is because of the TruckersMP API not working properly... sorry!")
            End If

            If ets2adbhash.ToString.Contains("0000") Then
                btn_ets2mp.Text = "Open TruckersMP Launcher (ETS2MP)" + vbCrLf + "(?)"
                tip_ets2_btn.SetToolTip(btn_ets2mp, "YART was unable to determine the current version of TruckersMP" + vbCrLf + "This is because of the TruckersMP API not working properly... sorry!")
                lbl_current_truckersmp_ver.Text = "Current TruckersMP Version: Unknown (?)"
                tip_truckersmp_lbl.SetToolTip(lbl_current_truckersmp_ver, "YART was unable to determine the current version of TruckersMP" + vbCrLf + "This is because of the TruckersMP API not working properly... sorry!")
            End If

        Catch ex As Exception
            CrashHandler.HandleCrash(ex)
            Me.Dispose()
        End Try




        Dim SuccessOnLoad As Boolean = ServersUpdate()
        If SuccessOnLoad = False Then
            CrashHandler.HandleCrash(Excepted)
            Me.Dispose()
        Else
            refresh_Timer.Start()
        End If
        Try
            Me.Show()
        Catch ex As Exception
            ' just have to catch incase already closed by FirstRun
        End Try
        SplashScreen.Dispose()
    End Sub

    Private Function ServersUpdate()
        Try

            Dim serversadd As String = "https://api.truckersmp.com/v2/servers"
            Dim sclient As WebClient = New WebClient()
            Dim sreader As StreamReader = New StreamReader(sclient.OpenRead(serversadd))
            Dim sjson As String = sreader.ReadToEnd()

            Dim sobj = JObject.Parse(sjson)
            Dim sdata As List(Of JToken) = sobj.Children().ToList
            Dim soutput As String = ""
            Dim sinteger As Integer = 1

            For Each sitem As JProperty In sdata
                sitem.CreateReader()
                Select Case sitem.Name
                    Case "response"
                        soutput += "Servers:" + vbCrLf
                        For Each server As JObject In sitem.Values
                            Dim id As String = server("id")
                            Dim game As String = server("game")
                            Dim sname As String = server("name")
                            Dim sshortname As String = server("shortname")
                            Dim sonline As String = server("online")
                            Dim players As String = server("players")
                            Dim queue As String = server("queue")
                            Dim maxplayers As String = server("maxplayers")
                            Dim speedlimiter As String = server("speedlimiter")
                            soutput += id + vbCrLf + game + vbCrLf + sname + vbCrLf + sshortname + vbCrLf + sonline + vbCrLf + players + vbCrLf + queue + vbCrLf + maxplayers + vbCrLf + speedlimiter + vbCrLf + vbCrLf

                            Select Case sinteger
                                Case "1" ' this whole case should be tidied up, it uses a few hundred lines for something that could probably be shortened
                                    s1_Name.Text = sname
                                    p1_Name.Text = "Players: " + players + " / " + maxplayers
                                    g1_Name.Text = game
                                    If queue = "0" Then
                                        q1_Name.Visible = False
                                        q1_Name.Text = queue
                                    Else
                                        q1_Name.Visible = True
                                        q1_Name.Text = "Now queuing: " + queue
                                    End If
                                    If sonline = "True" Then
                                        st1_Name.Text = "Online: Yes"
                                    Else
                                        st1_Name.Text = "Online: No"
                                    End If
                                    If speedlimiter = "1" Then
                                        sp1_Name.Text = "Speedlimiter: Yes"
                                    Else
                                        sp1_Name.Text = "Speedlimiter: No"
                                    End If
                                    sh1_Name.Text = sshortname
                                    Try
                                        Dim spbar As Decimal = players / maxplayers * 100
                                        t1_Total.Value = spbar
                                        If t1_Total.Value = 100 And queue = "0" Then
                                            q1_Name.Visible = True
                                            q1_Name.Text = "Full, but no queue"
                                        End If
                                    Catch ex As Exception
                                        ' caught an arithmetic overflow if 0 / 0 occurs, this is repeated
                                    End Try
                                    sinteger += 1
                                Case "2"
                                    s2_Name.Text = sname
                                    p2_Name.Text = "Players: " + players + " / " + maxplayers
                                    g2_Name.Text = game
                                    If queue = "0" Then
                                        q2_Name.Visible = False
                                        q2_Name.Text = queue
                                    Else
                                        q2_Name.Visible = True
                                        q2_Name.Text = "Now queuing: " + queue
                                    End If
                                    If sonline = "True" Then
                                        st2_Name.Text = "Online: Yes"
                                    Else
                                        st2_Name.Text = "Online: No"
                                    End If
                                    If speedlimiter = "1" Then
                                        sp2_Name.Text = "Speedlimiter: Yes"
                                    Else
                                        sp2_Name.Text = "Speedlimiter: No"
                                    End If
                                    sh2_Name.Text = sshortname
                                    Try
                                        Dim spbar As Decimal = players / maxplayers * 100
                                        t2_Total.Value = spbar
                                        If t2_Total.Value = 100 And queue = "0" Then
                                            q2_Name.Visible = True
                                            q2_Name.Text = "Full, but no queue"
                                        End If
                                    Catch ex As Exception

                                    End Try
                                    sinteger += 1
                                Case "3"
                                    s3_Name.Text = sname
                                    p3_Name.Text = "Players: " + players + " / " + maxplayers
                                    g3_Name.Text = game
                                    If queue = "0" Then
                                        q3_Name.Visible = False
                                        q3_Name.Text = queue
                                    Else
                                        q3_Name.Visible = True
                                        q3_Name.Text = "Now queuing: " + queue
                                    End If
                                    If sonline = "True" Then
                                        st3_Name.Text = "Online: Yes"
                                    Else
                                        st3_Name.Text = "Online: No"
                                    End If
                                    If speedlimiter = "1" Then
                                        sp3_Name.Text = "Speedlimiter: Yes"
                                    Else
                                        sp3_Name.Text = "Speedlimiter: No"
                                    End If
                                    sh3_Name.Text = sshortname
                                    Try
                                        Dim spbar As Decimal = players / maxplayers * 100
                                        t3_Total.Value = spbar
                                        If t3_Total.Value = 100 And queue = "0" Then
                                            q3_Name.Visible = True
                                            q3_Name.Text = "Full, but no queue"
                                        End If
                                    Catch ex As Exception

                                    End Try
                                    sinteger += 1
                                Case "4"
                                    s4_Name.Text = sname
                                    p4_Name.Text = "Players: " + players + " / " + maxplayers
                                    g4_Name.Text = game
                                    If queue = "0" Then
                                        q4_Name.Visible = False
                                        q4_Name.Text = queue
                                    Else
                                        q4_Name.Visible = True
                                        q4_Name.Text = "Now queuing: " + queue
                                    End If
                                    If sonline = "True" Then
                                        st4_Name.Text = "Online: Yes"
                                    Else
                                        st4_Name.Text = "Online: No"
                                    End If
                                    If speedlimiter = "1" Then
                                        sp4_Name.Text = "Speedlimiter: Yes"
                                    Else
                                        sp4_Name.Text = "Speedlimiter: No"
                                    End If
                                    sh4_Name.Text = sshortname
                                    Try
                                        Dim spbar As Decimal = players / maxplayers * 100
                                        t4_Total.Value = spbar
                                        If t4_Total.Value = 100 And queue = "0" Then
                                            q4_Name.Visible = True
                                            q4_Name.Text = "Full, but no queue"
                                        End If
                                    Catch ex As Exception

                                    End Try
                                    sinteger += 1
                                Case "5"
                                    s5_Name.Text = sname
                                    p5_Name.Text = "Players: " + players + " / " + maxplayers
                                    g5_Name.Text = game
                                    If queue = "0" Then
                                        q5_Name.Visible = False
                                        q5_Name.Text = queue
                                    Else
                                        q5_Name.Visible = True
                                        q5_Name.Text = "Now queuing: " + queue
                                    End If
                                    If sonline = "True" Then
                                        st5_Name.Text = "Online: Yes"
                                    Else
                                        st5_Name.Text = "Online: No"
                                    End If
                                    If speedlimiter = "1" Then
                                        sp5_Name.Text = "Speedlimiter: Yes"
                                    Else
                                        sp5_Name.Text = "Speedlimiter: No"
                                    End If
                                    sh5_Name.Text = sshortname
                                    Try
                                        Dim spbar As Decimal = players / maxplayers * 100
                                        t5_Total.Value = spbar
                                        If t5_Total.Value = 100 And queue = "0" Then
                                            q5_Name.Visible = True
                                            q5_Name.Text = "Full, but no queue"
                                        End If
                                    Catch ex As Exception

                                    End Try
                                    sinteger += 1
                                Case "6"
                                    s6_Name.Text = sname
                                    p6_Name.Text = "Players: " + players + " / " + maxplayers
                                    g6_Name.Text = game
                                    If queue = "0" Then
                                        q6_Name.Visible = False
                                        q6_Name.Text = queue
                                    Else
                                        q6_Name.Visible = True
                                        q6_Name.Text = "Now queuing: " + queue
                                    End If
                                    If sonline = "True" Then
                                        st6_Name.Text = "Online: Yes"
                                    Else
                                        st6_Name.Text = "Online: No"
                                    End If
                                    If speedlimiter = "1" Then
                                        sp6_Name.Text = "Speedlimiter: Yes"
                                    Else
                                        sp6_Name.Text = "Speedlimiter: No"
                                    End If
                                    sh6_Name.Text = sshortname
                                    Try
                                        Dim spbar As Decimal = players / maxplayers * 100
                                        t6_Total.Value = spbar
                                        If t6_Total.Value = 100 And queue = "0" Then
                                            q6_Name.Visible = True
                                            q6_Name.Text = "Full, but no queue"
                                        End If
                                    Catch ex As Exception

                                    End Try
                                    sinteger += 1
                                Case "7"
                                    s7_Name.Text = sname
                                    p7_Name.Text = "Players: " + players + " / " + maxplayers
                                    g7_Name.Text = game
                                    If queue = "0" Then
                                        q7_Name.Visible = False
                                        q7_Name.Text = queue
                                    Else
                                        q7_Name.Visible = True
                                        q7_Name.Text = "Now queuing: " + queue
                                    End If
                                    If sonline = "True" Then
                                        st7_Name.Text = "Online: Yes"
                                    Else
                                        st7_Name.Text = "Online: No"
                                    End If
                                    If speedlimiter = "1" Then
                                        sp7_Name.Text = "Speedlimiter: Yes"
                                    Else
                                        sp7_Name.Text = "Speedlimiter: No"
                                    End If
                                    sh7_Name.Text = sshortname
                                    Try
                                        Dim spbar As Decimal = players / maxplayers * 100
                                        t7_Total.Value = spbar
                                        If t7_Total.Value = 100 And queue = "0" Then
                                            q7_Name.Visible = True
                                            q7_Name.Text = "Full, but no queue"
                                        End If
                                    Catch ex As Exception

                                    End Try
                                    sinteger += 1
                                Case "8"
                                    s8_Name.Text = sname
                                    p8_Name.Text = "Players: " + players + " / " + maxplayers
                                    g8_Name.Text = game
                                    If queue = "0" Then
                                        q8_Name.Visible = False
                                        q8_Name.Text = queue
                                    Else
                                        q8_Name.Visible = True
                                        q8_Name.Text = "Now queuing: " + queue
                                    End If
                                    If sonline = "True" Then
                                        st8_Name.Text = "Online: Yes"
                                    Else
                                        st8_Name.Text = "Online: No"
                                    End If
                                    If speedlimiter = "1" Then
                                        sp8_Name.Text = "Speedlimiter: Yes"
                                    Else
                                        sp8_Name.Text = "Speedlimiter: No"
                                    End If
                                    sh8_Name.Text = sshortname
                                    Try
                                        Dim spbar As Decimal = players / maxplayers * 100
                                        t8_Total.Value = spbar
                                        If t8_Total.Value = 100 And queue = "0" Then
                                            q8_Name.Visible = True
                                            q8_Name.Text = "Full, but no queue"
                                        End If
                                    Catch ex As Exception

                                    End Try
                                    sinteger += 1


                            End Select
                        Next

                End Select
            Next
        Catch ex As Exception
            Excepted = ex
            Return False
        End Try
        Return True
    End Function

    Private Sub refresh_Timer_Tick(sender As Object, e As EventArgs) Handles refresh_Timer.Tick
        If TickTock = 0 Then
            refresh_Timer.Stop()
            Dim Success As Boolean = ServersUpdate()
            If Success = False Then
                CrashHandler.HandleCrash(Excepted)
                Me.Dispose()
            Else
                refresh_Timer.Start()
                TickTock = 30
                refresh_Time.Text = "Refreshing in 30 seconds"
            End If
        Else
            TickTock -= 1
            refresh_Time.Text = "Refreshing in " + TickTock.ToString + " seconds"
        End If
    End Sub

    Private Sub refresh_Now_Click(sender As Object, e As EventArgs) Handles refresh_Now.Click
        refresh_Timer.Stop()
        Dim Success As Boolean = ServersUpdate()
        If Success = False Then
            CrashHandler.HandleCrash(Excepted)
            Me.Dispose()
        Else
            refresh_Timer.Start()
            TickTock = 30
            refresh_Time.Text = "Refreshing in 30 seconds"
        End If
    End Sub

    Private Sub btn_Quit_Click(sender As Object, e As EventArgs)
        Application.Exit()
    End Sub

    Private Sub btn_Server_Click(sender As Object, e As EventArgs) Handles btn_Server.Click
        btn_Server.Enabled = False
        btn_News.Enabled = True
        btn_welcome.Enabled = True
        btn_Tools.Enabled = True
        btn_Play.Enabled = True
        pnl_news.Visible = False
        pnl_server.Visible = True
        pnl_tools.Visible = False
        pnl_play.Visible = False
        pnl_welcome.Visible = False
    End Sub

    Private Sub btn_News_Click(sender As Object, e As EventArgs) Handles btn_News.Click
        news_Browser.Navigate("http://truckersmp.com/blog")
        NewsShown = True

        btn_Server.Enabled = True
        btn_News.Enabled = False
        btn_welcome.Enabled = True
        btn_Tools.Enabled = True
        btn_Play.Enabled = True
        pnl_news.Visible = True
        pnl_server.Visible = False
        pnl_tools.Visible = False
        pnl_play.Visible = False
        pnl_welcome.Visible = False
    End Sub

    Private Sub btn_Tools_Click(sender As Object, e As EventArgs) Handles btn_Tools.Click
        btn_Server.Enabled = True
        btn_News.Enabled = True
        btn_welcome.Enabled = True
        btn_Tools.Enabled = False
        btn_Play.Enabled = True
        pnl_news.Visible = False
        pnl_server.Visible = False
        pnl_tools.Visible = True
        pnl_play.Visible = False
        pnl_welcome.Visible = False
    End Sub

    Private Sub btn_Settings_Click(sender As Object, e As EventArgs) Handles btn_welcome.Click
        btn_Server.Enabled = True
        btn_News.Enabled = True
        btn_welcome.Enabled = False
        btn_Tools.Enabled = True
        btn_Play.Enabled = True
        pnl_news.Visible = False
        pnl_server.Visible = False
        pnl_tools.Visible = False
        pnl_play.Visible = False
        pnl_welcome.Visible = True
    End Sub

    Private Sub player_info_search_Click(sender As Object, e As EventArgs) Handles player_info_search.Click
        Try
            Dim playerinfo As String = "https://api.truckersmp.com/v2/player/" + player_name_text.Text
            Dim pclient As WebClient = New WebClient()
            Dim preader As StreamReader = New StreamReader(pclient.OpenRead(playerinfo))
            Dim pjson As String = preader.ReadToEnd()


            Dim rules As Object = New JavaScriptSerializer().Deserialize(Of Object)(pjson)
            Dim errorname = rules("error")
            Dim response = rules("response")
            Dim id = rules("response")("id")
            Dim pname = rules("response")("name")
            Dim avatar = rules("response")("avatar")
            Dim joindate = rules("response")("joinDate")
            Dim steamID64 = rules("response")("steamID64")
            Dim groupname = rules("response")("groupName")
            Dim permissions = rules("response")("permissions")
            Dim gameadmin = rules("response")("permissions")("isGameAdmin")

            player_info_id.Text = "TruckersMP ID: " + id.ToString
            player_info_name.Text = pname.ToString
            player_info_img.ImageLocation = avatar
            player_info_date.Text = "Joined TruckersMP on: " + joindate.ToString
            player_info_steamid.Text = "SteamID64: " + steamID64.ToString
            player_info_group.Text = "This user is a: " + groupname.ToString

            If gameadmin = True Then
                player_info_admin.Text = "Warning! This player is an administrator!"
            Else
                player_info_admin.Text = "This player is not an administrator."
            End If


        Catch ex As Exception
            CrashHandler.HandleCrash(ex)
            Me.Dispose()
        End Try

    End Sub

    Private Sub player_info_ban_Click(sender As Object, e As EventArgs) Handles player_info_ban.Click
        Try
            player_ban_table.Items.Clear()


            Dim baninfo As String = "https://api.truckersmp.com/v2/bans/" + player_name_text.Text
            Dim bclient As WebClient = New WebClient()
            Dim breader As StreamReader = New StreamReader(bclient.OpenRead(baninfo))
            Dim bjson As String = breader.ReadToEnd()

            Dim bobj As JObject = JObject.Parse(bjson)
            Dim bdata As List(Of JToken) = bobj.Children().ToList
            Dim boutput As String = ""

            For Each bitem As JProperty In bdata
                bitem.CreateReader()
                Select Case bitem.Name
                    Case "response"
                        boutput += "Player information:" + vbCrLf
                        For Each info As JObject In bitem.Values
                            Dim expiration As String = info("expiration")
                            Dim timeAdded As String = info("timeAdded")
                            Dim active As String = info("active")
                            Dim reason As String = info("reason")
                            Dim adminName As String = info("adminName")
                            boutput += expiration + vbCrLf + timeAdded + vbCrLf + active + vbCrLf + reason + vbCrLf + adminName + vbCrLf + vbCrLf

                            Dim AddMe As New ListViewItem
                            AddMe.Text = (timeAdded)
                            AddMe.SubItems.Add(expiration)
                            If active = "True" Then
                                AddMe.SubItems.Add("Yes")
                            Else
                                AddMe.SubItems.Add("No")
                            End If
                            AddMe.SubItems.Add(reason)
                            AddMe.SubItems.Add(adminName)
                            player_ban_table.Items.Add(AddMe)
                        Next
                End Select
            Next


        Catch ex As Exception
            CrashHandler.HandleCrash(ex)
            Me.Dispose()
        End Try
    End Sub

    Private Sub btn_Play_Click(sender As Object, e As EventArgs) Handles btn_Play.Click
        If My.Settings.RulesShown = False Then
            Try
                Dim rulesweb As String = "https://api.truckersmp.com/v2/rules"
                Dim rclient As WebClient = New WebClient()
                Dim rreader As StreamReader = New StreamReader(rclient.OpenRead(rulesweb))
                Dim rjson As String = rreader.ReadToEnd()

                Dim rules As Object = New JavaScriptSerializer().Deserialize(Of Object)(rjson)
                Dim errorname = rules("error")
                Dim rulesstring = rules("rules")
                Dim revision = rules("revision")

                Rules_Popup.RecieveRules(rulesstring)
            Catch ex As Exception
                CrashHandler.HandleCrash(ex)
                Me.Dispose()
            End Try

        Else
            RulesDone()
        End If
    End Sub

    Public Function RulesDone()
        btn_Server.Enabled = True
        btn_News.Enabled = True
        btn_welcome.Enabled = True
        btn_Tools.Enabled = True
        btn_Play.Enabled = False
        pnl_news.Visible = False
        pnl_server.Visible = False
        pnl_tools.Visible = False
        pnl_play.Visible = True
        pnl_welcome.Visible = False

        Return Nothing ' fixes a warning
    End Function

    Private Sub btn_atsmp_Click(sender As Object, e As EventArgs) Handles btn_atsmp.Click ' uses main launcher due to injection, need help on intergrating this
        Try
            Dim readValue = My.Computer.Registry.GetValue(
        "HKEY_LOCAL_MACHINE\SOFTWARE\TruckersMP", "InstallDir", Nothing)

            Process.Start(readValue + "\Launcher.exe")
            Application.Exit()
        Catch ex As Exception
            MsgBox("Could not open TruckersMP official launcher!" + vbCrLf + "Sending you to the download page...", MsgBoxStyle.Exclamation, "Error")
            Process.Start("https://truckersmp.com/download")
        End Try
    End Sub

    Private Sub btn_atssp_Click(sender As Object, e As EventArgs) Handles btn_atssp.Click
        Try
            Dim readValue = My.Computer.Registry.GetValue(
    "HKEY_LOCAL_MACHINE\SOFTWARE\TruckersMP", "InstallLocationATS", Nothing)

            Process.Start(readValue + "\bin\win_x64\amtrucks.exe")
            Application.Exit()
        Catch ex As Exception
            MsgBox("Could not open American Truck Simulator, is it installed?", MsgBoxStyle.Exclamation, "Error")
        End Try
    End Sub

    Private Sub btn_ets2mp_Click(sender As Object, e As EventArgs) Handles btn_ets2mp.Click ' uses main launcher due to injection, need help on intergrating this
        Try
            Dim readValue = My.Computer.Registry.GetValue(
        "HKEY_LOCAL_MACHINE\SOFTWARE\TruckersMP", "InstallDir", Nothing)

            Process.Start(readValue + "\Launcher.exe")
            Application.Exit()
        Catch ex As Exception
            MsgBox("Could not open TruckersMP official launcher!" + vbCrLf + "Sending you to the download page...", MsgBoxStyle.Exclamation, "Error")
            Process.Start("https://truckersmp.com/download")
        End Try
    End Sub

    Private Sub btn_ets2sp_Click(sender As Object, e As EventArgs) Handles btn_ets2sp.Click
        Try
            Dim readValue = My.Computer.Registry.GetValue(
        "HKEY_LOCAL_MACHINE\SOFTWARE\TruckersMP", "InstallLocationETS2", Nothing)

            Process.Start(readValue + "\bin\win_x64\eurotrucks2.exe")
            Application.Exit()
        Catch ex As Exception
            MsgBox("Could not open Euro Truck Simulator 2, is it installed?", MsgBoxStyle.Exclamation, "Error")
        End Try
    End Sub

    Private Sub News_Browser_Navigation_Ended(sender As Object, e As EventArgs) Handles news_Browser.Navigated
        If Not news_Browser.Url.ToString.Contains("truckersmp.com") And NewsShown = True Then
            MsgBox("You must remain on the TruckersMP website!")
            news_Browser.Navigate("https://truckersmp.com/blog")
        End If
    End Sub

    Private Sub yart_truckersfm_play_Click(sender As Object, e As EventArgs) Handles yart_truckersfm_play.Click
        TruckersFM.Show()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If FirstTime = False Then
            Application.Exit()
        End If
    End Sub
End Class
